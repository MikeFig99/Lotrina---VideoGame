using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public bool fly = false;
    public float distance = 3;
    public float speed = 5;
    public int direction = 1;
    
    public bool eat = false;
    public bool idle = false;


    public float finalPosition1,finalPosition2;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        transform.localScale = new Vector2(direction, transform.localScale.y);
        finalPosition1 = transform.localPosition.x + distance;
        finalPosition2 = transform.localPosition.x - distance;
        if (eat) _animator.SetBool("eat",true);
        if (idle) _animator.SetBool("idle",true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            transform.Translate( Time.deltaTime * speed * -direction * Vector2.right );
            
            if (transform.localPosition.x > finalPosition1 || transform.localPosition.x < finalPosition2)
            {
                direction *= -1;
                transform.localScale *= new Vector2(-1,1);
            }
        }
    }
    
    public void Eat()
    {
        if(Random.Range(0,2) == 0)transform.localScale *= new Vector2(-1,1);
    }
}
