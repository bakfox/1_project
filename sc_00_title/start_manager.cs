using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void to_start()
    {
        loadManager.LoadScene_fast("sc_00");
    }
}
