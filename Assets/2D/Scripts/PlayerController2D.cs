using System;
using System.Collections;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private float moveH;
    public float jumpForce = 9;
    public float speedMove = 300;
    private Vector3 vel = Vector3.zero;
    public float smooth = 0.2f;
    public Joystick joystick;
    
    private Rigidbody2D rb;
    public Animator animator;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    
    private bool lookRight = true;
    private bool grounded;
    private bool swimming;
    public bool canMove = true;
    public Vector2 reboundVelocity = new Vector2(4,4);

    public AudioClip jumpSound;
    public AudioClip startSound;
    
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,8,false);
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveH = speedMove * joystick.Horizontal;
    }

    void FixedUpdate()
    {
        Falling();
        if (canMove)
        {
            Move(moveH * Time.fixedDeltaTime);
        }
        grounded = isGrounded();
        swimming = Swimming();
    }

    public void Rebound(Vector2 reboundPoint)
    {
        rb.velocity = new Vector2(-reboundVelocity.x * reboundPoint.x,reboundVelocity.y);
    }
    void Move(float move)
    {
        if (joystick.Horizontal != 0) animator.SetBool("isMoving",true);
        else animator.SetBool("isMoving",false);
        
        Vector3 velObj = new Vector2(move, rb.velocity.y);    
        rb.velocity = Vector3.SmoothDamp(rb.velocity,velObj, ref vel,smooth);

        if ((joystick.Horizontal < 0 && lookRight) || (joystick.Horizontal > 0 && !lookRight))
        {
            lookRight = !lookRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center,
            new Vector2(collider.bounds.size.x, collider.bounds.size.y), 0f, Vector2.down, 0.3f,
            LayerMask.GetMask("Ground"));
       
       if(raycastHit.collider != null) animator.SetBool("isGrounded", true);
       else animator.SetBool("isGrounded", false);
       
       return raycastHit.collider != null;
    }

    void Falling()
    {
        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping",false);
            animator.SetBool("isFalling",true);
        }
        else animator.SetBool("isFalling",false);
        
    }
    
    private bool Swimming()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center,
            new Vector2(collider.bounds.size.x, collider.bounds.size.y), 0f, Vector2.down, 0.1f,
            LayerMask.GetMask("Water"));

        if (raycastHit.collider != null)
        {
            animator.SetBool("isSwimming", true);
        }
        else animator.SetBool("isSwimming",false);
        
        return raycastHit.collider != null;
    }
    
    public void Jump()
    {
        if (grounded)
        {
            animator.SetBool("isJumping",true);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SFXManager.Instance.PlayAudio(jumpSound,0.5f);
        }
        //animator.SetBool("isJumping",false);
        
        if(swimming)
        {
            animator.SetBool("isJumping",false);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SFXManager.Instance.PlayAudio(jumpSound,0.5f);
        }
        
    }

    public void JumpForce()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    IEnumerator PlayerHurt()
    {
        Physics2D.IgnoreLayerCollision(7,8,true);
        for (int i = 0; i<=5; i++)
        {
            spriteRenderer.color = new Color(1,0,0);
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = new Color(1,1,1);
            yield return new WaitForSeconds(0.25f);
        }
        Physics2D.IgnoreLayerCollision(7,8,false);
    }
    
    IEnumerator ControlLost()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
}