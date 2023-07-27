using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInputText : MonoBehaviour
{
    private TMP_InputField playerText;
    // Start is called before the first frame update
    void Start()
    {
        playerText = gameObject.GetComponent<TMP_InputField>();
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            PlayerPrefs.SetString("PlayerName", "PLAYER");
        }

        playerText.text = PlayerPrefs.GetString("PlayerName");
    }

    
}
