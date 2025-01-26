using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//创建animator变量
    private Rigidbody2D rb;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        Debug.Log("setanimation");
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        anim.SetFloat("velocity",  Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
    }





}
