using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PausePanel : MonoBehaviour
{
    public Button PauseButton;
    public TMP_Text SuccessfullSaveText;

    private bool m_IsPause;

    public void OnSaveButton()
    {
        SuccessfullSaveText.alpha = 1f;
        this.TextFade(SuccessfullSaveText, 0.4f, 0.8f, 0f);
        SaveManager.SaveGame(SavablesManager.GenerateSave());
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnResumeButton()
    {
        SwitchPause();
    }

    public void OnPauseButton()
    {
        SwitchPause();
    }

    private void SwitchPause()
    {
        m_IsPause = !m_IsPause;
        PauseButton.gameObject.SetActive(!m_IsPause);
        gameObject.SetActive(m_IsPause);
        Time.timeScale = m_IsPause ? 0 : 1;
        transform.SetAsLastSibling();
    }
}
