using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obj_contor_manager : MonoBehaviour
{
    public GameObject obj_trun;
    public GameObject obj_char_1;
    public GameObject obj_char_2;
    public GameObject obj_char_1_light;
    public GameObject obj_char_2_light;

    public GameObject[] obj_chr_btn_hiden; // 0부터 순서대로

    [SerializeField] Animator anim_chr_1;//1캐릭 애니메이션
    [SerializeField] Animator anim_chr_2;//2캐릭 애니메이션


    sc_00_gameManager gm_temp;

    [Range(-360, 360)] float rotaion_obj = 0; // 돌아간 량;


    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();
        anim_chr_1 = obj_char_1.GetComponent<Animator>();
        anim_chr_2 = obj_char_2.GetComponent<Animator>();
        chang_start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void chang_start()
    {
        switch (gm_temp.player_char_number)
        {
            case 1:
                change_char_btn_1();
                break;
            case 2:
                change_char_btn_2();
                break;
            default:
                change_char_btn_1();
                break;
        }
    }
    public void change_char_btn_1()//캐릭터가 1번일 경우
    {
        obj_char_1.SetActive(true);
        obj_char_2.SetActive(false);
        obj_chr_btn_hiden[0].SetActive(true);
        obj_chr_btn_hiden[1].SetActive(false);
        obj_char_1_light.SetActive(true);
        obj_char_2_light.SetActive(false);
        gm_temp.player_char_number = 1;
        rotaion_obj = 0;
        obj_trun.transform.rotation = Quaternion.Euler(0, rotaion_obj, 0);

    }
    public void change_char_btn_2()
    {
        if (gm_temp.player_scedon_unlack == true)
        {
            obj_char_1.SetActive(false);
            obj_char_2.SetActive(true);
            obj_chr_btn_hiden[0].SetActive(false);
            obj_chr_btn_hiden[1].SetActive(true);
            obj_char_1_light.SetActive(false);
            obj_char_2_light.SetActive(true);
            gm_temp.player_char_number = 2;
            rotaion_obj = 0;
            obj_trun.transform.rotation = Quaternion.Euler(0, rotaion_obj, 0);
        }
    }
    public void drag_screan_left()
    {
        rotaion_obj--;
        obj_trun.transform.rotation = Quaternion.Euler(0, rotaion_obj, 0);
    }
    public void drag_screan_right()
    {
        rotaion_obj++;
        obj_trun.transform.rotation = Quaternion.Euler(0, rotaion_obj, 0);
    }
}
/*
    public void touch_screan()
    {
        if (gm_temp.player_char_number == 1)
        {
            anim_chr_1.SetTrigger("touch_true");
        }
        else if (gm_temp.player_char_number == 2)
        {
            anim_chr_2.SetTrigger("touch_true");
        }
    }
}
*/
