using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class UIMenuOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private TMP_Dropdown qualityDropdown;


    private static float volume = 1.0f;

    public void ChangeVolume(float newVolume)
    {
        volume = newVolume; 
        audioMixer.SetFloat("Volumen", volume);
    }

    public void ChangeQuality(string qualityName)
    {
        if (qualityName == "Low") { 
            QualitySettings.SetQualityLevel(1);
        } 
        else if (qualityName == "Medium") { QualitySettings.SetQualityLevel(2);
        } 
        else if (qualityName == "High") { QualitySettings.SetQualityLevel(3);
        } 
        else { Debug.LogWarning("Nivel de calidad no reconocido: " + qualityName); } // update 2.0 game

    }

    private void Start()
    {
        //--------------------------Volume
        audioMixer.SetFloat("Volumen", volume);
        volumeSlider.value = volume; 
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });







        //------------------------Quality
        qualityDropdown.value = QualitySettings.GetQualityLevel() -1;
       // Debug.Log("El nivel de calidad actual es: " + qualityDropdown.value);
        qualityDropdown.RefreshShownValue();


        qualityDropdown.onValueChanged.AddListener(delegate {
            ChangeQuality(qualityDropdown.options[qualityDropdown.value].text); });

    }

    private void Update()
    {
     //   Debug.Log("El nivel de calidad actual es: " + QualitySettings.GetQualityLevel());
    }

}
 