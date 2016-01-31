using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public static Countdown sngl;
    public float interval = 1.0f;
    public Sprite[] sequence;
    public AnimationCurve scale;
    public AnimationCurve alpha;

    public delegate void EndEvent();
    public EndEvent onEnd;

    int i;
    Image im;
    float t;

	// Use this for initialization
	void Awake () {
        im = GetComponent<Image>();
        i = sequence.Length; // Starts paused

        sngl = this;
	}
	
	// Update is called once per frame
	void Update () {

        if (i >= sequence.Length)
            return;

        t += Time.unscaledDeltaTime; // Unaffected by pause!

        if (t > interval)
        {
            t = 0.0f;
            i++;
            if (i < sequence.Length)
            {
                im.sprite = sequence[i];
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
                im.enabled = false;
            }
        }

        if (i < sequence.Length)
        {
            float sc = scale.Evaluate(t / interval);
            im.color = new Color(1, 1, 1, alpha.Evaluate(t / interval));
            transform.localScale = Vector3.one * sc;
        }
	    
	}

    public void Reset()
    {
        i = 0;
        im.enabled = true;
        if (sequence.Length > 0)
            im.sprite = sequence[0];
        t = 0.0f;
    }
}
