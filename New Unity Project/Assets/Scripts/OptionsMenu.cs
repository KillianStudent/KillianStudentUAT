using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameManager gameManager;
    public AudioMixer audioMixer;

    public bool DailyMapPref;
    public float MusicPref;
    public float SuondEffectPref;

    public GameObject MusicVolumeSlider;
    public GameObject SoundEffectVolumeSlider;
    public GameObject DailyMapToggle;

    int BoolToInt(bool value)   // function to quickly convert bools to ints for PlayerPrefs
    {
        if (value)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    bool IntToBool(int value)   // funciton to quickly convert ints to bools for PlayerPrefs
    {
        if (value != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        gameManager = GetComponent<GameManager>();  // game manager
        DailyMapPref = IntToBool(PlayerPrefs.GetInt("DailyMapPreference")); // gets the daily map preference and sets it on the UI
        SetMapOfTheDay(DailyMapPref);
        DailyMapToggle.GetComponent<Toggle>().isOn = IntToBool(PlayerPrefs.GetInt("DailyMapPreference"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolumePreference"));  // gets the music volume and sets the volume on the slider
        MusicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolumePreference");
        audioMixer.SetFloat("SoundEffectVolume", PlayerPrefs.GetFloat("SoundEffectVolumePreference"));  // gets the sound effect volume and sets the volume on the slider
        SoundEffectVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffectVolumePreference");
    }


    // function to switch between dailymap being true or false
    public void SetMapOfTheDay(bool option) // function that is called when the toggle is changed
    {
        if (option == true) // if the toggle is true
        {
            gameManager.DailyMap = true;    // sets the daily
            DailyMapPref = true;
            PlayerPrefs.SetInt("DailyMapPreference", BoolToInt(DailyMapPref));
        }
        else
        {
            gameManager.DailyMap = false;
            DailyMapPref = false;
            PlayerPrefs.SetInt("DailyMapPreference", BoolToInt(DailyMapPref));
        }
    }

    public void SetMusicVolume(float musicVolume)   // music volume slider
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);    // sets the slider on the audiomixer of MusicVolume
        PlayerPrefs.SetFloat("MusicVolumePreference", musicVolume);
    }

    public void SetSoundEffectVolume(float soundEffectVolume)
    {
        audioMixer.SetFloat("SoundEffectVolume", soundEffectVolume);    // sets the slider on teh audiomixer of SoundEffectVolume
        PlayerPrefs.SetFloat("SoundEffectVolumePreference", soundEffectVolume);
    }
}
