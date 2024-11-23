using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : PowerUp
{
    // Start is called before the first frame update
    public UnityEvent<float,int> vida;
    public override void ApplyEffect(GameObject player)
    {
        vida.Invoke(1,2);
    }

}
