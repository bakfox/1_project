using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_left_wall : MonoBehaviour
{
    float fush_power = -0.15f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f,0f,fush_power));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("end_line"))
        {
            Debug.Log("끝나는 곳");
            Destroy(this.gameObject);
        }
    }
}
