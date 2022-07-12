using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [SerializeField] private GameObject _tradePanel;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _objTxt;
    [SerializeField] private TMP_Text _objCountTxt;
    [SerializeField] private GameObject particleExplosion,particleLoop;

    public void ActiveTradePanel(Sprite icon, Color iconColor, string objTxt, float objCount)
    {
        //animation..
        _icon.sprite = icon;
        _icon.color = iconColor;
        _objTxt.text = objTxt;
        _objCountTxt.text = "x"+objCount.ToString();
        _tradePanel.SetActive(true);
        particleExplosion.SetActive(true);
        particleLoop.SetActive(true);
    }
}
