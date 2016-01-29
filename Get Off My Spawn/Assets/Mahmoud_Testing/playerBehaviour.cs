using System;
using UnityEngine;


public class playerBehaviour : MonoBehaviour
{
	[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.

	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}


	private void FixedUpdate()
	{

	}


	public void Move(float movex, float movey)
	{
		// If crouching, check to see if the character can stand up





		// Move the character
		m_Rigidbody2D.velocity = new Vector2(movex*m_MaxSpeed, movey*m_MaxSpeed);



	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
