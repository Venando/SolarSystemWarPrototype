using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button ContinueButton;

    private void Awake()
    {
        ContinueButton.interactable = SaveManager.HasSave();
    }

    public void OnContinueButton()
    {
        SaveManager.SetSaveLoad();
        SceneManager.LoadScene(1);
    }

    public void OnNewGameButton()
    {
        SceneManager.LoadScene(1);
    }
}
