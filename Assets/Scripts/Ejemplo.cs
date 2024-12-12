using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ejemplo : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public Sprite[] fillSprites;

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateImage);
    }

    void UpdateImage(float value)
    {
        int index = Mathf.FloorToInt(value * (fillSprites.Length - 1));
        fillImage.sprite = fillSprites[index];
    }
}
