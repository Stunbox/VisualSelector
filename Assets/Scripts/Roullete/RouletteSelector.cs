using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSelector : MonoBehaviour
{
    [SerializeField] private RouletteManager _rouletteManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("R_Option"))
        {
            _rouletteManager.SetLastOption(collision.GetComponent<R_Option>());
        }
    }
}
