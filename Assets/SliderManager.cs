using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _sliderValue_txt;
    
    public void OnSliderValueChange(float value)
    {
        _sliderValue_txt.text = value.ToString();
    }
    public void OnClickMinus()
    {
        _slider.value--;
        _sliderValue_txt.text = _slider.value.ToString();
    }
    public void OnClickPlus()
    {
        _slider.value++;
        _sliderValue_txt.text = _slider.value.ToString();
    }
}
