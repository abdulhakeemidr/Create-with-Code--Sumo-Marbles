using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Slider volumeSlider;
    public float volume;
    public AudioMixer mainMixer;
    public Sound[] sounds;

    Slider[] allSliders;


    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
        }
    }

    void Start() 
    {
        Play("Theme");
    }

    void Update()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void AddVolumeSliders()
    {
        allSliders = GameObject.FindObjectsOfType<Slider>(true);

        foreach(Slider sl in allSliders)
        {
            if(sl.gameObject.name == "VolumeSlider") volumeSlider = sl;
        }

        volumeSlider.onValueChanged.AddListener(SetVolume
            // delegate
            // {
            //     changeVolumeValue(volumeSlider, ref volume);
            //     foreach(Sound sound in sounds)
            //     {
            //         sound.source.volume = volume;
            //     }
            // }
        );
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

    void changeVolumeValue(Slider volSlider, ref float value)
    {
        if(volSlider != null)
        {
            value = volSlider.value;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
