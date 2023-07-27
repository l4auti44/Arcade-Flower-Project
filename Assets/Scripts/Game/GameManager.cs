using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance;

    private TextMeshProUGUI points;
    public static int numberPoints;
    public static string playerName;
    private float time = 1f;

    [SerializeField] private GameObject pelletMeter;
    public float left_right, top_bottom;

    public static float leftRightWall;
    public static float topBottom;
    [SerializeField] private int _pellets = 7;
    public static int pellets;
    [SerializeField] private Image pelletsSllider;


    // Start is called before the first frame update
    void Start()
    {
        playerName = SceneController.Instance._playerName;
        leftRightWall = left_right;
        topBottom = top_bottom;
        points = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        numberPoints = 0;
        pellets = _pellets;
        pelletsSllider.rectTransform.localScale = new Vector2(0f, pelletsSllider.rectTransform.localScale.y);
        points.text = numberPoints.ToString();
        refreshPelletsText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            numberPoints += 10;
            points.text = numberPoints.ToString();
            time += 1;
        }



    }

    public void pickUpPellet()
    {
        if (pellets < 7)
        {
            pellets++;
            refreshPelletsText();
        }
        
    }

    public void usePellets(int amount) 
    {
        pellets = pellets - amount;
        refreshPelletsText();
    }

    public void refreshPelletsText()
    {
        
        if (pellets == 0)
        {
            pelletsSllider.enabled = false;
        }
        else
        {
            pelletsSllider.enabled = true;
        }
        
        pelletsSllider.rectTransform.localScale = new Vector2(1 * pellets, pelletsSllider.rectTransform.localScale.y);

        
    }

    public void addPoints(int amount)
    {
        numberPoints += amount;
        points.text = numberPoints.ToString();
    }

    public void notEnoughtPelletAnim()
    {
        pelletMeter.GetComponent<Animator>().SetTrigger("notEnoughtPellet");
        pelletMeter.GetComponent<Animator>().SetTrigger("notEnoughtPellet");
    }
}
