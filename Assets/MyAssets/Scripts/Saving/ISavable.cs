using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    Dictionary<string, string> GetSaveValues();
    void SetSavedValues(Dictionary<string, string> loadedValues);

}
