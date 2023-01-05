using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    Button[] gameObjButtons;
    [SerializeField, Tooltip("Camera speakers are the global audio source. It's a key tool for soundtracks")]
    AudioSource CameraMusicAudio;
    [SerializeField]
    AudioSource CameraSFXAudio;

    [SerializeField]
    SoundAssets soundAssets;


    [Header("Options Sliders")]
    [SerializeField]
    Slider soundVolSlider;
    [SerializeField]
    Slider musicVolSlider;

    [SerializeField, Range(0f, 1f)]
    public float SoundVolume = 1f;
    [SerializeField, Range(0f, 1f)]
    public float MusicVolume = 0.5f;

    void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Add this function to UI button on clicks
    public void PlayMenuSound(SFX.UI_SFX sound)
    {
        if(CameraSFXAudio == null) return;

        CameraSFXAudio.volume = SoundVolume;

        switch(sound)
        {
            case SFX.UI_SFX.BUTTON_CLICK:
                //if(thisAudio.isPlaying == true) break;
                CameraSFXAudio.PlayOneShot(soundAssets.UIButtonClick);
                break;
        }
    }

    // To use this function, attach an audio source to the gameObject you
    // want the sound clip to play from
    public void PlaySound(SFX.PlayerSFX sound, GameObject audioSourceObj)
    {
        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();

        audioSource.volume = SoundVolume;

        if(audioSource == null)
        {
            Debug.Log(audioSourceObj.name + " has no audio source");
            return;
        }

        switch(sound)
        {
            case SFX.PlayerSFX.WALK:
                //if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.walkingStep);
                break;
            case SFX.PlayerSFX.WALK_BRIDGE:
                //if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.walkingStepBridge);
                break;
            case SFX.PlayerSFX.JUMP:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpUp);
                break;
            case SFX.PlayerSFX.JUMP_LAND:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpLand);
                break;
            case SFX.PlayerSFX.JUMP_LAND_BRIDGE:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpLandBridge);
                break;
            case SFX.PlayerSFX.PLAYER_DAMAGE:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.playerDamage);
                break;
            case SFX.PlayerSFX.PICKUP:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.Pickup);
                break;
            case SFX.PlayerSFX.BUS:
                if (audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.BusSounds);
                break;
            case SFX.PlayerSFX.CHECKPOINT:
                if (audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.Checkpoint);
                break;
        }
    }

    private void PlayInAudioSource(AudioSource specificSource, AudioClip thisClip)
    {
        if(thisClip == null)
        {
            Debug.Log("Sound Manager has no clip for this sound");
        }
        specificSource.clip = thisClip;

        // skips playing if already playing
        if(specificSource.isPlaying && specificSource.clip == thisClip) return;
        specificSource.PlayOneShot(thisClip);
    }


    // NEW LEVEL LOADED IMPLEMENTATION FUNCTIONS
    public void ApplySoundToWidgets()
    {
        // Finds the camera in the current scene
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(camera != null)
        {
            AudioSource[] cameraSounds = camera.GetComponents<AudioSource>();

            foreach(var sound in cameraSounds)
            {
                if(sound.clip == null)
                {
                    CameraSFXAudio = sound;
                    CameraSFXAudio.volume = SoundVolume;
                }
                else if(sound.clip != null)
                {
                    CameraMusicAudio = sound;
                    CameraMusicAudio.volume = MusicVolume;
                }
            }
        }

        // Finds all buttons that exist in the scene (added "true" to add inactive objects)
        gameObjButtons = GameObject.FindObjectsOfType<Button>(true);

        foreach(Button b in gameObjButtons)
        {
            //if(b.onClick.GetPersistentEventCount() > 0) break;

            b.onClick.AddListener(
                delegate
                {
                    PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
                }
            );
        }

        // Finds specific Sliders in the scene (When pause menu gets loaded)
        Slider[] allObjects = GameObject.FindObjectsOfType<Slider>(true);

        foreach(Slider sl in allObjects)
        {
            if(sl.gameObject.tag == "MusicSlider")
            {
                musicVolSlider = sl;
                musicVolSlider.value = MusicVolume;

                musicVolSlider.onValueChanged.AddListener(
                    delegate
                    {
                        changeVolumeValue(musicVolSlider, ref MusicVolume);
                        CameraMusicAudio.volume = MusicVolume;
                    }
                );
            }
            else if(sl.gameObject.tag == "SFXSlider")
            {
                soundVolSlider = sl;
                soundVolSlider.value = SoundVolume;

                soundVolSlider.onValueChanged.AddListener(
                    delegate
                    {
                        changeVolumeValue(soundVolSlider, ref SoundVolume);
                        CameraSFXAudio.volume = SoundVolume;
                    }
                );
            }
        }
    }

    void changeVolumeValue(Slider volSlider, ref float value)
    {
        if(volSlider != null)
        {
            value = volSlider.value;
        }
    }
}
