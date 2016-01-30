using UnityEngine;
using System.Collections;

public class BoundaryFillFence : MonoBehaviour {

    public float fence_size = 1.0f;
    public GameObject fence_prefab;
    public GameObject mid_prefab;
    public bool is_vert;

    BoxCollider2D collid;

	// Use this for initialization
	void Awake () {
        collid = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Fill()
    {
        // Now initialize enough fences to fill in the space
        if (!is_vert)
        {
            for (int x_i = 0; x_i < Mathf.CeilToInt(collid.size.x/fence_size); ++x_i) 
            {
                GameObject newObj;
                float x = collid.offset.x  - collid.size.x/2.0f + x_i * fence_size;
                if (mid_prefab != null && Mathf.Abs(x - collid.offset.x) <= fence_size*1.5f)
                {
                    newObj = Instantiate(mid_prefab);
                    x = x + fence_size * 1.5f;
                    x_i += 3;
                }
                else {
                    newObj = Instantiate(fence_prefab);
                }
                newObj.transform.SetParent(transform);
                newObj.transform.position = new Vector3(collid.offset.x + x, collid.offset.y, transform.position.z);
            }
        }
        else {
            for (float y = collid.offset.y - collid.size.y / 2.0f; y <= collid.offset.y + collid.size.y / 2.0f; y += fence_size)
            {
                GameObject newObj = Instantiate(fence_prefab);
                newObj.transform.SetParent(transform);
                newObj.transform.position = new Vector3(collid.offset.x, collid.offset.y + y, transform.position.z);
            }
        }
    }
}
