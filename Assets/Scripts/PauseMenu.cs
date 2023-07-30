using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private GameObject restartButton, resumeButton;
    private GameObject youAreDeadText, pauseText;
    private GameObject highscoreTable;
    private bool playerIsDead = false;

    // Update is called once per frame
    private void Start()
    {
        
        restartButton = pauseMenuUI.transform.Find("RestartButton").gameObject;
        restartButton.SetActive(false);
        resumeButton = pauseMenuUI.transform.Find("ResumeButton").gameObject;

        highscoreTable = pauseMenuUI.transform.Find("Highscore Table").gameObject;
        highscoreTable.SetActive(false);

        youAreDeadText = pauseMenuUI.transform.Find("DieText").gameObject;
        youAreDeadText.SetActive(false);
        pauseText = pauseMenuUI.transform.Find("PauseText").gameObject;


        pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerIsDead)
        {
            if (gameIsPaused )
            {
                Resume();
            }
            else
            {
                Pause(false);
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause(bool playerDead)
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        if (playerDead)
        {
            playerIsDead = true;
            restartButton.SetActive(true);
            youAreDeadText.SetActive(true);
            resumeButton.SetActive(false);
            pauseText.SetActive(false);
            highscoreTable.SetActive(true);
            highscoreTable.GetComponent<HighscoreTable>().CheckHighscore();
        }
        else
        {
            restartButton.SetActive(false);
            youAreDeadText.SetActive(false);
            resumeButton.SetActive(true);
            pauseText.SetActive(true);
            highscoreTable.SetActive(false);
        }
        

    }
}
