using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            GameManager.Instance.player.Rebound(other.GetContact(0).normal);
            GameManager.Instance.LoseHealth();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseHealth();
        }
    }
}
