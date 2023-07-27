using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public static string _playerName = "PLAYER";
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public static void SceneLoader(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void ChangeName()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            var playerNameText = GameObject.Find("TextPlayerName").GetComponent<TextMeshProUGUI>().text;
            
            if (playerNameText != "PLAYER")
            {
                _playerName = playerNameText;
                Debug.Log("PlayerName: " + _playerName.ToString());
            }
        }
    }

}
