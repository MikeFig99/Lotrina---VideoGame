using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 1f;
    public float direction = 1f;
    public bool questionFish = false;
    public GameObject questions;
    
    private float finalPosition1;
    private float finalPosition2;
    private bool stop = true;
    private SpriteRenderer spriteRenderer;
    public AudioClip catchSound;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(direction, transform.localScale.y);
        finalPosition1 = transform.position.x + distance;
        finalPosition2 = transform.position.x - distance;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(direction*speed,0f)*Time.deltaTime;
        
        if (transform.position.x > finalPosition1 || transform.position.x < finalPosition2 )
        {
            direction *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        if (questionFish == true && stop == true) StartCoroutine("ChangeColor");
    }
    
    IEnumerator ChangeColor()
    {
        stop = false;
        float r = Random.Range(0, 255);
        float g = Random.Range(0, 255);
        float b = Random.Range(0, 255);
        spriteRenderer.color = new Color(r/255,g/255,b/255);
        yield return new WaitForSeconds(0.25f);
        stop = true;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlayAudio(catchSound);
            print("atrapo un pez");
            Destroy(gameObject);
            GameManager.Instance.RegainHealth();
            if (questionFish == true)
            {
                questions.SetActive(true);
            }
        }
    }
}
