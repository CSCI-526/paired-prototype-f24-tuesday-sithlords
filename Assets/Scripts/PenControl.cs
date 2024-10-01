using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D pen;
    private Vector2 startDragPosition;   // Where the drag starts
    private Vector2 endDragPosition;     // Where the drag ends
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
        // Detect when the mouse button is pressed
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
            // Calculate the direction and magnitude of the flick
            Vector2 flickDirection = (endDragPosition - startDragPosition).normalized;
            float flickMagnitude = (endDragPosition - startDragPosition).magnitude;

            // Apply force to the pen's rigidbody based on the drag distance and direction
            pen.AddForce(flickDirection * flickMagnitude * flickForceMultiplier, ForceMode2D.Impulse);
        }

    }
}
