using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesUI : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject botonContinuar;
    [SerializeField] private GameObject menuOpciones;
    
    private bool menuOpen = false;
    private void Start()
    {
        LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 1f).setDelay(1f)
            .setEase(LeanTweenType.easeOutBounce); //.setOnComplete(BajarAlpha);
        LeanTween.moveX(botonContinuar.GetComponent<RectTransform>(), 0, 1f).setDelay(4f)
            .setEase(LeanTweenType.easeOutBounce); //.setOnComplete(BajarAlpha);
    }
    
    public void ActivarMenuOpciones(){
        if (menuOpen)
    {
        // Animacion cerrar menu
        LeanTween.moveY(menuOpciones.GetComponent<RectTransform>(), -160, 1f)
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(() => menuOpen = false); 
    }
    else
    {
        // Animacion abrir menu
        LeanTween.moveY(menuOpciones.GetComponent<RectTransform>(), 110, 1f)
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(() => menuOpen = true); 
    }
}
}

