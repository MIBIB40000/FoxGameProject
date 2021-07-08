using UnityEngine;

public class Frog : Enemy
{
    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;
    [SerializeField] private float jumpLenght = 10f;
    [SerializeField] private float jumpHeight = 15f;
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

				if (coll.IsTouchingLayers((ground)))
				{
					rb.velocity = new Vector2(-jumpLenght, jumpHeight);
					//state = State.jumping;
					anim.SetBool("jumping", true);
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

				if (coll.IsTouchingLayers((ground)))
				{
					rb.velocity = new Vector2(jumpLenght, jumpHeight);
					//state = State.jumping;
					anim.SetBool("jumping", true);
				}
			}
			else
			{
				facingLeft = true;
			}
		}
	}

}
