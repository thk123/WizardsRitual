using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Linear_Player_Control: MonoBehaviour
{
	private playerBehaviour m_Character;
    private bool m_Jump;


    private void Awake()
    {
		m_Character = GetComponent<playerBehaviour>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
        // Pass all parameters to the character control script.
		m_Character.Move(h, v);
    }
}

