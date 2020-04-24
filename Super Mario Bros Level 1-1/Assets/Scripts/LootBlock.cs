using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBlock : MonoBehaviour
{
    public GameObject loot;
    public GameObject after;
    public GameObject coin;
    public GameObject mushroom;

    public bool haveMushroom;

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
        if (collision.gameObject.tag == "Head")
        {
            loot.SetActive(false);
            after.SetActive(true);
            if (haveMushroom)
            {
                mushroom.SetActive(true);
            }
            else
                coin.SetActive(true);
        }
    }
}
