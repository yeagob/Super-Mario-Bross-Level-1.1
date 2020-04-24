using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Añadido a posteriori
        if(collision.contacts[0].normal == Vector2.right || collision.contacts[0].normal == Vector2.left)
            speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Feet")
        {
            this.enabled = false;
            GetComponent<Animator>().Play("Die");
            gameObject.tag = "Untagged";
            Destroy(gameObject, 1);
            collision.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 300));
        }
    }
}
