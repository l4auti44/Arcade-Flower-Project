using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealtText").GetComponent<TextMeshProUGUI>();
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
        GameObject.Find("Canvas").GetComponent<PauseMenu>().Pause(true);

    }

}
