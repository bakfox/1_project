using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sound_manager : MonoBehaviour
{
    AudioSource this_source;
    public AudioClip main_sd;
    public AudioClip ingame_sd;

    
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        this_source = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    public static sound_manager find_sound_manage()
    {
        if (GameObject.FindGameObjectWithTag("sound").GetComponent<sound_manager>() == null)
        {
            return null;
        }
        else 
        {
            sound_manager sm_temp = GameObject.FindGameObjectWithTag("sound").GetComponent<sound_manager>();
            return sm_temp;
        }
        
    }
    public void ingame_sound_bgm()//인게임 사운드 bgm변경후 재생
    {
        this_source.clip = ingame_sd;
        this_source.Play();
    }
    public void mainmenu_sound_bgm()
    {
        this_source.clip = main_sd;
        this_source.Play();
    }
}
