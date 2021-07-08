using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource death;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
        death = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        anim.SetTrigger("death");
        rb.velocity = new Vector2(0,0); //проблему перемещения спрайта не фиксит
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;     //а это должно пофиксить
        death.Play();
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
