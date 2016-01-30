using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemonChargeBar : MonoBehaviour {

    public float transparent_radius = 1.0f;
    public Summoner summoner;
    public PhysicsPlayerControl player;

    Image bar_image, frame_image;
    Animator anim;

	// Use this for initialization
	void Awake () {
        bar_image = GetComponent<Image>();
        frame_image = transform.FindChild("BarFrame").GetComponent<Image>();
        anim = GetComponent<Animator>();
	}

    void Start()
    {
        if (summoner == null)
        {
            summoner = GameObject.FindObjectOfType<Summoner>();
        }
        if (player == null)
        {
            player = GameObject.FindObjectOfType<PhysicsPlayerControl>();
        }
    }

    void Update()
    {
        bar_image.fillAmount = summoner.SummonQuality;
        // Also fade if player is close!
        float pos_ratio = ((Vector2)player.transform.position - (Vector2)Camera.main.ScreenToWorldPoint(transform.position)).magnitude/transparent_radius;
        anim.SetFloat("Blend", Mathf.Clamp01(pos_ratio));

    }

}
