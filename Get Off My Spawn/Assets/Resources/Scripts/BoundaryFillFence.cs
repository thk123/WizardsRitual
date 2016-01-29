using UnityEngine;
using System.Collections;

public class BoundaryFillFence : MonoBehaviour {

    public float fence_size = 1.0f;
    public GameObject fence_prefab;

    BoxCollider2D collid;

	// Use this for initialization
	void Awake () {
        collid = GetComponent<BoxCollider2D>();
        // Now initialize enough fences to fill in the space
        for (float x = collid.offset.x-collid.size.x/2.0f; x <= collid.offset.x+collid.size.x/2.0f; x += fence_size)
        {
            GameObject newObj = Instantiate(fence_prefab);
            newObj.transform.SetParent(transform);
            newObj.transform.position = new Vector3(collid.offset.x + x, collid.offset.y, transform.position.z);
        }
        for (float y = collid.offset.y - collid.size.y / 2.0f; y <= collid.offset.y + collid.size.y / 2.0f; y += fence_size)
        {
            GameObject newObj = Instantiate(fence_prefab);
            newObj.transform.SetParent(transform);
            newObj.transform.position = new Vector3(collid.offset.x, collid.offset.y + y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
