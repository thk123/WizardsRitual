using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndText : MonoBehaviour {

    public static EndText sngl;

    Text my_text, child_text;

    void Awake()
    {
        sngl = this;
        my_text = GetComponent<Text>();
        child_text = transform.FindChild("Text").GetComponent<Text>();
    }

    public void Show(string T)
    {
        my_text.text = T;
        my_text.enabled = true;
        child_text.enabled = true;
    }

    public void Hide()
    {
        my_text.enabled = false;
        child_text.enabled = false;
    }


}
