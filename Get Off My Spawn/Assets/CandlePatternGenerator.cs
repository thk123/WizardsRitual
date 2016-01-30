using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandlePatternGenerator : MonoBehaviour {

    public struct candle_pos
    {
        public int circle;
        public int candle;

        public candle_pos(int CircleIndex, int ClockPosition)
        {
            circle = CircleIndex;
            candle = ClockPosition;
        }

        public override string ToString()
        {
            return circle + ":" + candle;
        }
    }

    public float tot_radius = 3.0f;
    public int candle_num = 5;
    public int circle_num = 1;
    public Color circle_color;
    public float circle_width;
    public Material circle_material;
    public Candle candle_prefab;

    List<List<Vector2>> pattern;
    LineRenderer[] circle_rends;
    Queue<Candle> correct_sequence;

    List<Candle> Candles;

    public GameObject NextCandlePrefab;
    GameObject CurrentMarker;

	// Use this for initialization
	void Awake () {

        Candles = new List<Candle>();

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
        for (int circle_index = 0; circle_index < circle_num; ++circle_index)
        {
            float r = tot_radius / circle_num * (circle_index+1);
            float phi = Mathf.PI * 2.0f * Random.value;
            pattern.Add(new List<Vector2>());
            for (int j = 0; j < circle_sizes[circle_index]; ++j)
            {
                pattern[circle_index].Add(new Vector2(r * Mathf.Cos(Mathf.PI * 2.0f / circle_sizes[circle_index] * j + phi),
                                           r * Mathf.Sin(Mathf.PI * 2.0f / circle_sizes[circle_index] * j + phi)));

                Candle candle = Instantiate(candle_prefab);
                candle.transform.SetParent(transform);
                candle.transform.localPosition = (Vector3)pattern[circle_index][j];
                candle.SetCandlePosition(new candle_pos(circle_index, j));
                candle.OnCandleLit += Candle_OnCandleLit;
                Candles.Add(candle);

            }
            
            // Now draw the circle
            GameObject circle_draw = new GameObject("circle_" + (circle_index+1));
            circle_draw.transform.SetParent(transform);
            circle_draw.transform.localPosition = Vector3.zero;
            circle_rends[circle_index] = circle_draw.AddComponent<LineRenderer>();
            circle_rends[circle_index].useWorldSpace = false;
            circle_rends[circle_index].SetVertexCount(circle_sizes[circle_index]+1);
            circle_rends[circle_index].SetColors(circle_color, circle_color);
            circle_rends[circle_index].SetWidth(circle_width, circle_width);
            circle_rends[circle_index].material = circle_material;
            circle_rends[circle_index].sortingLayerName = "Circle";
            for (int j = 0; j <= circle_sizes[circle_index]; ++j)
            {
                if (j < circle_sizes[circle_index])
                    circle_rends[circle_index].SetPosition(j, (Vector3)pattern[circle_index][j]);
                else
                    circle_rends[circle_index].SetPosition(j, (Vector3)pattern[circle_index][0]);
            }

        }

        // And generate the correct sequence!
        /*List<candle_pos> ordered_sequence = new List<candle_pos>();
        candle_pos cpos = new candle_pos();

        for (int i = 0; i < circle_num; ++i)
        {
            for (int j = 0; j < circle_sizes[i]; ++j)
            {
                cpos.circle = i;
                cpos.candle = j;
                ordered_sequence.Add(cpos);
            }
        }
        // Now shuffle the ordered sequence
        for (int i = 0; i < ordered_sequence.Count; ++i)
        {
            int iswap = Random.Range(i, ordered_sequence.Count);
            cpos = ordered_sequence[i];
            ordered_sequence[i] = ordered_sequence[iswap];
            ordered_sequence[iswap] = cpos;
        }
        correct_sequence = new Queue<candle_pos>(ordered_sequence);*/

        //List<candle_pos> ordered_sequence = new List<candle_pos>();
        /*candle_pos cpos = new candle_pos();

        for (int i = 0; i < circle_num; ++i)
        {
            for (int j = 0; j < circle_sizes[i]; ++j)
            {
                cpos.circle = i;
                cpos.candle = j;
                ordered_sequence.Add(cpos);
            }
        }*/
        // Now shuffle the ordered sequence
        for (int i = 0; i < Candles.Count; ++i)
        {
            int iswap = Random.Range(i, Candles.Count);
            Candle cpos = Candles[i];
            Candles[i] = Candles[iswap];
            Candles[iswap] = cpos;
        }
        correct_sequence = new Queue<Candle>(Candles);

        HighlightCandle(correct_sequence.Peek());
    }
	
	// Update is called once per frame
	void Update () {
	}

    void Candle_OnCandleLit(Candle sender)
    {
        if(correct_sequence.Peek() == sender)
        {
            correct_sequence.Dequeue();
            Candle next_candle = correct_sequence.Peek();
            HighlightCandle(next_candle);
        }
        else
        {
            sender.SetCandleLit(false);
            // TODO: knock some quality off the summoning
        }
    }

    void HighlightCandle(Candle next_candle)
    {
        if(CurrentMarker == null)
        {
            CurrentMarker = Instantiate(NextCandlePrefab);    
        }
        CurrentMarker.transform.parent = next_candle.transform;
        CurrentMarker.transform.localPosition = Vector3.zero;
        
    }
}



