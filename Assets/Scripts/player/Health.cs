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
    private TextMeshProUGUI healthText, youDie, highscore;

    private Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealtText").GetComponent<TextMeshProUGUI>();
        youDie = GameObject.Find("die").GetComponent<TextMeshProUGUI>();
        restartButton = GameObject.Find("restart").GetComponent<Button>();
        highscore = GameObject.Find("highscoreText").GetComponent<TextMeshProUGUI>();
        youDie.enabled = false;
        restartButton.enabled = false;
        restartButton.image.enabled = false;
        highscore.enabled = false;
        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        healthText.text = "x " +health;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void increaseHealth(float amount)
    {

    }

    public void refreshText()
    {
        healthText.text = "x " + health;
    }

    public void die()
    {
        GetComponent<AudioManager>().PlaySound("Killed");
        checkHighscore();
        youDie.enabled = true;
        restartButton.enabled = true;
        restartButton.image.enabled = true;
        highscore.enabled = true;
        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        Time.timeScale = 0f;
    }

    private void checkHighscore()
    {
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
    }

    public void restart()
    {
        youDie.enabled = false;
        restartButton.enabled = false;
        restartButton.image.enabled = false;
        highscore.enabled = false;

        restartButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        Time.timeScale = 1f;
        health = 5f;
    }
}
