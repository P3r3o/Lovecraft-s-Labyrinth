using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip exposition;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(exposition, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.keys_remaining == 0) {
            endgame();
        }
        if (PlayerController.is_dead) {
            restart();
        }
    }
    
    void endgame() {
        SceneManager.LoadScene("Win");
        return;
    }
    
    void restart() {
         SceneManager.LoadScene("Level");
         return;
    }
}