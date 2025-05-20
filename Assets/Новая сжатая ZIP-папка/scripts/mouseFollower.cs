using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollower : MonoBehaviour
{
    public void Update()
    {
        Vector2 cursosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2 (cursosPos.x, cursosPos.y);
    }
}
