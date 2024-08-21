using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    const float _blockLength = 0.35f;

    // Start is called before the first frame update
    void Start() {
        moveCorutine = StartCoroutine(MoveDown());
    }

    // Update is called once per frame
    void Update() {
        handleInput();
    }


    // Shape falling 
    Coroutine moveCorutine = null;

    IEnumerator MoveDown() {
       while (true) {
            transform.Translate(0, -_blockLength, 0, Space.World);
            yield return new WaitForSeconds(1);
        }
    }

    void handleInput() {
        // Movement
        float xOffset = 0;
        float yOffset = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            xOffset = -_blockLength;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            xOffset = _blockLength;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            yOffset = -_blockLength;
        }

        transform.Translate(xOffset, yOffset, 0, Space.World);

        // Rotation
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            transform.eulerAngles += new Vector3(0, 0, -90);
        }
    }
}
