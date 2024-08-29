using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPointCoords;
    [SerializeField] GameObject[] shapesPrefabs;
    [SerializeField] BlockRay[] lines;

    int deletedLinesCount;

    // Place random shape at spawnpoint
    public void SpawnShape() {
        int randIndex = Random.Range(0, shapesPrefabs.Length);
        GameObject shape = Instantiate(shapesPrefabs[randIndex], spawnPointCoords.position, Quaternion.identity);
    }

    // Running rays check when shape falls
    public void RunRays() {
        for (int lineID = 0; lineID < lines.Length; lineID++)
            lines[lineID].RunRay();

        CheckLines();
    }

    // Checking lines for completeness and deleting if it is
    void CheckLines() {
        for (int lineID = 0; lineID < lines.Length; lineID++) {  
            if (lines[lineID].blocks.Count == 10) {
                deletedLinesCount++;
                lines[lineID].DestroyLine();
                DropDown(lineID);
            }
        }
    }
    
    // Shift blocks above erased line to the bottom (Looks like a shit, which need to refactor) P.S. messy tutorial tbh not my fault lol 
    void DropDown(int erasedLineID) {
        for (int lineID = erasedLineID - 1; lineID >= 0; lineID--) {
            BlockRay line = lines[lineID];
            for (int blockID = 0; blockID < line.blocks.Count; blockID++)
                line.blocks[blockID].gameObject.transform.position += new Vector3(0, -Shape.blockLength, 0);
        } 

        RunRays();
    }

    void Start() {
        // Initalization of blockRays indexes
        for (int lineID = 0; lineID < lines.Length; lineID++)
            lines[lineID].rowIndex = lineID;

        SpawnShape();

        deletedLinesCount = 0;
    }

    void Update() {
        
    }
}
