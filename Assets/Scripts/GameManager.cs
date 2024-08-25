using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPointCoords;
    [SerializeField] GameObject[] shapesPrefabs;


    void Start() {
        SpawnShape();
    }

    void Update() {
        
    }

    // Place random shape at spawnpoint
    public void SpawnShape() {
        int randIndex = Random.Range(0, shapesPrefabs.Length);
        GameObject shape = Instantiate(shapesPrefabs[randIndex], spawnPointCoords.position, Quaternion.identity);
    }
}
