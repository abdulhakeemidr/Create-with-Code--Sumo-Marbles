using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct SaveData
{
    [Header("Key Mapping Settings")]
    public TMP_Dropdown DropdownOrientation;
}

public class SaveLoadPrefs : MonoBehaviour
{
    public SaveData UIDisplay;
    public Settings playerSettings;

    void OnEnable()
    {
        LoadSettingsUI();
    }
    
    private void SaveSettings()
    {
        playerSettings.DropdownOrientation = UIDisplay.DropdownOrientation.value;
        Debug.Log("Setting data saved!");
    }

    private void LoadSettingsUI()
    {
        UIDisplay.DropdownOrientation.value = playerSettings.DropdownOrientation;
        Debug.Log("Setting data loaded!");
    }

    void OnDisable()
    {
        SaveSettings();
    }
}
