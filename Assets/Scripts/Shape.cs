using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // One grid unit value
    const float _blockLength = 0.35f;
    
    // Collision with border flags
    bool leftBorderIntersection = false;
    bool rightBorderIntersection = false ;


    // Shape falling 
    Coroutine moveCorutine = null;
    IEnumerator MoveDown() {
       while (true) {
            transform.Translate(0, -_blockLength, 0, Space.World);
            yield return new WaitForSeconds(1);
        }
    }

    // Shape freezing
    void stopMovement() {
        if (moveCorutine != null) {
            StopCoroutine(moveCorutine);
            moveCorutine = null;
        }
    }

    // User controls 
    void handleInput() {
        // Movement
        float xOffset = 0;
        float yOffset = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !leftBorderIntersection) {
            xOffset = -_blockLength;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !rightBorderIntersection) {
            xOffset = _blockLength;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            yOffset = -_blockLength;
        }

        transform.Translate(xOffset, yOffset, 0, Space.World);

        // Rotation
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            transform.eulerAngles += new Vector3(0, 0, -90); // todo fix IShape height diff 
        }
    }


    // Boundary collision handler
    internal void BorderColided(string tag) {
        switch(tag) {
            case "LeftBorder":
                leftBorderIntersection = true; break;
            case "RightBorder":
                rightBorderIntersection = true; break;
            case "BottomBorder":
                stopMovement(); break;
            default:
                leftBorderIntersection = false;
                rightBorderIntersection = false;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moveCorutine = StartCoroutine(MoveDown());
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
