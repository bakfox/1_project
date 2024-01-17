using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sc_00_ui_manager : MonoBehaviour
{
    public TextMeshProUGUI text_gold_now;
    public TextMeshProUGUI text_now_energy;
    public GameObject obj_stage_back;
    public TextMeshProUGUI text_stage_now;
    public GameObject obj_nead_more_hart;

    sc_00_gameManager gm_temp;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();

        text_stage_now.SetText(gm_temp.player_stage + "" + "STAGE");// 현재 스테이지 표시 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text_gold_now.SetText(gm_temp.player_now_gold+"");
        text_now_energy.SetText(gm_temp.player_now_energy + ""+"/"+""+gm_temp.player_max_energy);
    }
    public void go_ingame()// 게임 시작
    {
        if (gm_temp.player_now_energy > 0)
        {
            gm_temp.player_now_energy--;
            gm_temp.Save();
            if (gm_temp.player_first_start == true)
            {
                loadManager.LoadScene_fast("tutorial");
            }else
            loadManager.LoadScene("sc_01");
        }
        else
            StartCoroutine("nead_more_hart");
    }
    public void go_shop()
    {
        gm_temp.Save();
        loadManager.LoadScene_fast("sc_00_shop");
       
    }
    public void add_stage()//스테이지 버튼 누르면 올라가고 max보다 같더나 크면 max로 변경 
    {
        gm_temp.player_stage++;
        if (gm_temp.player_stage >= gm_temp.player_stage_max)
        {
            gm_temp.player_stage = gm_temp.player_stage_max;
            Debug.Log(gm_temp.player_stage);
            
        }
        text_stage_now.SetText(gm_temp.player_stage + "" + "STAGE");
    }
    public void subtract_stage()//스테이지 버튼 누르면 빼고 0이하로 내려가면 1로 고정
    {
        gm_temp.player_stage--;
        if (gm_temp.player_stage <= 0)
        {
            gm_temp.player_stage = 1;
            
        }
        text_stage_now.SetText(gm_temp.player_stage + "" + "STAGE");
    }
    public void gold_test_1000()// test용
    {
        gm_temp.player_now_gold += 1000;
        gm_temp.player_now_energy += 10;
        gm_temp.Save();
    }
    IEnumerator nead_more_hart()
    {
        obj_nead_more_hart.SetActive(true);
        yield return new WaitForSeconds(1f);
        obj_nead_more_hart.SetActive(false);
        StopCoroutine("nead_more_hart");
    }
    public void go_credit()
    {
        loadManager.LoadScene_fast("sc_00_credit");
    }
}
