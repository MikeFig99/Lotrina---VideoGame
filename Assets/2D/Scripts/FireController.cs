using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    GameManager.Instance.StartCoroutine("Death");
  }
}
