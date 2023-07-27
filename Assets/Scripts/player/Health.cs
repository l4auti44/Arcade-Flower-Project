using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private TextMeshProUGUI healthText, youDie;
    [SerializeField] private Canvas canvas;
    private GameObject highscore;

    private Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        healthText = canvas.transform.Find("HealtText").GetComponent<TextMeshProUGUI>();
        youDie = canvas.transform.Find("die").GetComponent<TextMeshProUGUI>();
        restartButton = canvas.transform.Find("restart").GetComponent<Button>();
        highscore = canvas.transform.Find("Highscore Table").gameObject;


        youDie.enabled = false;
        restartButton.enabled = false;
        restartButton.image.enabled = false;
        highscore.SetActive(false);
        
        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        healthText.text = "x " +health;
    }



    public void decreaseHealth(float damage) {

        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            die();
        }
        refreshText();
    
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        refreshText();
    }

    public void refreshText()
    {
        healthText.text = "x " + health;
    }

    public void die()
    {
        GetComponent<AudioManager>().PlaySound("Killed");
        highscore.GetComponent<HighscoreTable>().CheckHighscore();
        
        //checkHighscore();
        youDie.enabled = true;
        restartButton.enabled = true;
        restartButton.image.enabled = true;
        highscore.SetActive(true);

        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        Time.timeScale = 0f;
    }

    private void checkHighscore()
    {
        
        /*
        string level = SceneManager.GetActiveScene().name;
        if (GameManager.numberPoints > PlayerPrefs.GetFloat(level))
        {
            PlayerPrefs.SetFloat(level, GameManager.numberPoints);
            highscore.text = "Highscore: " + GameManager.numberPoints;
        }
        else
        {
            highscore.text = "Highscore: " + PlayerPrefs.GetFloat(level);
        }
        */
    }

    public void restart()
    {
        youDie.enabled = false;
        restartButton.enabled = false;
        restartButton.image.enabled = false;
        highscore.SetActive(false);

        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        Time.timeScale = 1f;
        health = 5f;
    }
}
