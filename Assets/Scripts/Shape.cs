using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // One grid unit value
    const float _blockLength = 0.35f;
    
    // Collision with border flags
    bool leftBorderIntersection = false;
    bool rightBorderIntersection = false;

    bool isMoving = true;

    // Blocks stash
    Transform map;

    // Shape falling 
    Coroutine moveCorutine = null;
    IEnumerator MoveDown() {
       while (true) {
            transform.Translate(0, -_blockLength, 0, Space.World);
            yield return new WaitForSeconds(1);
        }
    }

    // Shape freezing
    void StopMovement() {
        if (!isMoving) return;
        else {
            isMoving = false;
            if (moveCorutine != null) {
                StopCoroutine(moveCorutine);
                moveCorutine = null;
            }

            // Assigning shape content to map (parent block stash)
            while (transform.childCount > 0) {
                GameObject blockObj = transform.GetChild(0).gameObject;
                blockObj.GetComponent<Block>().isMoving = false;
                blockObj.transform.SetParent(map);
                blockObj.tag = "StaticBlock";
            }

            // Destroying shape
            Destroy(gameObject);

            // Creating new shape
            FindObjectOfType<GameManager>().SpawnShape();

            // Conditions of rays deleting check
            FindObjectOfType<GameManager>().RunRays();

        }
    }

    // User controls 
    void HandleInput() {
        if (!isMoving) return;
        else {
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
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                transform.eulerAngles += new Vector3(0, 0, -90); // todo fix IShape height diff 
            }
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
            case "StaticBlock":
                StopMovement(); break;
            default:
                leftBorderIntersection = false;
                rightBorderIntersection = false;
                break;
        }
    }

    void Start() {
        moveCorutine = StartCoroutine(MoveDown());
        map = GameObject.FindGameObjectWithTag("Map").transform;
    }

    void Update() {
        HandleInput();
    }
}
