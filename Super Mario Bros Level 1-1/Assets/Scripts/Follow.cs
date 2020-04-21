using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x > transform.position.x)
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            
    }
}
