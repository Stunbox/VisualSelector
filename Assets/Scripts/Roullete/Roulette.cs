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
    public RectTransform rectTransform;
    [SerializeField] private bool _focused = false;
    [SerializeField] private Collider2D _collider;
    
    [SerializeField] private GameObject _empty;
    public float sizeAnimDuration;
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void PreviewNextRoulette(R_Option r_Option)
    {

        Vector3 from = new Vector3(1,1,1);
        Vector3 to = new Vector3(0, 0, 0);

        StartCoroutine(Lerp(from, to, sizeAnimDuration/1.5f, nextRoulette.rectTransform, () =>
        {
            if (r_Option.optionsData.Length > 0 && nextRoulette != null)
            {
                nextRoulette.SetRouletteOptions(r_Option.optionsData);
                nextRoulette._empty.SetActive(false);
            }
            else
            {
                nextRoulette.ClearRouletteOptions();
                nextRoulette._empty.SetActive(true);
            }
            StartCoroutine(Lerp(to, from, sizeAnimDuration/1.5f, nextRoulette.rectTransform, () => { }));
        }
        ));
    }
    public void FocusOnOption(R_Option option)
    {
        Quaternion from = rectTransform.rotation;
        Quaternion to = new Quaternion(0, 0, option.angleRotation.z, option.angleRotation.w);

        StartCoroutine(Lerp(from, to, sizeAnimDuration / 1.5f, rectTransform, () => { }));
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
    public void ClearRouletteOptions()
    {
        for (int i = 0; i < r_Options.Length; i++)
        {
            r_Options[i].gameObject.SetActive(false);
        }
    }
    public bool IncreaseSize(float multiplier)
    {
        bool contineIncrasing = true;
        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
        Vector3 fromSize = rectTrans.localScale;
        Vector3 toSize;

        if(fromSize.x == 0)
        {
            toSize = new Vector3(1, 1, 1);
            contineIncrasing = false;
        }
        else
        {
            toSize = rectTrans.localScale * multiplier;   
        }

        StartCoroutine(Lerp(fromSize, toSize, sizeAnimDuration,rectTrans,()=> { CheckCanFocus(rectTrans); }));
        return contineIncrasing;
    }
    
    public bool DismisSize()
    {
        RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();
        Vector3 fromSize = rectTrans.localScale;
        Vector3 toSize;
        if (fromSize.x > 0)
        {
            if (fromSize.x > 0 && fromSize.x < 1.5) 
            {
                toSize = new Vector3(0, 0, 0);
                StartCoroutine(Lerp(fromSize, toSize, sizeAnimDuration, rectTrans, () => {}));
                return false;
            }
            toSize = rectTrans.localScale * 0.625f;

            StartCoroutine(Lerp(fromSize, toSize, sizeAnimDuration, rectTrans, () => { CheckCanFocus(rectTrans); }));
        }
        return true;
    }
    private void CheckCanFocus(RectTransform rectTrans)
    {
        _focused = (rectTrans.localScale.x > 1.5f) && (rectTrans.localScale.x < 1.7f) ? true : false;
        _collider.enabled = _focused;
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
    IEnumerator Lerp(Quaternion from, Quaternion to, float duration, RectTransform rectTrans, EmptyFunction onFinished)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            rectTrans.rotation = Quaternion.Lerp(from, to, elapsedTime / duration);
            yield return null;
        }
        onFinished();
    }
}
