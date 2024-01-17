using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spownManager : MonoBehaviour
{
    public GameObject[] map_obj_L;
    public GameObject[] map_obj_R;

    sc_1_gameManager gameMg_temp;

    public GameObject[] trab_obj;
    public GameObject[] trash_obj;
    public bool end_game = false;
    // Start is called before the first frame update
    void Start()
    {
        gameMg_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<sc_1_gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        end_game = gameMg_temp.time_over;
    }
    private void FixedUpdate()
    {
        StartCoroutine("make_earth");
        if (gameMg_temp.game_start && !end_game)
        {
            StartCoroutine("make_trash");
            StartCoroutine("make_trap");
        }
    }
    IEnumerator make_earth()
    {
        yield return new WaitForSeconds(3f);
        int i_temp = random_index();
        int i_temp_2 = random_index();
        make_map_obj(i_temp , i_temp_2);
        
    }

    IEnumerator make_trash()
    {
        float time_temp = gameMg_temp.time_limit / (gameMg_temp.trash_stack_max + 11);
        Debug.Log(time_temp + " 쿨타임");
        yield return new WaitForSeconds(time_temp);
        int i_temp = random_trahs_index();
        make_trash_obj(i_temp);
    }
    IEnumerator make_trap()
    {
        float time_temp = gameMg_temp.time_limit / (gameMg_temp.trap_max);
        Debug.Log(time_temp + " 쿨타임");
        yield return new WaitForSeconds(time_temp);
        int i_temp = random_trap_index();
        make_trap_obj(i_temp);
    }

    int random_index()
    {
        int i = 0;
        i = Random.Range(0,3);
        return i;
    }

    int random_trahs_index()//쓰레기 위치 정하기
    {
        int i = 0;
        i = Random.Range(0, 3);
        return i;
    }
    int random_trap_index()//트랩 위치 정하기 예시 0 왼쪽 1 오른쪽 
    {
        int i = 0;
        i = Random.Range(0, 2);
        return i;
    }

    void make_map_obj(int i , int i_2)//i = R i_2 = L입니다 
    {
        GameObject temp_game_obj_L = Instantiate(map_obj_L[i_2]);
        GameObject temp_game_obj_R = Instantiate(map_obj_R[i]);
        StopCoroutine("make_earth");
    }

    void make_trash_obj(int i)
    {
        GameObject temp_trash_obj = Instantiate(trash_obj[i]);
        StopCoroutine("make_trash");
    }
    void make_trap_obj(int i)
    {
        GameObject temp_trap_obj = Instantiate(trab_obj[i]);
        StopCoroutine("make_trap");
    }
}
