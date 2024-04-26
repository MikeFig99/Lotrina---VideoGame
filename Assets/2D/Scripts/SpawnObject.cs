using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objeto;
    private bool flag = true;
    public float delay = 2;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && flag)
        {
            flag = false;
            //print("Aparecio un objeto");
            Instantiate(objeto, transform.GetChild(0).position, transform.GetChild(0).rotation);
            StartCoroutine("Delay");
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        flag = true;
    }
}