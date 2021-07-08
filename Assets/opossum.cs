using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum : Enemy
{
	[SerializeField] private float leftCap;
	[SerializeField] private float rightCap;
	[SerializeField] private LayerMask ground;

	private bool facingLeft = true;

	//private Animator anim;
	private Collider2D coll;
	//private Rigidbody2D rb;

	protected override void Start()
	{
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
	}
	private void Update()
	{
		if (anim.GetBool("jumping"))
		{
			if (rb.velocity.y < .1)
			{
				anim.SetBool("falling", true);
				anim.SetBool("jumping", false);
			}
		}
		if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
		{
			anim.SetBool("falling", false);
		}
	}
	private void Move()
	{
		if (facingLeft)
		{
			if (transform.position.x > leftCap)
			{
				if (transform.localScale.x != 1)
				{
					transform.localScale = new Vector3(1, 1);
				}
			}
			else
			{
				facingLeft = false;
			}
		}

		else
		{
			if (transform.position.x < rightCap)
			{
				if (transform.localScale.x != -1)
				{
					transform.localScale = new Vector3(-1, 1);
				}
			}
			else
			{
				facingLeft = true;
			}
		}
	}

}
