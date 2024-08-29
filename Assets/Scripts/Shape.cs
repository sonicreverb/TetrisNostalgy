using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // One grid unit value
    [SerializeField] public const float blockLength = 0.35f;
    [SerializeField] float fallingSpeed = 1;
    // When you press down button yo
    [SerializeField] float fallingAcceleration = 150;

    // Collision with border flags
    bool leftBorderIntersection = false;
    bool rightBorderIntersection = false;

    bool isMoving = true;

    // Blocks stash
    Transform map;

    // Shape falling 
    Coroutine moveCorutine = null;
    // For coroutine needs 
    float coroutineTime = 0f;

    IEnumerator MoveDown() {
       while (true) {
            coroutineTime = 0;
            transform.Translate(0, -blockLength, 0, Space.World);
            yield return new WaitForSeconds(1f / fallingSpeed);
        }
    }


    // Sub func for valid shape speed changes
    void RestartCoroutine() {
        if (moveCorutine != null) {
            StopCoroutine(moveCorutine);
            moveCorutine = StartCoroutine(MoveDown());
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !leftBorderIntersection) {
                xOffset = -blockLength;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && !rightBorderIntersection) {
                xOffset = blockLength;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                fallingSpeed = fallingAcceleration;
                if (coroutineTime > 1f / fallingSpeed) RestartCoroutine();
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)) {
                fallingSpeed = 1;
            }

            transform.Translate(xOffset, 0, 0, Space.World);

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
        if (isMoving) coroutineTime += Time.deltaTime;
        HandleInput();
    }
}
