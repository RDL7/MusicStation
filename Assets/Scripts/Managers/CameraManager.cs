using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;
    public Vector3 currentCameraPosition;

    private void Awake() {
        currentCameraPosition = transform.position;
        if(offset == Vector3.zero) {
            offset = currentCameraPosition;
        }
        
    }

    [Range (0, 100)]
    public float speed = 10f;
    public Vector3 offset;
    void LateUpdate ()
    {
        if (target != null)
        {
            float interpolation = speed * Time.deltaTime;
            Vector3 desiredPosition = target.transform.position + offset;
            // Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, interpolation);
            transform.position = desiredPosition;
        }
    }
}