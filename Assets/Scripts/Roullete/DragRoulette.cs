using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragRoulette : MonoBehaviour
{
    public bool dragOnSurfaces = true;

    private Camera _myCamera;
    private Vector3 _screenPos;
    private float _angleOffset;
    private Collider2D _col;

    private void Start()
    {
        _myCamera = Camera.main;
        _col = GetComponent<Collider2D>();
        
    }
    private void Update()
    {
        Vector3 mousePos = _myCamera.ScreenToWorldPoint(Input.mousePosition);
        if (_col == Physics2D.OverlapPoint(mousePos)) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                _screenPos = _myCamera.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - _screenPos;
                _angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
            if (Input.GetMouseButton(0))
            {
                {
                    Vector3 vec3 = Input.mousePosition - _screenPos;
                    float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle + _angleOffset);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("MouseUp");
                //Check actual option;
            }
        }
            
        
    }
}
