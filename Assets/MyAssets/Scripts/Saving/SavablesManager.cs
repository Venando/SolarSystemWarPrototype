using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct SerializablePair
{
    public string Key;
    public string Value;
}

[System.Serializable]
public struct SerializableList
{
    public List<SerializablePair> List;
}

public class SavablesManager : MonoBehaviour
{
    private static SavablesManager Instance { set; get; }

    private List<(string name, GameObject gameObject)> ObjectsToSave = new List<(string, GameObject)>();

    private void Awake()
    {
        Instance = this;
    }

    public static GameObject InstantiateSavable(GameObject prefab)
    {
        var obj = Instantiate(prefab);
        Instance.AddObjectToSave(prefab.name, obj.gameObject);
        return obj;
    }

    public static T InstantiateSavable<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        var obj = Instantiate(prefab, position, rotation);
        Instance.AddObjectToSave(prefab.name, obj.gameObject);
        return obj;
    }

    private void AddObjectToSave(string name, GameObject gameObject)
    {
        ObjectsToSave.RemoveAll(n => n.gameObject == null);
        ObjectsToSave.Add((name, gameObject));
    }

    public static void LoadSave(string saveData)
    {
        Instance.ObjectsToSave.RemoveAll(n => n.gameObject == null);
        Instance.ObjectsToSave.ForEach(n => Destroy(n.gameObject));

        var savedObjects = JsonUtility.FromJson<SerializableList>(saveData);

        savedObjects.List.ForEach(obj =>
        {
            var objPrefab = Resources.Load<GameObject>(obj.Key);

            var loadedObj = InstantiateSavable(objPrefab);

            var serializedComponentsList = JsonUtility.FromJson<SerializableList>(obj.Value);

            serializedComponentsList.List.ForEach(serializedComponent => 
            {
                var component = loadedObj.GetComponent(serializedComponent.Key);
                var savedValues = JsonUtility.FromJson<SerializableList>(serializedComponent.Value).List.ToDictionary(n => n.Key, n => n.Value);
                (component as ISavable).SetSavedValues(savedValues);
            });

        });
    }

    public static string GenerateSave()
    {
        var objectsToSave = Instance.ObjectsToSave;

        objectsToSave.RemoveAll(n => n.gameObject == null);

        var savedObjectList = objectsToSave.Select(PrefabNameAndGameObjectToSerializablePair).ToList();

        var serializableList = new SerializableList() { List = savedObjectList };

        var json = JsonUtility.ToJson(serializableList);

        return json;
    }

    private static SerializablePair PrefabNameAndGameObjectToSerializablePair((string name, GameObject gameObject) obj)
    {
        var savedObjectList = GameObjectToSerializablePairList(obj.gameObject);
        var serializableList = new SerializableList() { List = savedObjectList };

        var json = JsonUtility.ToJson(serializableList);
        var key = obj.name;

        return new SerializablePair() { Key = key, Value = json };
    }

    private static List<SerializablePair> GameObjectToSerializablePairList(GameObject gameObject)
    {
        return gameObject.GetComponents<ISavable>().Select(SavableToSerializablePair).ToList();
    }

    private static SerializablePair SavableToSerializablePair(ISavable savable)
    {
        var savedObjectList = savable.GetSaveValues().Select(valuesDict =>
        {
            return new SerializablePair() { Key = valuesDict.Key, Value = valuesDict.Value };
        }).ToList();

        var serializableList = new SerializableList() { List = savedObjectList };

        var json = JsonUtility.ToJson(serializableList);
        var key = savable.GetType().ToString();

        return new SerializablePair() { Key = key, Value = json };
    }
}
