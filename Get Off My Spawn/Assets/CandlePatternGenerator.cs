using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandlePatternGenerator : MonoBehaviour {

    public float tot_radius = 3.0f;
    public int candle_num = 5;
    public int circle_num = 1;
    public SortingLayer circle_sortlayer;

    public GameObject candle_prefab;

    List<List<Vector2>> pattern;

	// Use this for initialization
	void Awake () {

        int[] circle_sizes = new int[circle_num];

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
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
