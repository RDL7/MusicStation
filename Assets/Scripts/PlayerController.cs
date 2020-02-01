using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = GameManager.instance.playerSpeed;

        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }
}
