using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float Valor)
    {
        sliderValue = Valor;
        PlayerPrefs.SetFloat("VolumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

    public void MuteUnmute()
    {
        if (AudioListener.volume == 0)
        {
            // Poner sonido
            AudioListener.volume = sliderValue;
            PlayerPrefs.SetFloat("volumenAudio", sliderValue);
            imagenMute.enabled = false;
        }
        else
        {
            // Quitar sonido
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("volumenAudio", 0);
            imagenMute.enabled = true;
        }
    }
}
