using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Parent object shape component
    Shape parentObject;

    private void Start() {
        parentObject = GetComponentInParent<Shape>();
    }

    // Sending border tag to collision handler if intersects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder") || collision.CompareTag("RightBorder")|| collision.CompareTag("BottomBorder")) {
            parentObject.BorderColided(collision.tag);
        }
    }

    // Sending none tag to collision handler if there is no intersection  
    private void OnTriggerExit2D(Collider2D collision)
    {
        parentObject.BorderColided("None");
    }
}
