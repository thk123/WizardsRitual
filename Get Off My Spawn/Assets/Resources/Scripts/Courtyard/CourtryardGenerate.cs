using UnityEngine;
using System.Collections;

public class CourtryardGenerate : MonoBehaviour {

    public GameObject fence_prefab;
    public Rect fence_shape;
    public Sprite lawn_prefab;

    Texture2D lawn;

	// Use this for initialization
	void Awake () {
        // Generate the fence as per given shape
        GameObject new_side;
        BoxCollider2D box;
        BoundaryFillFence bound;
        // Top side
        new_side = Instantiate(fence_prefab);
        new_side.transform.SetParent(transform, false);
        new_side.transform.localPosition = Vector3.forward;
        box = new_side.GetComponent<BoxCollider2D>();
        box.size = new Vector2(fence_shape.width, 1.0f);
        box.offset = new Vector2(0, fence_shape.y + fence_shape.height);
        bound = new_side.GetComponent<BoundaryFillFence>();
        bound.is_vert = false;
        bound.Fill();
        // Bottom side
        new_side = Instantiate(fence_prefab);
        new_side.transform.SetParent(transform, false);
        new_side.transform.localPosition = Vector3.back;
        box = new_side.GetComponent<BoxCollider2D>();
        box.size = new Vector2(fence_shape.width, 1.0f);
        box.offset = new Vector2(0, fence_shape.y);
        bound = new_side.GetComponent<BoundaryFillFence>();
        bound.is_vert = false;
        bound.Fill();
        // Right side
        new_side = Instantiate(fence_prefab);
        new_side.transform.SetParent(transform, false);
        box = new_side.GetComponent<BoxCollider2D>();
        box.size = new Vector2(1.0f, fence_shape.height);
        box.offset = new Vector2(fence_shape.x+fence_shape.width, 0);
        bound = new_side.GetComponent<BoundaryFillFence>();
        bound.is_vert = true;
        bound.Fill();
        // Left side
        new_side = Instantiate(fence_prefab);
        new_side.transform.SetParent(transform, false);
        box = new_side.GetComponent<BoxCollider2D>();
        box.size = new Vector2(1.0f, fence_shape.height);
        box.offset = new Vector2(fence_shape.x, 0);
        bound = new_side.GetComponent<BoundaryFillFence>();
        bound.is_vert = true;
        bound.Fill();

        // Now the lawn
        float pixPerU = lawn_prefab.pixelsPerUnit;
        // Calculate the required size!
        lawn = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight);
        Texture2D base_tile = lawn_prefab.texture;
        for (int x = 0; x < lawn.width; x += base_tile.width)
        {
            int w = x + base_tile.width > lawn.width-1 ? lawn.width - x : base_tile.width;
            for (int y = 0; y < lawn.height; y += base_tile.height)
            {
                int h = y + base_tile.height > lawn.height-1 ? lawn.height - y : base_tile.height;
                lawn.SetPixels(x, y, w, h, base_tile.GetPixels());
            }
        }
        lawn.Apply();
        GameObject lawn_obj = new GameObject();
        lawn_obj.transform.SetParent(transform, false);
        SpriteRenderer srend = lawn_obj.AddComponent<SpriteRenderer>();
        srend.sprite = Sprite.Create(lawn, new Rect(0, 0, lawn.width, lawn.height), Vector2.one * 0.5f, pixPerU);
        srend.sortingLayerName = "Lawn";
    }

    // Update is called once per frame
    void Update () {
	
	}
}
