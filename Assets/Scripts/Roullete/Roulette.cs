using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Roulette : MonoBehaviour
{
    public int _level;
    public R_Option[] r_Options; //defined in editor
    public Roulette nextRoulette;
    [SerializeField] private bool _focused = false;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private RectTransform _rectTransform;
    public Quaternion quaternion;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        quaternion = _rectTransform.rotation;

    }

    public void PreviewNextRoulette(R_Option r_Option)
    {
        if(nextRoulette != null)
        {
            nextRoulette.SetRouletteOptions(r_Option.optionsData);
        }
    }
    public void FocusOnOption(R_Option option)
    {
        _rectTransform.rotation = new Quaternion(0,0,option.angleRotation.z,option.angleRotation.w);
    }
    public void SetRouletteOptions(OptionData[] optionsData)
    {
        if(optionsData.Length <= r_Options.Length)
        {
            for(int i = 0; i < r_Options.Length;i++) 
            {
                try { 
                    r_Options[i].SetData(optionsData[i]);
                    r_Options[i].gameObject.SetActive(true);
                }
                catch
                {
                    r_Options[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public bool IncreaseSize(float multiplier)
    {
        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
        if(rectTrans.localScale.x == 0)
        {
            rectTrans.localScale = new Vector3(1, 1, 1);
            return false;
        }
        else
        {
            rectTrans.localScale = rectTrans.localScale * multiplier;
            
        }
        CheckCanFocus(rectTrans);
        return true;
    }
    public bool DismisSize()
    {
        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
        if (rectTrans.localScale.x > 0)
        {
            
            if (rectTrans.localScale.x > 0 && rectTrans.localScale.x < 1.5) 
            {
                rectTrans.localScale = new Vector3(0, 0, 0);
                return false;
            }
            rectTrans.localScale = rectTrans.localScale * 0.625f;
            CheckCanFocus(rectTrans);
        }
        return true;
    }
    private void CheckCanFocus(RectTransform rectTrans)
    {
        _focused = (rectTrans.localScale.x > 1.5f) && (rectTrans.localScale.x < 1.7f) ? true : false;
        _collider.enabled = _focused;
    }
}
