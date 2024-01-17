using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_1_gameManager : MonoBehaviour
{
    public int stage_select ;// 1 ~7 이런 형식으로 
    public int player_char_indxe;// 플레이어 캐릭터 값
    public GameObject[] player_char;


    public int trash_stack_max;
    public int trash_stack_now = 0;
    public bool trash_end = false;//쓰레기 수집 끝나면 true로

    public float time_limit;
    public float time_now = 0;
    public bool time_over = false;//제한시간 종료 시 true로

    public bool game_start = false;//6초 후 시작.

    public int trap_max;// 함정갯수

    public GameObject obj_add_1_trash;
    public GameObject obj_slash_10_time;

    int reward_gold;
    sc_00_gameManager gm_temp;
    public sc_1_ui_manager ui_temp;

    int i = 0; // 한번으로 제한하기 위한 코드
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();
        Invoke("game_statuse_change",6f);
        Invoke("stage_seting", 0.1f);
        Invoke("char_seting", 0.1f);
    }
    void game_statuse_change()
    {
        Debug.Log("몇초지남");
        game_start = true;
    }
   void char_seting()
    {
        player_char_indxe = gm_temp.player_char_number;
        switch (player_char_indxe)
        {
            case 1:
                player_char[0].SetActive(true);
                break;
            case 2:
                player_char[1].SetActive(true);
                break;
            default:
                player_char[0].SetActive(true);
                break;
        }
    }
    void stage_seting()
    {
        stage_select = gm_temp.player_stage;
            switch (stage_select)
            {
                case 1:
                    trash_stack_max = 20;
                    time_limit = 120f;
                    time_now = time_limit;
                    trap_max = 5;
                    break;
                case 2:
                    trash_stack_max = 30;
                    time_limit = 140f;
                    time_now = time_limit;
                    trap_max = 6;
                    break;
                case 3:
                    trash_stack_max = 40;
                    time_limit = 150f;
                    time_now = time_limit;
                    trap_max = 8;
                    break;
                case 4:
                    trash_stack_max = 50;
                    time_limit = 160f;
                    time_now = time_limit;
                    trap_max = 10;
                    break;
                default:
                    trap_max = 5;
                    trash_stack_max = 20;
                    time_limit = 120f;
                    time_now = time_limit;
                    Debug.Log("스테이지 오류 발생");
                    break;
            }
    }
    public void add_trash()
    {
        StartCoroutine("add_trash_point");
        trash_stack_now++;
        if (trash_stack_max <= trash_stack_now)
        {
            trash_end = true;
        }
    }
    public void slash_now_time()
    {
        StartCoroutine("slash_time");
        ui_temp.time_count -= 10;
    }
    public int compensation_action()
    {
        int gold = 0;
        if (!trash_end)
        {
            gold = 100 * stage_select - (trash_stack_max - trash_stack_now )* 5 * stage_select;// 실패시 디폴트 100 골드에 스테이지 곱하고  못먹은 수치만큼 차감 
            gold = gold + gm_temp.player_gold_percent * (gold / 10);//아래 설명
            reward_gold = gold;
            gm_temp.player_now_gold = reward_gold;
        }
        else
        {
            gold = 300 * stage_select + (trash_stack_now - trash_stack_max) * 10 * stage_select;
            gold = gold + (gm_temp.player_gold_percent * gold / 10);// 상점에 골드 비례 10퍼센트식 증가하면서 보상 더줌 
            reward_gold = gold;
            gm_temp.player_now_gold = reward_gold;
        }
        gm_temp.Save();
        return gold;
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("time_oevr_on",3.5f);
    }
    void time_oevr_on()
    {
        if (time_now <= 0)
        {
            time_over = true;
            sc_1_ui_manager um_temp = this.gameObject.GetComponentInChildren<sc_1_ui_manager>();
            if (i == 0)//한번만 화면에 띄우기 위한 i
            {
                um_temp.time_over_text_on();
                i++;
            }
            Invoke("reward_menu_on",3f);
        }
    }
    void reward_menu_on() 
    {
        sc_1_ui_manager um_temp = this.gameObject.GetComponentInChildren<sc_1_ui_manager>();
        um_temp.time_over_text_off();
        um_temp.rewaard_menu_on();
    }
    IEnumerator add_trash_point()// 먹을때 쓰레기 포인트 증가 이펙트 관리
    {
        obj_add_1_trash.SetActive(true);
        yield return new WaitForSeconds(1);
        obj_add_1_trash.SetActive(false);
        StopCoroutine("add_trash_point");
    }
    IEnumerator slash_time()//함정 맞을때 글자 나온거 관리
    {
        obj_slash_10_time.SetActive(true);
        yield return new WaitForSeconds(1);
        obj_slash_10_time.SetActive(false);
        StopCoroutine("slash_time");
    }
}
