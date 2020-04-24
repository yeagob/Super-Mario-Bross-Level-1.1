using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject blue;
    public GameObject ground;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Head")
        {
            if (collision.GetComponentInParent<PlayerMovement>().extraLife)
            {
                Instantiate(particles, transform);
                GetComponent<SpriteRenderer>().enabled =false;
                GetComponent<BoxCollider2D>().enabled = false;
                blue.SetActive(true);
                ground.SetActive(false);
                collision.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                collision.GetComponentInParent<PlayerMovement>().isJumping = false;
            }
        }
    }
}
