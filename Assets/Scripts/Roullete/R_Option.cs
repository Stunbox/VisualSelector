using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_Option : MonoBehaviour
{
    
    [SerializeField] private int _num;
    [SerializeField] private string _text;
    [SerializeField] private Sprite _icon;

    [SerializeField] private Image _uIicon;
    [SerializeField] private TMP_Text _uItext;

    [SerializeField] public OptionData[] optionsData;
    
    public void SetData(OptionData optionData)
    {

        _num = optionData.num;
        _text = optionData.text;
        _icon = optionData.icon;

        _uIicon.sprite = _icon;
        _uItext.text = _text;
    }
}
