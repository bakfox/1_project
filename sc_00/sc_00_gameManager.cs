using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class sc_00_gameManager : MonoBehaviour
{
    public GameObject obj_end_menu;

    public int player_char_number = 1;//1~2까지 현재 2캐릭.
    public int player_now_gold = 0;
    public int player_stage = 1;//기본 1 스테이지 
    public int player_stage_max = 4;//4 스테이지 최대 현재 
    public int player_max_energy = 12;//에너지 최대치
    public int player_now_energy = 12;
    public bool player_add_energy_today = false; // 받아가면 true 변경
    public bool player_scedon_unlack = false; // 2번째 캐릭터 개방하면 true 로 변경
    public int player_gold_percent = 0 ;//골드 퍼센트 스텍
    public int player_spead_now = 0; //상점에서 구매하는 스피드 스텍 
    public bool player_first_start = true;

    public DateTime player_last_data = DateTime.Now;
    public TimeSpan player_timespan;

    string data_path;//저장 장소 awake에서 초기화

    public save_user_data save_temp = new save_user_data();

    GameObject last_click_obj;
    // Start is called before the first frame update
    private void Awake()
    {
        data_path = Application.persistentDataPath + "/" + "save_user_data" + ".json";
    }
    void Start()
    {
        
        Debug.Log(player_now_energy);
        Load();
        now_add_energy();
        Save();
        Debug.Log(player_last_data);
    }
    public void now_add_energy()
    {
        
        DateTime player_data_temp;
        DateTime now;
        now = DateTime.Now;
        player_data_temp = Convert.ToDateTime(player_last_data);
        player_timespan = now - player_data_temp;
        Debug.Log(player_timespan+"입니다.");
        if ((float)player_timespan.TotalSeconds / 3600 >= 1f)
        {
            player_add_energy_today = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            obj_end_menu.SetActive(true);
        }
    }
    public void game_off_true()
    {
        Save();
        Application.Quit();
    }
    public void game_off_false()
    {
        obj_end_menu.SetActive(false);
    }
    // 불러오기
    public save_user_data Load()
    {
        Debug.Log(data_path);
        save_user_data save_temp = new save_user_data();
        if (!File.Exists(data_path))
        {
            Save();
        }
        if (File.Exists(data_path))
        {
            string json = File.ReadAllText(data_path);

            byte[] data =  System.Convert.FromBase64String(json);
            string j_data = System.Text.Encoding.UTF8.GetString(data);

            save_temp = JsonUtility.FromJson<save_user_data>(j_data);
            
        }
        player_char_number = save_temp.user_char;
        player_now_gold = save_temp.user_gold;
        player_stage = save_temp.user_stage;
        player_max_energy = save_temp.user_max_energy;
        player_now_energy = save_temp.user_now_energy;
        player_add_energy_today = save_temp.user_add_energy_today;
        player_scedon_unlack = save_temp.user_scedon_unlack;
        player_gold_percent = save_temp.user_gold_percent;
        player_spead_now = save_temp.user_spead_now;
        player_first_start = save_temp.user_first_start;
        Debug.Log(save_temp.user_last_data);
        if (save_temp.user_last_data == "")
        {
            player_last_data = DateTime.Now;
        }
        else
            player_last_data = DateTime.ParseExact(save_temp.user_last_data, "yyyy-MM-dd HH:mm:ss", null);
        Debug.Log(player_last_data+"오류 없습니다.");
        Debug.Log(save_temp.user_now_energy + "받아옴");
        return save_temp;
    }
    //저장
    public void Save()
    {
        save_temp.user_gold = player_now_gold;
        save_temp.user_char = player_char_number;
        save_temp.user_stage = player_stage;
        save_temp.user_max_energy = player_max_energy;
        save_temp.user_add_energy_today = player_add_energy_today;
        save_temp.user_scedon_unlack = player_scedon_unlack;
        save_temp.user_spead_now = player_spead_now;
        save_temp.user_gold_percent = player_gold_percent;
        save_temp.user_first_start = player_first_start;

        Debug.Log(player_add_energy_today+"상태");
        if (player_add_energy_today == false)
        {
            if (player_now_energy < player_max_energy)
            {
                DateTime datetime_temp = DateTime.Now;
                save_temp.user_last_data = datetime_temp.ToString("yyyy-MM-dd HH:mm:ss");
                Debug.Log(save_temp.user_last_data+"입니다");
                player_now_energy += 10;
                if (player_now_energy > player_max_energy)
                {
                    player_now_energy = player_max_energy;
                }
                player_add_energy_today = true;
                save_temp.user_now_energy = player_now_energy;
            }
        }
        else
        {
            save_temp.user_now_energy = player_now_energy;
            save_temp.user_last_data = player_last_data.ToString("yyyy-MM-dd HH:mm:ss");
        }
            

        
        string json = JsonUtility.ToJson(save_temp);
        Byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        string j_data = System.Convert.ToBase64String(data);

        File.WriteAllText(data_path, j_data);

    }
    public static sc_00_gameManager find_game_manager() // 게임매니저 받아오는 함수
    {
        sc_00_gameManager gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<sc_00_gameManager>();
        return gm_temp;
    }
}
[System.Serializable]
public class save_user_data
{
    public int user_gold = 0;
    public int user_char = 1; // 1 기본 ~ 2 다른캐릭 현재 2까지만 있음..
    public int user_stage = 1; // 1스테이지 ~ 2스테이지 
    public int user_max_energy = 12;
    public int user_now_energy = 11;
    public bool user_add_energy_today = false;

    public bool user_scedon_unlack = false;// 2번째 캐릭터 언락

    public int user_gold_percent;
    public int user_spead_now;

    public string user_last_data;// 그날 처음 플레이한 시간 기준.

    public bool user_first_start = true;// 처음 시작시 튜토리얼
}
