using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RouletteManager : MonoBehaviour
{
    [SerializeField] private int actual_lvl = 0;
    [SerializeField] private Roulette[] roulettes;
    [SerializeField] private R_Option _lastOption;

    public void SetLastOption(R_Option r_Option)
    {
        _lastOption = r_Option;
        PreviewNexRoulette(actual_lvl);
    }
    private void PreviewNexRoulette(int actual_lvl)
    {
        roulettes[actual_lvl].PreviewNextRoulette(_lastOption);
    }

}
[System.Serializable]
public class OptionData
{
    [SerializeField] public int num;
    [SerializeField] public string text;
    [SerializeField] public Sprite icon;

    [SerializeField] public OptionData[] optionsData;
    public OptionData(int num)
    {
        this.num = num;
        optionsData = new OptionData[6];
    }
    public void SetData(string text,Sprite icon)
    {
        this.text = text;
        this.icon = icon;
    }
}

