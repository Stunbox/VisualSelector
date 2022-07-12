using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_Option : MonoBehaviour
{
    
    [SerializeField] public int num;
    [SerializeField] public string text;
    [SerializeField] public Sprite icon;
    [SerializeField] public Color iconColor;

    [SerializeField] private Image _uIicon;
    [SerializeField] private TMP_Text _uItext;

    [SerializeField] public Vector4 angleRotation;
    [SerializeField] public OptionData[] optionsData;
    

    private void Start()
    {
        CalculateAngleForSelected();
    }
    public void SetData(OptionData optionData)
    {

        num = optionData.num;
        text = optionData.text;
        icon = optionData.icon;
        iconColor = optionData.iconColor;
        _uIicon.color = optionData.iconColor;

        optionsData = optionData.optionsData;
        
        _uIicon.sprite = icon;
        _uItext.text = text;
    }
    private void CalculateAngleForSelected()
    {
        angleRotation = new Vector4(0,0,this.GetComponent<RectTransform>().rotation.z*-1, this.GetComponent<RectTransform>().rotation.w);
    }
}
