using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour 
{
    public GameObject player;
    public AudioSource Audio;
    private NavMeshAgent agent;
    public AudioClip bark_1;
    public AudioClip bark_2;
    public AudioClip bark_3;
    public AudioClip bark_4;
    public AudioClip chase;
    public AudioClip roar;
    public AudioClip curious;
    private float timer = 0.0f;
    private string state = "wandering";

    private int current_target = -1;
    public Transform target_1;
    public Transform target_2;
    public Transform target_3;
    public Transform target_4;
    private Transform target;
    

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        int seed = System.DateTime.Now.Millisecond;
        target = GetComponent<Transform>();
        Random.InitState(seed);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.keys_remaining <= 2) {
            state = "hunting";
        }

        if (state == "hunting") {
            if (Vector2.Distance(transform.position, player.transform.position) > 25.0f && PlayerController.keys_remaining > 2) { 
                timer = 0.0f;
                state = "curious";
                agent.SetDestination(transform.position);
                AudioSource.PlayClipAtPoint(curious, transform.position, 1.0f);
            }

            agent.SetDestination(player.transform.position);
        } 
 
        else if (state == "curious") {
            if (Vector2.Distance(transform.position, player.transform.position) > 15.0f) { 
                timer = 0.0f;
                state = "wandering";
            }

            if (timer > 1.5f) {
                timer = 0.0f;
                state = "hunting";
                AudioSource.PlayClipAtPoint(roar, transform.position, 1.0f);
            } 
            
            else {
                timer += 1.0f * Time.deltaTime;
            }

            Debug.Log(timer);
        }

        else if (state == "wandering") {
            // If see player
            if (Vector2.Distance(transform.position, player.transform.position) < 10.0f) {
                state = "curious";  
                timer = 0.0f;
                agent.SetDestination(transform.position);
                AudioSource.PlayClipAtPoint(curious, transform.position, 1.0f);
            } 

            // Select new target if reached objective
            else if (current_target < 0 || Vector2.Distance(transform.position, target.position) < 10.0f) {
                int last_target = current_target;

                while (last_target == current_target) {
                    current_target = Random.Range(0, 4);
                }

                if (current_target == 0) {
                    target = target_1;
                }

                else if (current_target == 1) {
                    target = target_2;
                } 

                else if (current_target == 2) {
                    target = target_3;
                } 

                else if (current_target == 3) {
                    target = target_4;
                } 
            }
            
            agent.SetDestination(target.transform.position);
        }
    }
}
