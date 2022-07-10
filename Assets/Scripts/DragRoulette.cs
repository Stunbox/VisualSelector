using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragRoulette : MonoBehaviour,IBeginDragHandler, IDragHandler
{
    public bool dragOnSurfaces = true;

    private Camera myCamera;
    private Vector3 screenPos;
    private float angleOffset;
    private Collider2D col;

    private void Start()
    {
        myCamera = Camera.main;
        col = GetComponent<Collider2D>();
        
    }
    private void Update()
    {
        Vector3 mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(col == Physics2D.OverlapPoint(mousePos))
            {
                screenPos = myCamera.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                Vector3 vec3 = Input.mousePosition - screenPos;
                //Debug.Log(vec3);
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                //Debug.Log(angle + " + " + angleOffset);
                transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
            }
        }
    }
}
