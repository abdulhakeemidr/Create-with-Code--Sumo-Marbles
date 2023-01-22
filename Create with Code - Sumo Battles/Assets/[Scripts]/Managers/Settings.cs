using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "SaveData/Settings")]
public class Settings : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume;
}
