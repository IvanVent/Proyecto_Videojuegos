using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad=55;
    public float tiempo=6;
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
}
