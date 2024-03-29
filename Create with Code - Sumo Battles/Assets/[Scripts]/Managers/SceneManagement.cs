using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    [SerializeField] private Settings playerSettings;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        //Debug.Log("Hello");
        
    }

    int LoadJoystickPositioning()
    {
        return playerSettings.DropdownOrientation;
    }

    void OnEnable() 
    {
        //Debug.Log("Scene play");
        SceneManager.sceneLoaded += onNewSceneLoaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= onNewSceneLoaded;
    }

    void onNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("New Scene");
        AudioManager.instance.AddVolumeSliders();

        if(scene.name == "Game")
        {
            PlayerController.GameStart = false;
            GameManager.instance.ApplyKeyMappingPosition(LoadJoystickPositioning());
        }
    }
}
