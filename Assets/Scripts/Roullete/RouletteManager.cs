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

    private bool reached = false;

    [Header("UI Manager")]
    [SerializeField] Button backBtn;
    [SerializeField] Image icon;
    [SerializeField] SliderManager sliderManager;
    [SerializeField] TradeManager tradeManager;
    [Header("Animation Manager")]
    [SerializeField] AnimationManager animationManager;
    private void Start()
    {
        SetConfigOnRoulettes();
        backBtn.gameObject.SetActive(false);
    }
    private void SetConfigOnRoulettes()
    {
        foreach (Roulette roulette in _roulettes)
        {
            roulette.sizeAnimDuration = animationManager.sizeAnimDuration;
        }
    }
    public void SetLastOption(R_Option r_Option) //se llama al dejar la opcion seleccionada
    {
        if (_lastOption != null) { animationManager.SetUnHighlightOption(_lastOption.GetComponent<Animator>()); }
        
        _lastOption = r_Option;
        animationManager.SetHighlightOption(_lastOption.GetComponent<Animator>());

        Vector3 from = icon.rectTransform.localScale;
        Vector3 to = new Vector3(0, 0, 0);

        StartCoroutine(Lerp(from, to, animationManager.sizeAnimDuration / 1.5f,icon.rectTransform, () => 
        {
            icon.sprite = _lastOption.icon;
            Color color = new Color(_lastOption.iconColor.r, _lastOption.iconColor.g, _lastOption.iconColor.b, 1f);
            icon.color = color;
            StartCoroutine(Lerp(to, from, animationManager.sizeAnimDuration / 1.5f, icon.rectTransform, () => { }));
        }));

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
    private bool CheckIsSomeOptionActive(Roulette roulette)
    {
        R_Option[] options = roulette.gameObject.GetComponentsInChildren<R_Option>();
        foreach(R_Option option in options)
        {
            if (option.gameObject.activeInHierarchy) { return true; }
        }
        return false;
    }
    #region Buttons
    public void OnClickSelectedOption() // se llama al elegir una opcion
    {
        if (!reached)
        {
            if (CheckIsSomeOptionActive(_roulettes[_actual_lvl+1]))
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
                if (_actual_lvl == _roulettes.Length - 2) //menos los dos primeros niveles que ya estan visibles
                {
                    sliderManager.gameObject.SetActive(true);
                    reached = true;
                }
            }
        }
        else
        {
            if(sliderManager.GetSliderValue() > 0)
            {
                Color color = new Color(_lastOption.iconColor.r, _lastOption.iconColor.g, _lastOption.iconColor.b, 1f);
                tradeManager.ActiveTradePanel(_lastOption.icon, color, _lastOption.optionsData[0].text, sliderManager.GetSliderValue());
            }
            
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
        if (_actual_lvl < _roulettes.Length - 2) //menos los dos primeros niveles que ya estan visibles
        {
            sliderManager.gameObject.SetActive(false);
            reached = false;
        }
    }
    IEnumerator Lerp(Vector3 from, Vector3 to, float duration, RectTransform rectTrans, EmptyFunction onFinished)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            rectTrans.localScale = Vector3.Lerp(from, to, elapsedTime / duration);
            yield return null;
        }
        onFinished();
    }
    #endregion
}
public delegate void EmptyFunction();
[System.Serializable]
public class OptionData
{
    [SerializeField] public int num;
    [SerializeField] public string text;
    [SerializeField] public Sprite icon;
    [SerializeField] public Color iconColor;

    [SerializeField] public OptionData[] optionsData;
}

