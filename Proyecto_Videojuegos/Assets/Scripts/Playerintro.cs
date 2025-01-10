using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Playerintro : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1,sfx2;
    public Rigidbody2D rb;
    SpriteRenderer sprite_renderer;
    public Animator animator;
    private UnityEngine.Vector2 movement;
    float caidax=7f;
    float caiday=0f;
    public Iniciadorsiguienteintro ini;
    private bool cae=false;
    [FormerlySerializedAs("velocidad")] [SerializeField]private float speed=0f;
    public GameObject segundapared,arbol1,arbol2,nubes1,nubes2,talkingtext;
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
        talkingtext.SetActive(true);
        src.clip=sfx1;
        src.Play();
        StartCoroutine(waitandstart(9));
        
    }

    IEnumerator waitandstart(float f)
    {
        anda=false;
        yield return new WaitForSeconds(f);
        anda=true;
        talkingtext.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {   
        if(anda){
            movement = new UnityEngine.Vector2(0.25f, 0);
            rb.velocity = movement * speed;
            animator.SetFloat("Speed",Mathf.Abs(0.25f)+Mathf.Abs(0));
        }
        else if(cae){
            rb.velocity=new UnityEngine.Vector2(caidax, caiday);
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
    public void Cae(){
        cae=true;
        src.clip=sfx2;
        src.Play();
        StartCoroutine(parabola());
    }
    IEnumerator parabola()
    {
        while(true){
            yield return new WaitForSeconds(0.3f);
            caidax-=1;
            caiday-=1;
        }

    }
    public void OnTriggerEnter2D(Collider2D trigger){
        
        
        
        if(trigger.CompareTag("abajo")){
            ini.siguiente();
            gameObject.SetActive(false);
        }
        
    }
    

}
