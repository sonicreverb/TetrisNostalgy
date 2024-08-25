using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Parent object shape component
    Shape parentObject;

    public bool isMoving = true;

    private void Start() {
        parentObject = GetComponentInParent<Shape>();
    }

    // Sending border tag to collision handler if intersects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vertical boundaries
        if (collision.CompareTag("LeftBorder") || collision.CompareTag("RightBorder")) {
            if (parentObject != null) parentObject.BorderColided(collision.tag);
        }
        // Horizontal boundaries
        if (collision.CompareTag("BottomBorder") ||
            (collision.CompareTag("StaticBlock") && Mathf.Approximately(collision.transform.position.x, transform.position.x))) {
            if (parentObject != null) parentObject.BorderColided(collision.tag);
        }
    }

    // Sending none tag to collision handler if there is no intersection  
    private void OnTriggerExit2D(Collider2D collision) {
        if (parentObject != null) parentObject.BorderColided("None");
    }
}
