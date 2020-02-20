using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private const string SAVE_KEY = "save";
    private const string IS_SAVE_LOADING = "load";

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

    public static string GetSave()
    {
        return PlayerPrefs.GetString(SAVE_KEY);
    }

    public static void SaveGame(string str)
    {
        PlayerPrefs.SetString(SAVE_KEY, str);
    }

    public static void SetSaveLoad()
    {
        PlayerPrefs.SetInt(IS_SAVE_LOADING, 1);
    }

    public static bool IsSaveLoading()
    {
        return PlayerPrefs.HasKey(IS_SAVE_LOADING);
    }

    public static void GameLoaded()
    {
        PlayerPrefs.DeleteKey(IS_SAVE_LOADING);
    }

}
