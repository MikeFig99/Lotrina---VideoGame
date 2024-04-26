using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloodController : MonoBehaviour
{
    public GameObject agua;
    private LowPolyWater.LowPolyWater move;
    public float frequency, length, height,incress = 0;
    public GameObject sign;
    public AudioSource audioSource;
    public AudioClip drama;

    private void Start()
    {
        move = agua.GetComponent<LowPolyWater.LowPolyWater>();
    }

    public void Flood()
    {
        print("ïnundacion");
        agua.transform.position += new Vector3(0,incress,0);
        move.waveFrequency = frequency != 0 ? frequency : move.waveFrequency ;
        move.waveHeight = height != 0 ? height : move.waveHeight;
        move.waveLength = length != 0 ? length : move.waveLength;
        audioSource.clip = drama;
        audioSource.Play();
        sign.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sign.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    
}
