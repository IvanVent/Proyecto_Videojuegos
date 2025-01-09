using System;
using System.Collections;
using System.Numerics;
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
    private UnityEngine.Vector2 movement;
    [FormerlySerializedAs("velocidad")] [SerializeField]private float speed=0f;
    public GameObject segundapared,arbol1,arbol2,nubes1,nubes2;
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
        StartCoroutine(waitandstart(9));
        
    }

    IEnumerator waitandstart(float f)
    {
        anda=false;
        yield return new WaitForSeconds(f);
        anda=true;

    }

    // Update is called once per frame
    void Update()
    {   
        if(anda){
            movement = new UnityEngine.Vector2(0.25f, 0);
            rb.velocity = movement * speed;
            animator.SetFloat("Speed",Mathf.Abs(0.25f)+Mathf.Abs(0));
        }
        else{
            movement = new UnityEngine.Vector2(0, 0);
            rb.velocity = movement * speed;
            animator.SetFloat("Speed",Mathf.Abs(0)+Mathf.Abs(0));
        }
        
    }
    public void para(){
        anda=false;
    }
    public void segundaparte(){
        rb.transform.position=new UnityEngine.Vector2(-10,0);
        waitandstart(1);
        segundapared.SetActive(true);
        arbol2.SetActive(true);
        nubes2.SetActive(true);
        arbol1.SetActive(false);
        nubes1.SetActive(false);


    }

}
