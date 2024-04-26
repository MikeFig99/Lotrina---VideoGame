using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        Debug.Log("Salir...");
        Application.Quit();
    }
}