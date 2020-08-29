using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPtalform : MonoBehaviour
{
    public GameObject jumpgb;
    public float jumpPower;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = jumpgb.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.Play("Jump_run Platform");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower));
        }

    }
}
