using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buy_manager : MonoBehaviour
{
    sc_00_gameManager gm_temp;

    public GameObject obj_buy; // ����� ����� ������Ʈ
    public GameObject obj_you_nead_more_gold;

    public GameObject obj_second_char;

    int nead_gold;
    // Start is called before the first frame update
    void Start()
    {
        gm_temp = sc_00_gameManager.find_game_manager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buy_second_char()// �ι�° ĳ���� ��� Ŭ�� 
    {
        int nead_second_gold = 1000;
        if (gm_temp.player_now_gold >= nead_second_gold)
        {
            nead_gold = nead_second_gold;
            obj_buy.SetActive(true);
        }
        else
            StartCoroutine("you_nead_more_gold");
    }
    public void yes_plesa()//�춧
    {
        int nead_gold_temp = buy_char();
        if (nead_gold_temp <= gm_temp.player_now_gold)
        {
            gm_temp.player_now_gold = gm_temp.player_now_gold - nead_gold_temp;
            gm_temp.player_scedon_unlack = true;
            gm_temp.Save();
            obj_buy.SetActive(false);
        }
        else
        {
            StartCoroutine("you_nead_more_gold");
            obj_buy.SetActive(false);
        }
    }public void no_plesa()//�ź�
    {
        obj_buy.SetActive(false);
    }
    public int buy_char()// ĳ���� �ʿ��� �ݾ� ����.
    {
        int gold_index = 0;
        gold_index = nead_gold;
        return gold_index;
    }
    IEnumerator you_nead_more_gold()
    {
        obj_you_nead_more_gold.SetActive(true);
        yield return new WaitForSeconds(1f);
        obj_you_nead_more_gold.SetActive(false);
        StopCoroutine("you_nead_more_gold");
    }
    public void nead_more_gold(int i)//��� ������ i �� ��� ��𼭵� �޾ƿ��� 
    {
        if (gm_temp.player_now_gold < i)
        {
            StartCoroutine("you_nead_more_gold");
        }
    }
}
