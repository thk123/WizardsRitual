using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public static Countdown sngl;
    public float interval = 1.0f;
    //public Sprite[] sequence;
    public string[] text_sequence;
    public AnimationCurve scale;
    public AnimationCurve alpha;
    public Color col;

    public delegate void EndEvent();
    public EndEvent onEnd;

    int i;
    Image im;
    Text txt;
    float t;

	// Use this for initialization
	void Awake () {
        im = GetComponent<Image>();
        txt = GetComponent<Text>();
        i = text_sequence.Length; // Starts paused

        sngl = this;
	}
	
	// Update is called once per frame
	void Update () {

        if (i >= text_sequence.Length)
            return;

        t += Time.unscaledDeltaTime; // Unaffected by pause!

        if (t > interval)
        {
            t = 0.0f;
            i++;
            if (i < text_sequence.Length)
            {
                //im.sprite = sequence[i];
                txt.text = text_sequence[i];
            }
            else
            {
                // Countdown's over!
                if (onEnd != null)
                {
                    onEnd();
                    // And reset the delegate
                    onEnd = null;
                }
                //im.enabled = false;
                txt.enabled = false;
            }
        }

        if (i < text_sequence.Length)
        {
            float sc = scale.Evaluate(t / interval);
            //im.color = new Color(1, 1, 1, alpha.Evaluate(t / interval));
            txt.color = new Color(col.r, col.g, col.b, alpha.Evaluate(t / interval));
            transform.localScale = Vector3.one * sc;
        }
	    
	}

    public void Reset()
    {
        i = 0;
        //im.enabled = true;
        txt.enabled = true;
        if (text_sequence.Length > 0)
        {
            //im.sprite = sequence[0];
            txt.text = text_sequence[0];
        }
        t = 0.0f;
    }
}
