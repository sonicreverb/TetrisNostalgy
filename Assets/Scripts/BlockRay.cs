using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRay : MonoBehaviour
{
    [SerializeField] float rayDistance;
    Vector3 rayPos;

    // Static blocks container
    public List<Block> blocks = new List<Block>();
    public int rowIndex;

    void Start() {
        rayPos = transform.position;
    }

    // Adding all colided with ray blocks to container (blocks)
    public void RunRay()  {
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.right, rayDistance);

        if (hits.Length > 0) {
            blocks.Clear();
            for (int blockID = 0; blockID < hits.Length; blockID++) {
                if (hits[blockID].collider.CompareTag("StaticBlock"))
                    blocks.Add(hits[blockID].collider.GetComponent<Block>());
            }
        }

        print($"{gameObject.name} - {blocks.Count}");
    }
   
    public void DestroyLine() {
        for (int blockID = 0; blockID < blocks.Count; blockID++)
            Destroy(blocks[blockID].gameObject);

        blocks.Clear();
    }
    void Update() {
    }
}
