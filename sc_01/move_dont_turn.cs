using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_dont_turn : MonoBehaviour
{
    public bool turn_left = false;
    public bool turn_right = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn_left)
        {
            this.transform.rotation = Quaternion.Euler(12f, -13, 0);
        } else if (turn_right)
        {
            this.transform.rotation = Quaternion.Euler(12f, 13, 0);
        }
        else
        this.transform.rotation = Quaternion.Euler(12f,0,0);
    }
}
