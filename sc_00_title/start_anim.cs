using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class start_anim : MonoBehaviour
{
    public GameObject[] obj_meteos;//���׿���

    bool wait_second = false;// �ð� ������ ���߰� 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wait_second == false)
        {
            StartCoroutine("meteos_spead_ve");
        }
        
    }
    IEnumerator meteos_spead_ve()//���׿� ������ ����
    {
        for (int i = 0; i < obj_meteos.Length; i++)
        {
            obj_meteos[i].GetComponent<Rigidbody2D>().AddForce(new Vector3(-60f, -60f, 0f), ForceMode2D.Impulse);
        }  
        yield return new WaitForSeconds(2f);
        wait_second = true;
        StopCoroutine("meteos_spead_ve");
    }

}
