using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    [Header("Data manager")]
    [SerializeField] private int _actual_lvl = 0;
    [SerializeField] private Roulette[] _roulettes;

    [SerializeField] private float _sizeMultiplier;
    private R_Option _lastOption;

    [Header("UI Manager")]
    [SerializeField] Button backBtn;
    [SerializeField] Image icon;
    private void Start()
    {
        backBtn.gameObject.SetActive(false);
    }

    public void SetLastOption(R_Option r_Option) //se llama al dejar la opcion seleccionada
    {
        _lastOption = r_Option;
        icon.sprite = _lastOption.icon;
        PreviewNexRoulette();
    }
    private void PreviewNexRoulette() // se llama para ver los elementos de esa opcion
    {
        _roulettes[_actual_lvl].PreviewNextRoulette(_lastOption);
    }
    public void FocusOnOption()
    {
        _roulettes[_actual_lvl].FocusOnOption(_lastOption);
    }
    #region Buttons
    public void OnClickSelectedOption() // se llama al elegir una opcion
    {
        _actual_lvl++;
        backBtn.gameObject.SetActive(true);
        foreach (Roulette roulette in _roulettes)
        {
            if (!roulette.IncreaseSize(_sizeMultiplier)) 
            {
                break;
            };
        }
    }
    public void OnClickReturnOption()
    {
        
        if (_actual_lvl != 0)
        {
            _actual_lvl--;
            
            foreach (Roulette roulette in _roulettes)
            {
                if (!roulette.DismisSize())
                {
                    break;
                };
            }
            backBtn.gameObject.SetActive(_actual_lvl > 0);
        }
        
    }

    #endregion
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

