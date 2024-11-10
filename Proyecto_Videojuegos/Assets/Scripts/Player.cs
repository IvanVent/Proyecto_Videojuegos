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

    [SerializeField] private float shootCooldown;
    private bool canShoot = true;
    private Vector3 objetivo;
    public Camera camara;
 
    private Vector2 direccion;
    public Rigidbody2D rb;
    private Vector2 input;
    public float velocidad=2;
    private Boolean inmortal=false;
    public GameObject flech,arco,heart,halfheart,noheart;
    SpriteRenderer sprite_renderer;
    
    private bool isDead=false;
    public Animator animator;
    void Start()
    {
        vida.Invoke(maxlife,0);
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }
    
    void Update()
    {
        
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.None;


        if (!isDead)
        {
            objetivo=camara.ScreenToWorldPoint(Input.mousePosition);
            float angulo=Mathf.Atan2(objetivo.y-transform.position.y,objetivo.x-transform.position.x);
            float anguloGrados=(180/Mathf.PI)*angulo;
            transform.rotation=Quaternion.Euler(0,0,anguloGrados);
            
            input.x=Input.GetAxisRaw("Horizontal");
            input.y=Input.GetAxisRaw("Vertical");
            direccion=input.normalized;
            rb.MovePosition(rb.position+direccion*velocidad*Time.deltaTime);
        
            if((Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)) && canShoot){
                GameObject flechadis=Instantiate(flech,arco.transform.position,Quaternion.identity);
                Flecha flecha=flechadis.GetComponent<Flecha>();
                flecha.targetVector=transform.right;
                StartCoroutine(ShootCooldown());
        
            }
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
        if(collider.gameObject.CompareTag("Enemy") && !isDead){
          
            if(!inmortal){
                vida.Invoke(0.5f, 1);
                inmortal=true;
                StartCoroutine(Invincible());
            }
        }
    }

    private IEnumerator Invincible()
    {
        
        animator.SetBool("Damage",true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Damage",false);
        inmortal=false;
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy") && !isDead){
            
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
    
    // setter para un booleano que no permite mover el personaje cuando está muerto
    public void SetIsDead()
    {
        isDead=true;
    }

}
