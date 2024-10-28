using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad=55;
    public float tiempo=3;
    public Vector2 targetVector;
    private
    void Start()
    {
        Destroy(gameObject,tiempo);
        float angulo = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(velocidad*Vector2.right*Time.deltaTime); 
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") )
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        //La hitbox de la puerta se hace un trigger para que el jugador pueda pasar mientras que al mismo tiempo, destruya las flechas
        if (other.gameObject.CompareTag("door"))
        {
            Destroy(gameObject);
        }
    }
}
