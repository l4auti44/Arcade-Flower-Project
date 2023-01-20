using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private TextMeshProUGUI points;
    private float numberPoints;
    private float time = 1f;


    public float left_right, top_bottom;

    public static float leftRightWall;
    public static float topBottom;
    public static int pellets = 0;
    [SerializeField] private Slider pelletsSllider;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Instance.Start();
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

        leftRightWall = left_right;
        topBottom = top_bottom;
        points = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        numberPoints = 0;
        pellets = 0;
        points.text = "Points: " + numberPoints;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            numberPoints += 10;
            points.text = "Points: " + numberPoints;
            time += 1;
        }
        
        



    }

    public void pickUpPellet()
    {
        pellets++;
        refreshPelletsText();
    }

    private void refreshPelletsText()
    {
        Debug.Log(pellets);
        Image fillpel = GameObject.Find("FillPellets").GetComponent<Image>();
        if (pellets == 0)
        {
            fillpel.enabled = false;
        }
        else
        {
            fillpel.enabled = true;
        }
        pelletsSllider.value = pellets;
    }

}
