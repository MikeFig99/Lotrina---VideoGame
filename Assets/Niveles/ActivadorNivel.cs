using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNivel : MonoBehaviour {
	
	public GameObject Nivel;
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Nivel.SetActive (true);
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Nivel.SetActive(false);
		}
	}
}
