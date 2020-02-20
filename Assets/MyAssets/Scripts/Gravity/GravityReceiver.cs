using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReceiver : GravityObject
{
    public GravityApplier IgnoreGravityApplier { get; set; }

    private void OnEnable()
    {
        GravityController.Instance.AddGravityReceiver(this);
    }

    private void OnDisable()
    {
        GravityController.Instance.RemoveGravityReceiver(this);
    }
}
