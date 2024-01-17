using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public GameObject player_obj;
    Animator player_anim;
    Rigidbody player_trs;
    public float player_spead; // 플레이어 스피드 골드로 강화 가능 좌우 움직이는 속도임
    sc_1_gameManager gm_temp;
    sc_00_gameManager sm_temp;

    move_dont_turn dont_turn_temp;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<sc_1_gameManager>();
        sm_temp = sc_00_gameManager.find_game_manager();
        player_spead_add(); 
        Invoke("find_char",5.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void find_char()
    {
        player_obj = GameObject.FindGameObjectWithTag("Player");
        player_trs = player_obj.GetComponent<Rigidbody>();
        if (player_obj.GetComponent<Animator>() != null)
        {
            player_anim = player_obj.GetComponent<Animator>();
        }
        dont_turn_temp = player_obj.GetComponent<move_dont_turn>();
    }
    public void touch_left()//왼쪽 클릭 앤 드래그 
    {
        if (gm_temp.game_start)
        {
            dont_turn_temp.turn_left = true;
            dont_turn_temp.turn_right = false;
            player_trs.AddForce(Vector3.left * player_spead);
        }
    }
    public void touch_right()//오른쪽 클릭 앤 드래그 
    {
        if (gm_temp.game_start)
        {
            dont_turn_temp.turn_left = false;
            dont_turn_temp.turn_right = true;
            player_trs.AddForce(Vector3.right * player_spead);
        }
        
    }
    public void dont_touch()//아무 입력 없음
    {
        dont_turn_temp.turn_left = false;
        dont_turn_temp.turn_right = false;
    }
    void player_spead_add()//상점 플레이 속도 합쳐서 계산
    {
        player_spead = player_spead + (sm_temp.player_spead_now * 1.5f); 
    }
}
