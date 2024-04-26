using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionController : MonoBehaviour
{
    public GameObject [] correctIncorrect;
    public GameObject complete;
    
    public Sprite correct;
    public Sprite incorrect;

    private int[] questions={0,0,0,0};
    private int count = 0;
    private int score;
    
    void Start()
    {
        for (int i = 1; i < 4; )
        {
            questions[i] = Random.Range(0, 12);
            if (questions[i] != questions[i - 1])
            {
                i++;
            }
        }
        transform.GetChild(questions[count+1]).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (count >= 3)
        {
            if (score >= 2)
            {
                complete.SetActive(true);
                print("GANASTE");
            }
            else
            {
                GameManager.Instance.StartCoroutine("Death");
                print("PERDISTE");
            }
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    public void CorrectAnswer()
    {
        correctIncorrect[count].GetComponent<UnityEngine.UI.Image>().overrideSprite = correct;
        print("Respuesta Correcta");
        score++;
        StartCoroutine("QuestionChange");
    }
    public void IncorrectAnswer()
    {
        correctIncorrect[count].GetComponent<UnityEngine.UI.Image>().overrideSprite = incorrect;
        print("Respuesta Inconrrecta");
        StartCoroutine("QuestionChange");
    }
    
    IEnumerator QuestionChange()
    {
        transform.GetChild(questions[count+1]).gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        count++;
        transform.GetChild(questions[count+1]).gameObject.SetActive(true);
    }
}
