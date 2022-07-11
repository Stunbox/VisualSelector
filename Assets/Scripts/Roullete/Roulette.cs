using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Roulette : MonoBehaviour
{
    public int _level;
    public R_Option[] r_Options; //defined in editor

    public Roulette nextRoulette;

    public void PreviewNextRoulette(R_Option r_Option)
    {
        if(nextRoulette != null)
        {
            nextRoulette.SetRouletteOptions(r_Option.optionsData);
        }
        
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
}
