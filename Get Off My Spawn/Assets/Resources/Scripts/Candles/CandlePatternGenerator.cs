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
    //public float circle_width;
    //public Material circle_material;
    public Candle candle_prefab;
    public int circle_side;
    public GameObject explosion_prefab;
    public SummonDescription[] summons;

    public AudioClip[] demon_voices;

    int[] circle_sizes;
    List<List<Vector2>> pattern;
    //LineRenderer[] circle_rends;
    List<Candle> correct_sequence;

    List<Candle> Candles;

    public GameObject NextCandlePrefab;
    GameObject CurrentMarker;
    Texture2D circle_drawing;
    GameObject CircleObject;


	// Use this for initialization
	void Awake () {
        Candles = new List<Candle>();
        Restart();        
    }
	
    public void Restart()
    {
        // Remove any candles
        foreach(Candle candle in Candles)
        {
            GameObject.Destroy(candle.gameObject);
        }

        // Remove the circle
        if(CircleObject != null)
        {
            GameObject.Destroy(CircleObject);
        }

        if(CurrentMarker != null)
        {
            GameObject.Destroy(CurrentMarker);
            CurrentMarker = null;
        }

        Candles = new List<Candle>();

        circle_sizes = new int[circle_num];
        //circle_rends = new LineRenderer[circle_num];

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
                candle.OnCandleExtinguished += Candle_OnCandleExtinguished;
                Candles.Add(candle);

            }

        }
        // Now shuffle the ordered sequence
        for (int i = 0; i < Candles.Count; ++i)
        {
            int iswap = Random.Range(i, Candles.Count);
            Candle cpos = Candles[i];
            Candles[i] = Candles[iswap];
            Candles[iswap] = cpos;
        }
        correct_sequence = new List<Candle>(Candles);

        HighlightCandle(correct_sequence[0]);

        // Now draw the circle
        DrawCircle();

        //Sort the array of demons
        System.Array.Sort(summons, (s1, s2) => (int)Mathf.Sign(s1.demon_threshold - s2.demon_threshold));

    }

    void Start()
    {
        Summoner.SummonSuccess += SummonDemon;
    }

	// Update is called once per frame
	void Update () {
	}

    void DrawCircle()
    {
        float pixel_size = circle_side / (2.0f * tot_radius);
        Color[] color_array = new Color[circle_side*circle_side];

        circle_drawing = new Texture2D(circle_side, circle_side);
        circle_drawing.alphaIsTransparency = true;
        circle_drawing.filterMode = FilterMode.Point;
        // Start by clearing
        for (int i = 0; i < circle_side*circle_side; ++i)
        {
            color_array[i] = Color.clear;
        }
        // Now draw the circle
        for (int circle_i = 0; circle_i < circle_num; ++circle_i)
        {
            if (circle_sizes[circle_i] < 2)
                continue;
            // Set all vertices as untouched
            bool[] verts = new bool[circle_sizes[circle_i]];
            // Decide a random "step"
            int step = Random.Range(1, circle_sizes[circle_i] / 2);
            int curr_vert = 0;
            while (curr_vert > -1)
            {
                int next_vert = (curr_vert + step)%circle_sizes[circle_i];
                if (!verts[next_vert])
                {
                    // Draw line!
                    float tstep = 1.0f/((pattern[circle_i][curr_vert] - pattern[circle_i][next_vert]).magnitude*pixel_size);
                    for (float t = 0.0f; t < 1.0f; t += tstep)
                    {
                        Vector2 tpos = (Vector2.Lerp(pattern[circle_i][curr_vert], pattern[circle_i][next_vert], t) + Vector2.one*tot_radius)*pixel_size;
                        int[] xpos = { Mathf.FloorToInt(tpos.x), Mathf.FloorToInt(tpos.x), Mathf.CeilToInt(tpos.x), Mathf.CeilToInt(tpos.x) };
                        int[] ypos = { Mathf.FloorToInt(tpos.y), Mathf.CeilToInt(tpos.y), Mathf.FloorToInt(tpos.y), Mathf.CeilToInt(tpos.y) };
                        for (int sq_i = 0; sq_i < 4; ++sq_i)
                        {
                            // Calculate color!
                            Color px_col = circle_color;
                            px_col.a = px_col.a*Mathf.Clamp01(1.0f - (new Vector2(xpos[sq_i], ypos[sq_i]) - tpos).magnitude/2.0f);
                            int px_i = xpos[sq_i] + ypos[sq_i] * circle_side;
                            if (px_i >= 0 && px_i < color_array.Length)
                                color_array[px_i] = px_col;
                        }
                    }
                    verts[next_vert] = true;
                    curr_vert = next_vert;
                }
                else
                {
                    curr_vert = System.Array.FindIndex<bool>(verts, b => b == false);
                }

                //curr_vert = -1;
            }
        }

        circle_drawing.SetPixels(color_array);
        circle_drawing.Apply();

        // Create a sprite
        CircleObject = new GameObject("circle_drawing");
        CircleObject.transform.SetParent(transform, false);
        SpriteRenderer draw_sr = CircleObject.AddComponent<SpriteRenderer>();
        draw_sr.sortingLayerName = "Circle";
        draw_sr.sprite = Sprite.Create(circle_drawing, new Rect(0, 0, circle_side, circle_side), Vector2.one * 0.5f, pixel_size);
    }

    void Candle_OnCandleLit(Candle sender)
    {
        if(correct_sequence[0] == sender)
        {
            sender.CorrectCandle();
            correct_sequence.RemoveAt(0);
            Summoner.sngl.CorrectCandle();
            if(correct_sequence.Count > 0)
            {
                HighlightCandle(correct_sequence[0]);
            }
        }
        else
        {
            int i = correct_sequence.FindIndex(c => c == sender);
            // Remove the candle from the sequence, at this point...
            if (i < 0)
                return; 
            correct_sequence.RemoveAt(i);
            Summoner.sngl.WrongCandleLit();
            sender.IncorrectCandle();
        }

        if (correct_sequence.Count == 0)
        {
            // Also hide highligher
            if (CurrentMarker != null)
                CurrentMarker.SetActive(false);
            Summoner.sngl.Summon();
        }
    }

    void Candle_OnCandleExtinguished(Candle sender)
    {
        // Damn! Gotta put it back in place!
        // Also punish the player

        correct_sequence.Add(sender);
        Summoner.sngl.ExtinguishedCandle();

    }

    void HighlightCandle(Candle next_candle)
    {
        if(CurrentMarker == null)
        {
            CurrentMarker = Instantiate(NextCandlePrefab);    
        }
        CurrentMarker.transform.parent = next_candle.transform;
        CurrentMarker.transform.localPosition = Vector3.zero;
		CurrentMarker.GetComponent<Animator> ().SetTrigger ("zoom_in");
        
    }

    void SummonDemon()
    {
        Instantiate(explosion_prefab, transform.position, transform.rotation);
        float summon_power = Summoner.sngl.SummonQuality;

        for (int i = summons.Length-1; i >= 0; --i)
        {
            print(summons[i].demon_threshold + " " + summon_power);
            if (summons[i].demon_threshold <= summon_power)
            {
                // Do the summoning!
                Instantiate(summons[i].demon_prefab, transform.position, transform.rotation);
                break;
            }
        }

        // Also play an audio clip
        if (demon_voices.Length > 0)
        {
            int clip_i = Random.Range(0, demon_voices.Length);
            AudioSource asource = GetComponent<AudioSource>();
            asource.pitch = 2.0f - summon_power;
            asource.clip = demon_voices[clip_i];
            asource.Play();
        }
    }
}



