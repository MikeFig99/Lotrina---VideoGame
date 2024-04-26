using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float distanceAttack = 15;
    public float visionAngle = 130f;
    public float speed = 2f;
    public float cadency = 2f;
    public bool move = true;
    private bool isMove = true;

    public Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject bullet;
    
    private bool detected = false;
    private bool canShoot = true;

    public AudioClip shotSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (move && isMove) Move();
        else animator.SetBool("isMove",false);
        
        Vector3 playerVector = player.position - transform.position;
        transform.GetChild(0).right = playerVector;
        
        if (Vector3.Angle(transform.right, playerVector.normalized) < visionAngle / 2 && playerVector.magnitude < distanceAttack)
        {
            isMove = false;
            if (canShoot)
            {
                animator.SetBool("isAttack",true);
                print("Disparo");
                StartCoroutine(Attack());
                canShoot = false;
            }
            detected = true;
        }
        else
        {
            animator.SetBool("isAttack",false);
            isMove = true;
            detected = false;
        }
    }

    void Move()
    {
        animator.SetBool("isMove",true);
        RaycastHit2D platform = Physics2D.Raycast(transform.GetChild(0).position, Vector2.down, 2);
        rb.velocity = new Vector2(speed,rb.velocity.y);
        if (!platform)
        {
            transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y + 180f,0f);
            speed *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        float halfVisionAngle = visionAngle / 2;
        
        Vector2 p1 = PointForAngle(halfVisionAngle), p2 = PointForAngle(-halfVisionAngle);
        
        Gizmos.color = detected ? Color.green : Color.red; 
        Gizmos.DrawLine(transform.position,(Vector2)transform.position + p1);
        Gizmos.DrawLine(transform.position,(Vector2)transform.position + p2);
    }

    Vector2 PointForAngle(float angle)
    {
        return transform.TransformDirection(new Vector2(math.cos(angle * Mathf.Deg2Rad),math.sin(-angle*Mathf.Deg2Rad))*distanceAttack) ;
    }

    IEnumerator Attack()
    {
        SFXManager.Instance.PlayAudio(shotSound,0.4f);
        Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
        yield return new WaitForSeconds(cadency);
        canShoot = true;
    }
}