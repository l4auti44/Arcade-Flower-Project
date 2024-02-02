using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LimitInputCharacters : MonoBehaviour
{
    public TMP_InputField mainInputField;
    public int maxAmount;

    void Start()
    {
        //Changes the character limit in the main input field.
        mainInputField.characterLimit = maxAmount;
    }


}
