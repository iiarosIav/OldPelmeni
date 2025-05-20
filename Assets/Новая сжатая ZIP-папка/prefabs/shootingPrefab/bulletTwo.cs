using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTwo : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }
}
