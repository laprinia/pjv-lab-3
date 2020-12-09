using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Slider _slider;
    public Gradient Gradient;
    public Image fillImage;
    public void SetHealth(int health)
    {
        _slider.value = health;
        fillImage.color = Gradient.Evaluate(_slider.normalizedValue);
    }

    public void SetMaximumHealth(int maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
        fillImage.color=Gradient.Evaluate(1f);
    }
}
