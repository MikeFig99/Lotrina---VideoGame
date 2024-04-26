using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceBackground : MonoBehaviour
{
    private Material material;
    private float offset;
    private Rigidbody2D rb;
    public float speed;
    
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        offset = (rb.velocity.x * 0.1f)* Time.deltaTime * speed;
        material.mainTextureOffset += new Vector2(offset,0f);
    }
}
