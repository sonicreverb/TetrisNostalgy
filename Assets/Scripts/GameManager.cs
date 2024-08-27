using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPointCoords;
    [SerializeField] GameObject[] shapesPrefabs;
    [SerializeField] BlockRay[] lines;

    // Place random shape at spawnpoint
    public void SpawnShape() {
        int randIndex = Random.Range(0, shapesPrefabs.Length);
        GameObject shape = Instantiate(shapesPrefabs[randIndex], spawnPointCoords.position, Quaternion.identity);
    }

    // Running rays check when shape falls
    public void RunRays() {
        for (int lineID = 0; lineID < lines.Length; lineID++) {
            lines[lineID].RunRay();
        }
    }

    void Start() {
        // Initalization of blockRays indexes
        for (int lineID = 0; lineID < lines.Length; lineID++)
            lines[lineID].rowIndex = lineID;

        SpawnShape();
    }

    void Update() {
        
    }
}
