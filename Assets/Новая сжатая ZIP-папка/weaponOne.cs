using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponOne : MonoBehaviour
{

    public float offset;

    void Update()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(offset, offset, rotZ + offset);
    }
}
