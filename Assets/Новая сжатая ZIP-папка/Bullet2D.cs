using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet2D : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.isTrigger) 
        {
            switch (coll.tag)
            {
                case "Enemy_1":
                    break;
                case "Enemy_2":
                    break;
            }

            Destroy(gameObject);
        }
    }
}
