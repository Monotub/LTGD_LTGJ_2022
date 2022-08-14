using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    [SerializeField] Image recastGraphic;

    bool timerActive = false;
    float recastTimer = 2.5f;
    float timer;



    private void Update()
    {
        if (!timerActive) return;

        timer -= Time.deltaTime;
        recastGraphic.fillAmount = timer / recastTimer;

        if(timer <= 0)
        {
            recastGraphic.gameObject.SetActive(false);
            recastGraphic.fillAmount = 1;
            timerActive = false;
        }
    }

    public void StartRecastTimer()
    {
        timerActive = true;
        timer = recastTimer;
        recastGraphic.gameObject.SetActive(true); 
    }

    public bool CanCast() => !timerActive;
}
