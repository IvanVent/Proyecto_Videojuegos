using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugador : MonoBehaviour
{

    private Vector3 objetivo;
    public Camera camara;
    // Start is called before the first frame update
    private Vector2 direccion;
    public Rigidbody2D rb;
    private Vector2 input;
    public float velocidad=2;
    public GameObject flech,arco;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            // Temporarily freeze horizontal movement
            
            rb.constraints = RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezePositionY;
            rb.velocity=new Vector2(0,0);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
    // Restore regular movement when leaving the wall
        if (collision.gameObject.CompareTag("Wall")) {
            rb.constraints = RigidbodyConstraints2D.None;  // Remove all constraints
        }
    }

}
