using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class check_button : MonoBehaviour , IPointerEnterHandler
{
    public shop_ui_manager shmg_temp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        shmg_temp.click_button = this.gameObject;
    }
}
