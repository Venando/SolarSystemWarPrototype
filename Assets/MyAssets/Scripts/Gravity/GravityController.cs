using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float G;

    private List<GravityApplier> m_gravityAppliers = new List<GravityApplier>();
    private List<GravityReceiver> m_gravityReceivers = new List<GravityReceiver>();

    public static GravityController Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        foreach (var receiver in m_gravityReceivers)
        {
            ReceiveGravity(receiver);
        }
    }

    private void ReceiveGravity(GravityReceiver receiver)
    {
        foreach (var applier in m_gravityAppliers)
        {
            if (receiver.IgnoreGravityApplier == applier)
                continue;

            var direction = applier.transform.position - receiver.transform.position;
            var sqrDistance = direction.sqrMagnitude;
            var forceMagnitude = G * ((receiver.rb.mass * applier.rb.mass) / sqrDistance);
            var force = direction.normalized * forceMagnitude;
            receiver.rb.AddForce(force);
        }
    }

    public void AddGravityApplier(GravityApplier gravityApplier)
    {
        if (!m_gravityAppliers.Contains(gravityApplier))
            m_gravityAppliers.Add(gravityApplier);
    }

    public void RemoveGravityApplier(GravityApplier gravityApplier)
    {
        if (m_gravityAppliers.Contains(gravityApplier))
            m_gravityAppliers.Remove(gravityApplier);
    }

    public void AddGravityReceiver(GravityReceiver gravityReceiver)
    {
        if (!m_gravityReceivers.Contains(gravityReceiver))
            m_gravityReceivers.Add(gravityReceiver);
    }

    public void RemoveGravityReceiver(GravityReceiver gravityReceiver)
    {
        if (m_gravityReceivers.Contains(gravityReceiver))
            m_gravityReceivers.Remove(gravityReceiver);
    }
}
