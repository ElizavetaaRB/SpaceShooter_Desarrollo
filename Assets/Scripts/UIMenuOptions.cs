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

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volumen",volume);
    }

    public void ChangeQuality(string qualityName)
    {
        if (qualityName == "Low") { 
            QualitySettings.SetQualityLevel(1); } 
        else if (qualityName == "Medium") { QualitySettings.SetQualityLevel(2); } 
        else if (qualityName == "High") { QualitySettings.SetQualityLevel(3); } 
        else { Debug.LogWarning("Nivel de calidad no reconocido: " + qualityName); }

    }

    private void Start()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel(); 
        qualityDropdown.RefreshShownValue();


        qualityDropdown.onValueChanged.AddListener(delegate {
            ChangeQuality(qualityDropdown.options[qualityDropdown.value].text); });

    }

}
