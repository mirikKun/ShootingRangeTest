using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,3);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
