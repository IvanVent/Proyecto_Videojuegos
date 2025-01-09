using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Playerintro : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1;
    public Rigidbody2D rb;
    SpriteRenderer sprite_renderer;
    public Animator animator;
    private Vector2 movement;
    [FormerlySerializedAs("velocidad")] [SerializeField]private float speed=0f;
    private bool anda=false;
    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        //WaitForSeconds(9f);
        
    }
    void Awake(){
        src.clip=sfx1;
        src.Play();
        StartCoroutine(waitandstart());
        
    }

    IEnumerator waitandstart()
    {
        yield return new WaitForSeconds(9);
        anda=true;

    }

    // Update is called once per frame
    void Update()
    {   
        if(anda){
            movement = new Vector2(0.25f, 0);
            rb.velocity = movement * speed;
            animator.SetFloat("Speed",Mathf.Abs(0.25f)+Mathf.Abs(0));
        }
        
    }
}
