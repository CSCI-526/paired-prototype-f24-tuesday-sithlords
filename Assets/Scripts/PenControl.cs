using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenControl : MonoBehaviour
{
    
    private Rigidbody2D pen;
    private Vector2 startDragPosition;   
    private Vector2 endDragPosition;     
    private bool isDragging = false;
    public float flickForceMultiplier = 10f;


    void Start()
    {
        Application.targetFrameRate = 60;
        pen = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is on the pen's collider
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePosition))
            {
                startDragPosition = mousePosition;
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FlickPen();
            isDragging = false;
        }

        void FlickPen()
        {
            
            Vector2 flickDirection = (endDragPosition - startDragPosition).normalized;
            float flickMagnitude = (endDragPosition - startDragPosition).magnitude;

            
            pen.AddForce(flickDirection * flickMagnitude * flickForceMultiplier, ForceMode2D.Impulse);
        }

    }
}
