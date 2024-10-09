using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public Transform player;
    public float transitionSpeed;

    private Vector3 targetPos, newPos;

    public Vector3 minPos, maxPos;
    

    // Hace update una vez ya se han hecho todos los updates
    void LateUpdate()
    {
        if(transform.position != player.position){
            targetPos = player.position;

            Vector3 camBoundaryPos = new Vector3(
                Mathf.Clamp(targetPos.x, minPos.x, maxPos.x),
                Mathf.Clamp(targetPos.y, minPos.y, maxPos.y),
                Mathf.Clamp(targetPos.z, minPos.z, maxPos.z));

            newPos = Vector3.Lerp(transform.position, camBoundaryPos, transitionSpeed);
            transform.position = newPos;
        }
    }
}
