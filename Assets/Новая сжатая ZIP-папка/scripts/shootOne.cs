using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootOne : MonoBehaviour
{
    public Transform fireOne;
    public GameObject bulletPref;
    

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPref, fireOne.position, fireOne.rotation);
    }
}
