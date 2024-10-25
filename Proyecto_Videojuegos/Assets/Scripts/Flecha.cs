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
    
    /*
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Las paredes tienen el tag de Wall en el prefab de la habitación (en el Tilemap_Border).
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    
}
