using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<float,int> vida;
    private float maxlife=3f;

    private Vector3 objetivo;
    public Camera camara;
    // Start is called before the first frame update
    private Vector2 direccion;
    public Rigidbody2D rb;
    private Vector2 input;
    public float velocidad=2;
    private Boolean inmortal=false;
    public GameObject flech,arco,heart,halfheart,noheart;
    SpriteRenderer sprite_renderer;
    
    void Start()
    {
        vida.Invoke(maxlife,0);
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.None;
        objetivo=camara.ScreenToWorldPoint(Input.mousePosition);
        float angulo=Mathf.Atan2(objetivo.y-transform.position.y,objetivo.x-transform.position.x);
        float anguloGrados=(180/Mathf.PI)*angulo;
        transform.rotation=Quaternion.Euler(0,0,anguloGrados);
        input.x=Input.GetAxisRaw("Horizontal");
        input.y=Input.GetAxisRaw("Vertical");
        direccion=input.normalized;
        rb.MovePosition(rb.position+direccion*velocidad*Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)){
            GameObject flechadis=Instantiate(flech,arco.transform.position,Quaternion.identity);
            Flecha flecha=flechadis.GetComponent<Flecha>();
            flecha.targetVector=transform.right;
        
        }
    }
    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Wall")) {
            Vector2 normal = collision.contacts[0].normal;
            // Temporarily freeze horizontal movement
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y)) 
            {
                // Horizontal wall, freeze X movement
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            } 
            else 
            {
                // Vertical wall, freeze Y movement
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

            rb.velocity = Vector2.zero;
        }
      
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy")){
                //print("bbbbbbbbb");
            if(!inmortal){
                vida.Invoke(0.5f, 1);
                inmortal=true;
                StartCoroutine(Invincible());
            }
        }
    }

    private IEnumerator Invincible()
    {
        
        Color color = sprite_renderer.color;
        color.a = Mathf.Clamp01(0.5f);
        sprite_renderer.color = color;
        yield return new WaitForSeconds(2);
        color.a = Mathf.Clamp01(1f);
        sprite_renderer.color = color;
        inmortal=false;
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy")){
                //print("bbbbbbbbb");
            if(!inmortal){
                vida.Invoke(0.5f, 1);
                inmortal=true;
                StartCoroutine(Invincible());
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
    // Restore regular movement when leaving the wall
        if (collision.gameObject.CompareTag("Wall")) {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

}
