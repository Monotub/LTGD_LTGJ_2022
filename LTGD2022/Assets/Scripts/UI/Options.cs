using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Options : MonoBehaviour
{
    [SerializeField] AudioMixer mainMixer;


    public void SetMainVol(float sliderValue)
    {
        // Converts value to logorithmic value for the attenuation of the mixer
        // Set min value of the slider to 0.0001
        mainMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20); 
    }

    public void SetSFXVol(float sliderValue)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVol(float sliderValue)
    {

        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
