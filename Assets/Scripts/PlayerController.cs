using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private float h_speed = 0.0f;
    private float v_speed = 0.0f;
    private float speed = 5.0f;
    private AudioSource footsteps;
    public static int keys_remaining = 4;
    public static bool is_dead;

    // Start is called before the first frame update
    void Start()
    {
        is_dead = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        footsteps = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Pickup") {
            keys_remaining--;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Enemy"){
            is_dead = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        h_speed = Input.GetAxisRaw("Horizontal");
        v_speed = Input.GetAxisRaw("Vertical"); 

        if (h_speed > 0) {
            animator.SetInteger("direction", 1);
        } 

        else if (h_speed < 0) {
            animator.SetInteger("direction", 3);
        } 
        
        if (v_speed > 0) {
            animator.SetInteger("direction", 0);
        } 
        
        else if (v_speed < 0) {
            animator.SetInteger("direction", 2);
        } 

        if (h_speed != 0 || v_speed != 0) {
            animator.SetBool("move", true);
            
            if (!footsteps.isPlaying) {
                footsteps.Play();
            }
        } else {
            animator.SetBool("move", false);
        }
    }

    void FixedUpdate() 
    {
        rb.velocity = new Vector2(h_speed * speed, v_speed * speed);
    }
}
