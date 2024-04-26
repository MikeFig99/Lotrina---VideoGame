using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireWorkController : MonoBehaviour
{
    private float distance = 10;
    private float finalPosition;
    private float speed = 1f;


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        float r = Random.Range(0,255);
        float g = Random.Range(0,255);
        float b = Random.Range(0,255);
        float a = Random.Range(50,255);
            
        spriteRenderer.color = new Color(r/255,g/255,b/255,a/255);
        
        speed = Random.Range(3, 5);
        distance = Random.Range(3, 8);
        transform.eulerAngles = new Vector3(0f,0f,Random.Range(-45,45));
        finalPosition = transform.position.y + distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < finalPosition)
        {
            transform.Translate( Vector2.up * Time.deltaTime * speed);
        }
        else
        {
            animator.SetBool("explosion",true);
        }
    }

    public void destruction()
    {
        Destroy(gameObject);
    }
}
