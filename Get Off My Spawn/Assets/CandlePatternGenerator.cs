using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandlePatternGenerator : MonoBehaviour {

    public float tot_radius = 3.0f;
    public int candle_num = 5;
    public int circle_num = 1;
    public Color circle_color;
    public float circle_width;
    public Material circle_material;
    public GameObject candle_prefab;

    List<List<Vector2>> pattern;
    LineRenderer[] circle_rends;

	// Use this for initialization
	void Awake () {

        int[] circle_sizes = new int[circle_num];
        circle_rends = new LineRenderer[circle_num];

        // Assign the sizes, from the outermost, going inwards
        int candles = candle_num;
        for (int i = circle_num-1; i >= 0; --i)
        {
            float ideal_num = (i + 1) * 1.0f/((circle_num + 1.0f) * circle_num / 2.0f)*candle_num;
            // If integer, okay; if not, round it up.
            if (ideal_num%1 != 0.0f)
            {
                ideal_num = Random.value > 0.5f ? Mathf.Floor(ideal_num) : Mathf.Ceil(ideal_num);
            }
            // A few special cases
            if (ideal_num > candles || i == 0)
                ideal_num = candles;
            circle_sizes[i] = (int)ideal_num;
            candles -= circle_sizes[i];
        }
        // Now assign positions

        pattern = new List<List<Vector2>>();
        for (int i = 0; i < circle_num; ++i)
        {
            float r = tot_radius / circle_num * (i+1);
            float phi = Mathf.PI * 2.0f * Random.value;
            pattern.Add(new List<Vector2>());
            for (int j = 0; j < circle_sizes[i]; ++j)
            {
                pattern[i].Add(new Vector2(r * Mathf.Cos(Mathf.PI * 2.0f / circle_sizes[i] * j + phi),
                                           r * Mathf.Sin(Mathf.PI * 2.0f / circle_sizes[i] * j + phi)));

                GameObject candle = Instantiate(candle_prefab);
                candle.transform.SetParent(transform);
                candle.transform.localPosition = (Vector3)pattern[i][j];
            }
            
            // Now draw the circle
            GameObject circle_draw = new GameObject("circle_" + (i+1));
            circle_draw.transform.SetParent(transform);
            circle_draw.transform.localPosition = Vector3.zero;
            circle_rends[i] = circle_draw.AddComponent<LineRenderer>();
            circle_rends[i].useWorldSpace = false;
            circle_rends[i].SetVertexCount(circle_sizes[i]+1);
            circle_rends[i].SetColors(circle_color, circle_color);
            circle_rends[i].SetWidth(circle_width, circle_width);
            circle_rends[i].material = circle_material;
            circle_rends[i].sortingLayerName = "Circle";
            for (int j = 0; j <= circle_sizes[i]; ++j)
            {
                if (j < circle_sizes[i])
                    circle_rends[i].SetPosition(j, (Vector3)pattern[i][j]);
                else
                    circle_rends[i].SetPosition(j, (Vector3)pattern[i][0]);
            }

        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
