using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyLog : MonoBehaviour
{
    float health = 10f;
    float speed = 3f;
    Transform target;
    public int colliderDamage = 2;
    public string colliderTag;
    private IEnumerator coroutine;

  
}

