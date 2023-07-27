using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public string _playerName = "PLAYER";
    
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



    public void SceneLoader(string name)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            var playerNameText = GameObject.Find("TextPlayerName").GetComponent<TextMeshProUGUI>().text;
            if (!string.IsNullOrEmpty(playerNameText))
            {
                _playerName = playerNameText;
                Debug.Log("PlayerName");
            }
            
        }
        SceneManager.LoadScene(name);
    }


}
