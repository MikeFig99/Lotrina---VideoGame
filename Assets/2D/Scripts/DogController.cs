using System.Collections;
using UnityEngine;


public class DogControlles : MonoBehaviour
{
    public float speedMove = 3;
    
    private Animator animator;
    
    public float timeToHowl = 10;
    public float direction = 1;
    private bool biting = false;
    private bool canMove = true;
    
    public AudioClip howlSound;
    public AudioClip biteSound;
    private AudioSource audioSource;
    
    void Start()
    {
        transform.localScale = new Vector2(direction, transform.localScale.y);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (timeToHowl <= 0)
        {
            timeToHowl = 10;
            canMove = false;
            StartCoroutine(Howl());
        }
        
        if (canMove) Move();
        
        timeToHowl-= Time.deltaTime;
        
    }
    void Move()
    {
        Detectobject();
        transform.position += new Vector3(direction*speedMove,0f)*Time.deltaTime;
    }

    void Detectobject()
    {
        RaycastHit2D raycastHitWall = Physics2D.Raycast(transform.position + new Vector3(direction, 0),
            new Vector2(direction, 0), 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHitPlayer = Physics2D.Raycast(transform.position + new Vector3(direction, 0),
            new Vector2(direction, 0), 1f, LayerMask.GetMask("Player"));
        
        if (raycastHitWall == true )
        {
            direction *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        
        if (raycastHitPlayer == true && biting == false)
        {
            biting = true;
            StartCoroutine(Attack());
            timeToHowl = 10;
        }
    }
    IEnumerator Attack()
    {
        animator.SetBool("Attack",true);
        audioSource.PlayOneShot(biteSound,0.5f);
        yield return new WaitForSeconds(1);
        biting = false;
        animator.SetBool("Attack", false);
    }
    
    IEnumerator Howl()
    {
        animator.SetBool("Howl",true);
        audioSource.PlayOneShot(howlSound);
        yield return new WaitForSeconds(2);
        canMove = true;   
        animator.SetBool("Howl", false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            if( animator.GetBool("Attack"))
            {
                GameManager.Instance.player.Rebound(other.GetContact(0).normal);
                GameManager.Instance.LoseHealth();
            }
            else
            {
                GameManager.Instance.player.Rebound(other.GetContact(0).normal);
                GameManager.Instance.player.animator.SetBool("isJumping",true);
            }
        }
    }

}

