using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class shop_ui_manager : MonoBehaviour
{
    public TextMeshProUGUI text_gold_now;
    public TextMeshProUGUI text_spead_now;
    public TextMeshProUGUI text_spead_next;
    public TextMeshProUGUI text_gold_percent_now;//골드 퍼센트 업그레이드 필요한 량 
    public TextMeshProUGUI text_gold_percent_next;//스피드 업그레이드 필요한 량 

    public GameObject[] obj_upgrade_img;//0이 골드 1이 스피드

    public GameObject obj_nead_gold;
    public GameObject obj_nead_spead;
    public GameObject upgrade_max;
    public GameObject obj_second_char;

    buy_manager bm_temp;
    sc_00_gameManager gm_temp;

    int gold_i = 0;// 골드업글에 필요한 량
    int spead_i = 0;// 스피드 업글에 필요한 량

    public GameObject click_button;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();
        bm_temp = gameObject.GetComponentInChildren<buy_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("reset_ui");

        char_unlak();
    }

    public void go_home()
    {
        gm_temp.Save();
        loadManager.LoadScene_fast("sc_00");
    }
    public void up_button_upgrade()
    {
        Debug.Log(this.gameObject.name);
        if (click_button.name == "gold_upgrade")
        {
            obj_upgrade_img[0].SetActive(false);
            obj_nead_gold.SetActive(true);

            TextMeshProUGUI gold_temp = obj_nead_gold.GetComponent<TextMeshProUGUI>();

            gold_i = upgrade_need_gold(gm_temp.player_gold_percent);

            if (gold_i == 0)
            {
                gold_temp.SetText("MAX");
            }
            else
                gold_temp.SetText("" + gold_i + "");
        }
        else if (click_button.name == "spead_upgrade")
        {
            obj_upgrade_img[1].SetActive(false);
            obj_nead_spead.SetActive(true);

            TextMeshProUGUI spead_temp = obj_nead_spead.GetComponent<TextMeshProUGUI>();

            spead_i = upgrade_need_gold(gm_temp.player_spead_now);

            if (spead_i == 0)
            {
                spead_temp.SetText("MAX");
            }
            else
                spead_temp.SetText("" + spead_i + "");
        }else
            return;
    }
    public void exit_button_upgrade()//버튼 나가면
    {
            obj_upgrade_img[0].SetActive(true);
            obj_nead_gold.SetActive(false);
            obj_upgrade_img[1].SetActive(true);
            obj_nead_spead.SetActive(false);

    }
    public void upgrade_click()//업그레이드 클릭시 
    {
        if (click_button.name == "gold_upgrade")
        {
            gold_i = upgrade_need_gold(gm_temp.player_gold_percent);
            if (gold_i != 0)
            {
                if (gm_temp.player_now_gold < gold_i)
                {
                    bm_temp.nead_more_gold(gold_i);
                }
                else
                {
                    gm_temp.player_now_gold = gm_temp.player_now_gold - gold_i;
                    gm_temp.player_gold_percent++;
                    gm_temp.Save();
                }
            }
            else
                StartCoroutine("upgrade_max_on");  
        }
        else if (click_button.name == "spead_upgrade")
        {
            spead_i = upgrade_need_gold(gm_temp.player_spead_now);
            if (spead_i != 0)
            {
                if (gm_temp.player_now_gold < spead_i)
                {
                    bm_temp.nead_more_gold(spead_i);
                }
                else
                {
                    gm_temp.player_now_gold = gm_temp.player_now_gold - spead_i;
                    gm_temp.player_spead_now++;
                    gm_temp.Save();
                }
            }
            else
                StartCoroutine("upgrade_max_on");
        }
        else
            return;
    }
    public void ui_setting()
    {
        text_gold_now.SetText("" + gm_temp.player_now_gold + "");
        float spead_i = 15f + (gm_temp.player_spead_now * 1.5f);
        float spead_n = 15f + (gm_temp.player_spead_now + 1) * 1.5f;
        text_spead_now.SetText("now : " + spead_i+"");
        text_spead_next.SetText("next : " + spead_n + "");
        int percent_i = 10 * (gm_temp.player_gold_percent);
        int percent_n = 10 * (gm_temp.player_gold_percent + 1);// 다음꺼 효과 표현 
        text_gold_percent_now.SetText("now : "+ percent_i+"%");
        text_gold_percent_next.SetText("next : " + percent_n+"%");
        StopCoroutine("reset_ui");
    }
    IEnumerator upgrade_max_on()
    {
        upgrade_max.SetActive(true);
        yield return new WaitForSeconds(1f);
        upgrade_max.SetActive(false);
        StopCoroutine("upgrade_max_on");
    }
    IEnumerator reset_ui()
    {
        yield return new WaitForFixedUpdate();
        ui_setting();
    }
    int upgrade_need_gold(int i)
    {
        int gold_indext = 0;
        switch (i)
        {
            case 0:
                gold_indext = 100;
                break;
            case 1:
                gold_indext = 250;
                break;
            case 2:
                gold_indext = 500;
                break;
            case 3:
                gold_indext = 750;
                break;
            case 4:
                gold_indext = 1000;
                break;
            case 5:
                gold_indext = 2000;
                break;
            default:
                gold_indext = 0;
                break;
        }

        return gold_indext;
    }
    void char_unlak()// 캐릭터 늘어날수록 밑에 추가
    {
        if (gm_temp.player_scedon_unlack)
        {
            obj_second_char.SetActive(false);
        }
    }
}
