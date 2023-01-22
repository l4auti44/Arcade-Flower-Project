using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance;


    private TextMeshProUGUI points;
    public static float numberPoints;
    private float time = 1f;


    public float left_right, top_bottom;

    public static float leftRightWall;
    public static float topBottom;
    public static int pellets = 0;
    [SerializeField] private Image pelletsSllider;

    

    // Start is called before the first frame update
    void Start()
    {

        leftRightWall = left_right;
        topBottom = top_bottom;
        points = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        numberPoints = 0;
        pellets = 0;
        pelletsSllider.rectTransform.localScale = new Vector2(0f, 1f);
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
        if (pellets < 7)
        {
            pellets++;
            refreshPelletsText();
        }
        
    }

    private void refreshPelletsText()
    {
        Debug.Log(pellets);
        
        if (pellets == 0)
        {
            pelletsSllider.enabled = false;
        }
        else
        {
            pelletsSllider.enabled = true;
        }
     
        pelletsSllider.rectTransform.localScale = new Vector2(pelletsSllider.rectTransform.localScale.x + 1f, 1f);
       
        
    }

}
