using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] lifes;
    public GameObject gameover;
    public AudioClip lifeupSound;
    public GameObject menuPause;
    private void Start()
    {
        menuPause.SetActive(false);
        lifes[0].SetActive(false);
        lifes[1].SetActive(false);
        lifes[2].SetActive(false);
        lifes[3].SetActive(false);
        lifes[4].SetActive(false);
        GameManager.Instance.health = 0;
        StartCoroutine(FullLife());
    }

    IEnumerator FullLife()
    {
        for(int i = 0 ;i <= GameManager.Instance.health; i++)
        {
            yield return new WaitForSeconds(0.25f);
            SFXManager.Instance.PlayAudio(lifeupSound);
            GameManager.Instance.RegainHealth();
        }
    }
    public void ActiveHealth(int indice,bool flag)
    {
        
        lifes[indice].SetActive(flag);
    }

    public void Pause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }
    
}
