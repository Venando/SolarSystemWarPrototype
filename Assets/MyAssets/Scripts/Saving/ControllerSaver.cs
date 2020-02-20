using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSaver : MonoBehaviour, ISavable
{
    public Dictionary<string, string> GetSaveValues()
    {
        var controller = GetComponent<IController>();
        return new Dictionary<string, string>()
        {
            { "Controller", controller.GetType().ToString() }
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        gameObject.AddComponent(Type.GetType(loadedValues["Controller"]));
    }
}
