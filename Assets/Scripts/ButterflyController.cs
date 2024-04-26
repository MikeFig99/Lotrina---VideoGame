using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    private bool change = false;
    public float speed = 3; 
    void Start()
    {
        transform.eulerAngles = new Vector3(0f,Random.Range(0,360),0f);
        change = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (change == true) StartCoroutine("ChangeDirection");
        transform.Translate(Time.deltaTime * speed * Vector3.forward );
            
    }

    IEnumerator ChangeDirection()
    {
        change = false;
        yield return new WaitForSeconds(Random.Range(1,5));
        transform.eulerAngles = new Vector3(0f,Random.Range(0,360),Random.Range(0,25));
        change = true;
    }
}
