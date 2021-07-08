using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	//Start() variables
	private Rigidbody2D rb;
	private Animator anim;
	private Collider2D coll;

	//Inspector variables
	[SerializeField] private LayerMask ground;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float jumpForce = 7f;
	[SerializeField] private int cherriesCount = 0;
	[SerializeField] private Text cherryText;
	[SerializeField] private float hurtForce = 10f;
	[SerializeField] private AudioSource cherry;
	[SerializeField] private AudioSource footstep;

	//FSM
	private enum State { idle, running, jumping, falling, hurt};
	private State state = State.idle;

	private void OnTriggerEnter2D (Collider2D collision) 
	{
		if(collision.tag == "Collectable")
		{
			cherry.Play();
			Destroy(collision.gameObject);
			cherriesCount += 1;
			cherryText.text = cherriesCount.ToString();
		}
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			if(state == State.falling)
			{
				enemy.JumpedOn();
				Jump();
			}
			else
			{
				if(other.gameObject.transform.position.x > transform.position.x)
				{
					rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
					state = State.hurt;
				}
				else
				{
					rb.velocity = new Vector2(hurtForce, rb.velocity.y);
					state = State.hurt;
				}
			}
		}
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
		//footstep = GetComponent<AudioSource>();
	}
	private void Update()
	{
		if(state != State.hurt)
		{
			Movement();
		}
		
		AnimationState();
		anim.SetInteger("state", (int)state);
	}
	private void Movement()
	{
		float hDirection = Input.GetAxis("Horizontal");

		if (hDirection < 0)
		{
			rb.velocity = new Vector2(-speed, rb.velocity.y);
			transform.localScale = new Vector2(-1, 1);
		}
		else if (hDirection > 0)
		{
			rb.velocity = new Vector2(speed, rb.velocity.y);
			transform.localScale = new Vector2(1, 1);
		}

		if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers((ground)))
		{
			Jump();
		}
	}
	private void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		state = State.jumping;
	}
	private void AnimationState()
	{
		if(state == State.jumping)
		{
			if(rb.velocity.y < .1f)
			{
				state = State.falling;
			}
		}

		else if(state == State.falling)
		{
			if (coll.IsTouchingLayers(ground))
			{
				state = State.idle;
			}
		}

		else if(rb.velocity.y < -4f)
		{
			state = State.falling;
		}

		else if (state == State.hurt)
		{
			if(Mathf.Abs(rb.velocity.x) < .1f)
			{ 
				state = State.idle; 
			}
		}

		else if(Mathf.Abs(rb.velocity.x) > 2f)
		{
			state = State.running;
			//FootstepSound();
		}

		else
		{
			state = State.idle;
		}

	}

	private void FootstepSound()
	{
		if (coll.IsTouchingLayers(ground))
		{
			footstep.Play();
		}
	}
}
