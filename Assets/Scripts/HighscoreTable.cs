using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    [SerializeField] private Sprite[] trophies;
    private bool alreadyGreen = false, flag = false;


    private void Awake()
    {

        
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        var str = PlayerPrefs.GetString("highscoreTable");
        if (string.IsNullOrEmpty(str))
        {
            //{"highscoreEntryList":[]}
            PlayerPrefs.SetString("highscoreTable", "{\"highscoreEntryList\":[]}");
        }
       
    }

    private void SortListByScore(Highscores highscores)
    {
        //Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();

        int index = 1;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if (index <= 10)
            {
                CreateHighscoreEntryTransfrom(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }
            else
            {
                break;
            }

            index++;
        }
    }

    private void CreateHighscoreEntryTransfrom(HighscoreEntry highscoreEntry, Transform container, List<Transform> transfromList)
    {
        float templateHeight = 36f;
        Transform entryTransform = Instantiate(entryTemplate, container);

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0f, -templateHeight * transfromList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transfromList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ST"; break;
            case 3: rankString = "3RD"; break;

        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        if (highscoreEntry.score == GameManager.numberPoints && !alreadyGreen)
        {
            alreadyGreen = true;
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().fontSize = 29f;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().fontSize = 29f;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().fontSize = 29f;
        }
        if (rank == 2)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().fontSize = 27f;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().fontSize = 27f;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().fontSize = 27f;
        }
        switch (rank)
        {
            default:
                entryTransform.Find("troph").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("troph").gameObject.SetActive(true);
                break;
            case 2:
                entryTransform.Find("troph").gameObject.SetActive(true);
                entryTransform.Find("troph").gameObject.GetComponent<Image>().sprite = trophies[1];
                break;
            case 3:
                entryTransform.Find("troph").gameObject.SetActive(true);
                entryTransform.Find("troph").gameObject.GetComponent<Image>().sprite = trophies[2];
                break;
        }

        transfromList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        //Load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry to highscores
        if (highscores.highscoreEntryList.Count > 10)
        {
            foreach (HighscoreEntry entry in highscores.highscoreEntryList)
            {

                if (GameManager.numberPoints > entry.score)
                {
                    highscores.highscoreEntryList.Add(highscoreEntry);
                    break;
                }
            }
        }
        else
        {
            highscores.highscoreEntryList.Add(highscoreEntry);
        }

        
        //Sort list
        SortListByScore(highscores);

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    
    public void CheckHighscore()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        AddHighscoreEntry(GameManager.numberPoints, GameManager.playerName);

    }

    public void LoadTable()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        SortListByScore(highscores);
    }

}
