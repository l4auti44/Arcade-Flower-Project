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
    


    private void Awake()
    {

        
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

       
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
        float templateHeight = 20f;
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
            case 3: rankString = "3ST"; break;

        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.green;
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
        highscores.highscoreEntryList.Add(highscoreEntry);

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

        if (highscores.highscoreEntryList.Count > 10)
        {
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {

                if (GameManager.numberPoints > highscoreEntry.score)
                {
                    AddHighscoreEntry(GameManager.numberPoints, "TEST");
                    
                    break;
                }

            }
        }
        else
        {
            AddHighscoreEntry(GameManager.numberPoints, GameManager.playerName);
        }

    }

}
