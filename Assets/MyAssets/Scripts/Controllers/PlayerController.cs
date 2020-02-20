using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RocketFiring))]
public class PlayerController : MonoBehaviour, IController
{
    private RocketFiring m_RocketFiring;

    private void Awake()
    {
        m_RocketFiring = GetComponent<RocketFiring>();
    }

    private void Update()
    {
        if (InputManager.IsClick())
        {
            m_RocketFiring.Fire();
        }
    }
}
