using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [SerializeField] private int pointsToExtraLife = 1000;
    [SerializeField] private int pointsPerSecond = 10;
    private int _pointsToExtraLife;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _pointsToExtraLife = pointsToExtraLife;
        playerName = PlayerPrefs.GetString("PlayerName");
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
            addPoints(pointsPerSecond);
            time += 1;
        }

        if (_pointsToExtraLife <= 0)
        {
            _pointsToExtraLife = pointsToExtraLife + _pointsToExtraLife;
            _player.GetComponent<Health>().IncreaseHealth(1);
        }

    }

    public void pickUpPellet(int points)
    {
        if (pellets < 7)
        {
            pellets++;
            refreshPelletsText();
        }
        addPoints(points);
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
        _pointsToExtraLife -= amount;
        points.text = numberPoints.ToString();
    }

    public void notEnoughtPelletAnim()
    {
        pelletMeter.GetComponent<Animator>().SetTrigger("notEnoughtPellet");
        pelletMeter.GetComponent<Animator>().SetTrigger("notEnoughtPellet");
    }
}
