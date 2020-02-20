using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSkin : MonoBehaviour, ISavable
{
    public GameObject[] PlanetSkins;

    private int m_SelectedSkin = -1;

    private static int[] SkinQue;
    private static int skinOrder;

    private void InitSkinQue()
    {
        SkinQue = Enumerable.Range(0, PlanetSkins.Length).OrderBy(_ => Random.value).ToArray(); 
    }

    private int GetNextSkinIndex()
    {
        if (SkinQue == null)
            InitSkinQue();
        skinOrder = skinOrder >= SkinQue.Length ? 0 : skinOrder;
        return SkinQue[skinOrder++];
    }

    private void Start()
    {
        CheckSkin();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void CheckSkin()
    {
        if (m_SelectedSkin == -1)
        {
            m_SelectedSkin = GetNextSkinIndex();
            InitSkin();
        }
    }

    private void InitSkin()
    {
        Instantiate(PlanetSkins[m_SelectedSkin], transform);
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        SkinQue = null;
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>() { { nameof(m_SelectedSkin), m_SelectedSkin.ToString() } };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        m_SelectedSkin = int.Parse(loadedValues[nameof(m_SelectedSkin)]);
        InitSkin();
    }
}
