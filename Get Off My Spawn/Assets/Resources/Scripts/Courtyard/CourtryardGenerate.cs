using UnityEngine;
using System.Collections;

public class CourtryardGenerate : MonoBehaviour {

    public GameObject fence_prefab;
    public Rect fence_shape;
    public Sprite[] lawn_prefabs;
    public GameObject gate_prefab;
    public GameObject stone_end_prefab;
    public GameObject stone_way_prefab;
    public float road_spacing = 1.0f, road_side_offset = 0.25f;

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
        bound.mid_prefab = gate_prefab;
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
        bound.mid_prefab = stone_end_prefab;
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

        // Now the road
        float road_y = fence_shape.y;

        while(road_y > Camera.main.transform.position.y-Camera.main.orthographicSize)
        {
            road_y -= road_spacing;
            print(road_y);
            new_side = Instantiate(stone_way_prefab);
            new_side.transform.SetParent(transform, false);
            new_side.transform.localPosition = new Vector3(road_side_offset, road_y);
        }

        // Now the lawn
        float pixPerU = lawn_prefabs[0].pixelsPerUnit;
        // Calculate the required size!
        int tile_width = lawn_prefabs[0].texture.width;
        int tile_height = lawn_prefabs[0].texture.height;
        lawn = new Texture2D(Mathf.CeilToInt(Camera.main.pixelWidth*1.1f), Mathf.CeilToInt(Camera.main.pixelHeight*1.1f));
        Texture2D base_tile;
        for (int x = 0; x < lawn.width; x += tile_width)
        {
            int w = x + tile_width > lawn.width-1 ? lawn.width - x : tile_width;
            for (int y = 0; y < lawn.height; y += tile_height)
            {
                base_tile = lawn_prefabs[Random.Range(0, lawn_prefabs.Length)].texture;
                int h = y + tile_height > lawn.height-1 ? lawn.height - y : tile_height;
                lawn.SetPixels(x, y, w, h, base_tile.GetPixels());
            }
        }
        lawn.Apply();
        GameObject lawn_obj = new GameObject("Lawn");
        lawn_obj.transform.SetParent(transform, false);
        SpriteRenderer srend = lawn_obj.AddComponent<SpriteRenderer>();
        print(lawn.height / (2.2f * Camera.main.orthographicSize));
        srend.sprite = Sprite.Create(lawn, new Rect(0, 0, lawn.width, lawn.height), Vector2.one * 0.5f, lawn.height/(2.2f*Camera.main.orthographicSize));
        srend.sortingLayerName = "Lawn";
    }

    // Update is called once per frame
    void Update () {
	
	}
}
