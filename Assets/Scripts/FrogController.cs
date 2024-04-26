using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogController : MonoBehaviour
{
    private bool change = false;
    private Animator _animator;
    private Rigidbody rb;

    public float jumpForce = 3;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f,Random.Range(0,360),0f);
        change = true;
        Physics2D.IgnoreLayerCollision(7,8,true);
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (change == true) StartCoroutine("ChangeDirection");
        
    }
    IEnumerator ChangeDirection()
    {
        change = false;
        yield return new WaitForSeconds(Random.Range(2,15));
        transform.eulerAngles = new Vector3(0f,Random.Range(0,360),0f);
        _animator.SetBool("jump",true);
        change = true;
    }
    public void Jump(string text)
    {
        rb.AddForce(transform.up* jumpForce, ForceMode.Impulse);
        rb.AddForce(transform.forward * (jumpForce + 1), ForceMode.Impulse);
        _animator.SetBool("jump",false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("jump",true);
    }
}
