using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 1.0f;
    
    void Update()
    {
        moveSpeed = GameManager.instance.playerSpeed;

        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }
}
