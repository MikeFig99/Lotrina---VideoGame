using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncoController : MonoBehaviour
{
    private int direction = -1;
    private float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(-1, 2);
        if(direction == 0 ) Destroy(gameObject);
        speed = Random.Range(1, 4);
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        Detectobject();
        transform.position += new Vector3(direction*speed,0f)*Time.deltaTime;
        Destroy(gameObject,20);
    }
    void Detectobject()
    {
        RaycastHit2D raycastHitWall = Physics2D.Raycast(transform.position + new Vector3(direction,0) ,
            new Vector2(direction, 0), 0.1f, LayerMask.GetMask("Ground"));
        
        if (raycastHitWall == true )
        {
            direction *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.Rebound(other.GetContact(0).normal);
            GameManager.Instance.LoseHealth();
        }
    }
}