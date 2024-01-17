using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class model_dont_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(0f, 0f, 12.84f);
    }
}
