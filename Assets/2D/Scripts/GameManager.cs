using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public PlayerController2D player;

    public AudioClip lossLifeSound;
    public AudioClip gameoverSound;
    public int health = 4;
    private void Awake()
    {
        hud.gameover.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseHealth()
    {
        SFXManager.Instance.PlayAudio(lossLifeSound);
        print("Pierde Vida");
        player.StartCoroutine("PlayerHurt");
        player.StartCoroutine("ControlLost");
        hud.ActiveHealth(health,false);
        health -= 1;
        if (health < 0)
        {
            StartCoroutine("Death");
            Physics2D.IgnoreLayerCollision(7,8,true);
            player.animator.SetBool("isDead",true);
        }
    }

    IEnumerator Death()
    {
        player.canMove = false;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
        SFXManager.Instance.PlayAudio(gameoverSound);
        hud.gameover.SetActive(true);
    }
    public void RegainHealth()
    {
        if(health < 4)
        {
            print("Recupera Vida");
            health += 1;
            hud.ActiveHealth(health,true);
        }
    }
}
