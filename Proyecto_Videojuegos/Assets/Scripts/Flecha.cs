using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad=15f;
    public float tiempo=3f;
    public Vector2 targetVector;
    void Start()
    {
        Destroy(gameObject,tiempo);
        float angulo = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Hitbox de las paredes son colliders por que no queremos que las flechas atraviesen las paredes
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        //La hitbox de la puerta se hace un trigger para que el jugador pueda pasar mientras que al mismo tiempo, destruya las flechas
        if (other.gameObject.CompareTag("door") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
