using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sc_1_ui_manager : MonoBehaviour
{
    public GameObject obj_text_go;
    public GameObject obj_text_ready;
    public GameObject obj_reward_menu;
    public GameObject obj_text_time_over;
    public GameObject obj_text_success;
    public GameObject obj_text_fail;

    public Image img_time;
    public float time_count;

    public TextMeshProUGUI text_goal;
    public TextMeshProUGUI text_reward;
    

    sc_1_gameManager gm_temp;
    sc_00_gameManager save_temp;
    public sound_controller sc_temp;

    bool end_reward_once = true;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<sc_1_gameManager>();
        save_temp = sc_00_gameManager.find_game_manager();
        ready_text_on();
        Invoke("set_time_count",6f); 
    }
    void set_time_count()
    {
        time_count = gm_temp.time_limit;
    }
    // Update is called once per frame
    void Update()
    {
        goal_text();
        Invoke("time_set",6f);
        if (gm_temp.time_over && end_reward_once)
        {
            change_result_text();
            change_clear_text();
        }
    }
    void goal_text()
    {
        text_goal.SetText(gm_temp.trash_stack_now + ""+ "/"+""+gm_temp.trash_stack_max);
    }
    void time_set()
    {
        gm_temp.time_now = time_count;
        float time_temp = time_count / gm_temp.time_limit;
        img_time.fillAmount = time_temp;
        time_count -= Time.deltaTime*1f;
    }
    public void rewaard_menu_on()
    {
        obj_reward_menu.SetActive(true);
    }
    public void rewaard_menu_off()
    {
        obj_reward_menu.SetActive(false);
    }
    public void ready_text_on()
    {
        obj_text_ready.SetActive(true);
        StartCoroutine("change_ready_text");
    }
    public void time_over_text_on()
    {
        obj_text_time_over.SetActive(true);
    }
    public void time_over_text_off()
    {
        obj_text_time_over.SetActive(false);
    }
    public void ready_text_off()
    {
        StopCoroutine("change_ready_text");
        Image text_temp = obj_text_go.GetComponent<Image>();
        text_temp.color = new Color(text_temp.color.r, text_temp.color.g, text_temp.color.b, 255f);
        obj_text_go.SetActive(false);
    }
    IEnumerator change_ready_text()//6초후 고로 텍스트 변경 
    {
        yield return new WaitForSeconds(5.5f);
        obj_text_go.SetActive(true);
        obj_text_ready.SetActive(false);
        Image text_temp = obj_text_go.GetComponent<Image>();
        yield return new WaitForSeconds(0.5f);
        ready_text_off();
    }
    public void go_home()
    {
        sc_temp.sm_temp.mainmenu_sound_bgm();
        save_temp.Save();
        loadManager.LoadScene("sc_00"); 
    }
    public void retry()
    {
        loadManager.LoadScene_fast("sc_01");
    }
    void change_result_text()
    {
        int gold_index = gm_temp.compensation_action();
        Debug.Log(gold_index);
        text_reward.SetText(gold_index+""+"G");
        end_reward_once = false;
    }
    void change_clear_text()
    {
        if (gm_temp.trash_end)
        {
            obj_text_success.SetActive(true);
        } else
            obj_text_fail.SetActive(true);
    }
}
