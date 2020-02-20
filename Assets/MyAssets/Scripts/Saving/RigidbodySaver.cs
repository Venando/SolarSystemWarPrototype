using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySaver : MonoBehaviour, ISavable
{
    public Dictionary<string, string> GetSaveValues()
    {
        var rigidbody = GetComponent<Rigidbody>();

        return new Dictionary<string, string>()
        {
            { nameof(rigidbody.velocity), rigidbody.velocity.ToParsableString() },
            { nameof(rigidbody.angularVelocity), rigidbody.angularVelocity.ToParsableString() },
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = loadedValues[nameof(rigidbody.velocity)].ToVector3();
        rigidbody.angularVelocity = loadedValues[nameof(rigidbody.angularVelocity)].ToVector3();
    }
}
