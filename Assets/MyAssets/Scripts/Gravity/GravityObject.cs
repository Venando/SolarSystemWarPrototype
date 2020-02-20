using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class GravityObject : MonoBehaviour
{
    public Rigidbody rb { private set; get; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
