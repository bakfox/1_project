using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash_time_to : MonoBehaviour
{
    sc_1_gameManager gm_temp;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<sc_1_gameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm_temp.slash_now_time();
            Destroy(this.gameObject);
        }
    }
}
