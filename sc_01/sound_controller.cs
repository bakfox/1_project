using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_controller : MonoBehaviour
{
    public sound_manager sm_temp;

    // Start is called before the first frame update
    void Start()
    {
        sm_temp = sound_manager.find_sound_manage();
        sm_temp.ingame_sound_bgm();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
