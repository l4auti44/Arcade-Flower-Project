using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreMainMenu : MonoBehaviour
{
    [SerializeField] GameObject highscoreMenu;
    [SerializeField] HighscoreTable _highscoreTable;
    // Start is called before the first frame update
    void Start()
    {

        _highscoreTable.LoadTable();
        highscoreMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHighscore()
    {
        highscoreMenu.SetActive(!highscoreMenu.activeSelf);
    }
}
