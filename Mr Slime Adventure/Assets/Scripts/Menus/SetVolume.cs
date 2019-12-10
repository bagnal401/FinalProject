using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    // creates a variable that will hold the audio mixer
    public AudioMixer mixer;
    public Slider slider;

    // this start method loads the player's preferred volume level
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }

    // will adjust volume of mixer based on logarithmic conversion
    public void SetLevel()
    {
        float sliderValue = slider.value;
        // remember to assign the right audio mixer to the slider!
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        // sets the player preference so that volume levels stay across scenes
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
