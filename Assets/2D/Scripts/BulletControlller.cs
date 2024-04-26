using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlllerBullet : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( Time.deltaTime * speed * Vector2.right  );
        Destroy(gameObject,5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.Rebound(other.GetContact(0).normal);
            GameManager.Instance.LoseHealth();
            Destroy(gameObject);
        }
    }
}
