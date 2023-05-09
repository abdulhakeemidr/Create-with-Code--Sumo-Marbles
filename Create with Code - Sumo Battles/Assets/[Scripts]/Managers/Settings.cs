using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "SaveData/Settings")]
public class Settings : ScriptableObject
{
    [Header("Sound Settings"), Range(0f, 1f)]
    public float MusicVolume;
    [Range(0f, 1f)]
    public float SoundVolume;
    [Header("Key Mappings")]
    public bool InvertYAxis;
    public bool InvertXAxis;
    public int DropdownOrientation;
}
