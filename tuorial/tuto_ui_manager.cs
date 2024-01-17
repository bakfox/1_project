using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuto_ui_manager : MonoBehaviour
{
    public GameObject left_button;
    public GameObject right_button;
    public GameObject[] img_grup;//�̹�����
    public GameObject first_chat;


    bool end_tutorial = false;
    bool start_first = true;// ó�� �Է�
    int now_img = 0;

    sc_00_gameManager gm_temp;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();
    }

    // Update is called once per frame
    void Update()
    {
        if (end_tutorial)
        {
            left_button.SetActive(false);
            right_button.SetActive(false);
            if (Input.anyKeyDown)
            {
                gm_temp.player_first_start = false;
                gm_temp.Save();
                loadManager.LoadScene("sc_01");
            }
        }
    }
    public void left_turn()
    {
        if (now_img <= 0)
        {
            now_img = 0;
        }
        else
            now_img--;
        set_img();
    }
    public void right_turn()
    {
        first_chat.SetActive(false);
        if (now_img >= img_grup.Length-1)
        {
            now_img = img_grup.Length-1;
            end_tutorial = true;
            
        }
        else
            if (start_first)// ó�� Ŭ���� 0���� ���� 
            {
            now_img = 0;
            start_first = false;
            }
        else
            now_img++;
        set_img();
        left_button.SetActive(true);
    }
    void set_img() // 0�ϰ�� 0��°�� �� ������ �ϰ�� �������� �� 
    {
        int i_up = now_img + 1;
        int i_down = now_img - 1;
        Debug.Log(now_img);
        Debug.Log(i_down+"�Դϴ�.");
        if (now_img == 0)
        {
            img_grup[now_img].SetActive(true);
            img_grup[i_up].SetActive(false);
        }
        else if(now_img == img_grup.Length - 1)
        {
            Debug.Log(now_img+"������");
            img_grup[i_down].SetActive(false);
            img_grup[now_img].SetActive(true);
        }
        else
        {
            img_grup[now_img].SetActive (true);
            img_grup[i_up].SetActive(false);
            img_grup[i_down].SetActive(false);
        }
    }
}
