using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour, ISavable
{
    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>()
        {
            { nameof(transform.position), transform.position.ToParsableString() },
            { nameof(transform.rotation.eulerAngles), transform.rotation.eulerAngles.ToParsableString() },
            { nameof(transform.localScale), transform.localScale.ToParsableString() },
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        transform.position = loadedValues[nameof(transform.position)].ToVector3();
        transform.eulerAngles = loadedValues[nameof(transform.rotation.eulerAngles)].ToVector3();
        transform.localScale = loadedValues[nameof(transform.localScale)].ToVector3();
    }
}
