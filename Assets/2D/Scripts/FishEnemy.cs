
using UnityEngine;
using System;

public class FishEnemy : MonoBehaviour
{
   
    public float distance = 5f;
    public float speed = 1f;
    public char axis = 'x';
    public float direction = 1f;
    private float finalPosition1;
    private float finalPosition2;

    // Start is called before the first frame update
    private void Start()
    {
       switch (axis)
       {
           case'x':

       	   transform.localScale = new Vector2(direction, transform.localScale.y);
           finalPosition1 = transform.position.x + distance;
           finalPosition2 = transform.position.x - distance;
	   break;

	   case 'y':
           transform.localScale = new Vector2(transform.localScale.x,direction);
           finalPosition1 = transform.position.y + distance;
           finalPosition2 = transform.position.y - distance;
           break;

       } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
       if(axis=='x')
       {
            transform.position += new Vector3(direction*speed,0f)*Time.deltaTime;
        
            if (transform.position.x > finalPosition1 || transform.position.x < finalPosition2 )
            {
                direction *= -1;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            } 
       }
       if(axis=='y')
       {
            transform.position += new Vector3(0f,direction*speed)*Time.deltaTime;
        
            if (transform.position.y > finalPosition1 || transform.position.y < finalPosition2 )
            {
                direction *= -1;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            } 
       }
   }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            print("atrapo un pez toxico");
            Destroy(gameObject);
	    
	    for(int i=0;i<=5;i++)
	    {
            GameManager.Instance.LoseHealth();
            }
        }
    }
}
