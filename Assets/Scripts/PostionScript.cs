using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1,0,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
