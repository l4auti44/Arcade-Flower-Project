using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    [HideInInspector] public static spawner Instance { get; private set; }

    public GameObject bullet;
    public GameObject bulletsParent;
    public float spawnTimeBullet = 1f;
    public GameObject wateryBlowHog;
    public float spawnTimeWatery = 6f;
    public GameObject anodeBeetle;
    public float spawnTimeAnodeBeetle = 10f;
    [SerializeField] private GameObject pellet;
    public float spawnTimePellet = 6f;
    public bool enableBullet = false, enableWateryBlowHog = false, enableAnoneBeetle = false, enablePellets = false;

    private float _spawnTimeBullet, _spawnTimeWatery, _spawnTimeAnodeBeetle, _spawnTimePellet;
    
    private bool enableScript = true;

    static public int bulletAmount = 0;

    private bool anodeBeetleSpawned = false;

    private Vector2 cornerBottomLeft;
    private Vector2 RightUpperCorner;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _spawnTimeBullet = spawnTimeBullet;
        _spawnTimeWatery = spawnTimeWatery;
        _spawnTimeAnodeBeetle = spawnTimeAnodeBeetle;
        _spawnTimePellet = spawnTimePellet;

    }

    

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("level"))
        {
            enableScript = true;

        }
        else
        {
            enableScript = false;
        }

        if (enableScript)
        {
            if (enableBullet) _spawnTimeBullet -= Time.deltaTime;
            if (enableWateryBlowHog) _spawnTimeWatery -= Time.deltaTime;
            if (enableAnoneBeetle) _spawnTimeAnodeBeetle -= Time.deltaTime;
            if (enablePellets) _spawnTimePellet -= Time.deltaTime;


            if (_spawnTimeBullet <= 0.0f)
            {
                GameObject.Instantiate(bullet, bullet.transform.position, this.transform.rotation, bulletsParent.transform);
                _spawnTimeBullet = spawnTimeBullet;
                bulletAmount++;
            }

            if (_spawnTimeWatery <= 0f)
            {
                GameObject.Instantiate(wateryBlowHog, wateryBlowHog.transform.position, this.transform.rotation);
                _spawnTimeWatery = spawnTimeWatery;
            }
            if (_spawnTimeAnodeBeetle <= 0)
            {
                GameObject.Instantiate(anodeBeetle, anodeBeetle.transform.position, anodeBeetle.transform.rotation);
                _spawnTimeAnodeBeetle = 1000f;
            }

            if (_spawnTimePellet <= 0)
            {
                GameObject.Instantiate(pellet, pellet.transform.position, pellet.transform.rotation);
                _spawnTimePellet = spawnTimePellet;
            }
        }

    }

    public void ResetAnodeBeetle()
    {
        anodeBeetleSpawned = false;
        _spawnTimeAnodeBeetle = spawnTimeAnodeBeetle;
    }


    public Dictionary<string, Vector3> GetSpawnPositionOnBorderOfArea()
    {
        Dictionary<string, Vector3> result = new Dictionary<string, Vector3>();
        result.Add("position", Vector3.zero);
        result.Add("rotation", Vector3.zero);
        result.Add("positionOnArea", Vector3.zero);
        var offset = 0.5f;
        bool valid = false;

        while (valid != true)
        {
            
            var randomX = UnityEngine.Random.Range(-GameManager.leftRightWall + offset, GameManager.leftRightWall - offset);
            var randomY = UnityEngine.Random.Range(-GameManager.topBottom + offset, GameManager.topBottom - offset);

            var randomWall = UnityEngine.Random.Range(0, 4);

            switch (randomWall)
            {
                //top
                case 0:
                    if (CheckObjectInArea("Enemy", new Vector2(randomX - 0.7f, GameManager.topBottom), new Vector2(randomX + 0.8f, GameManager.topBottom + 10)))
                    {
                        break;
                    }
                    result["position"] = new Vector3(randomX, GameManager.topBottom, 0f);
                    result["positionOnArea"] = Vector3.up;
                    break;
                //bottom
                case 1:
                    if (CheckObjectInArea("Enemy", new Vector2(randomX + 0.7f, -GameManager.topBottom), new Vector2(randomX -0.8f, -GameManager.topBottom - 10)))
                    {
                        break;
                    }
                    result["rotation"] = new Vector3(0, 0, 180f);
                    result["position"] = new Vector3(randomX, -GameManager.topBottom, 0f);
                    result["positionOnArea"] = Vector3.down;
                    break;

                //left
                case 2:
                    if (CheckObjectInArea("Enemy", new Vector2(-GameManager.leftRightWall, randomY + 0.7f), new Vector2(-GameManager.leftRightWall -10, randomY - 0.8f)))
                    {
                        break;
                    }
                    result["rotation"] = new Vector3(0, 0, 90f);
                    result["position"] = new Vector3(-GameManager.leftRightWall, randomY, 0f);
                    result["positionOnArea"] = Vector3.left;
                    break;

                //right
                case 3:
                    if (CheckObjectInArea("Enemy", new Vector2(GameManager.leftRightWall, randomY - 0.7f), new Vector2(GameManager.leftRightWall + 10, randomY + 0.8f)))
                    {
                        break;
                    }
                    result["rotation"] = new Vector3(0, 0, 270f);
                    result["position"] = new Vector3(GameManager.leftRightWall, randomY, 0f);
                    result["positionOnArea"] = Vector3.right;
                    break;

            }
            if (result["position"] != Vector3.zero)
            {
                valid = true;
            }
        }

        

        return result;

    }


    public bool CheckObjectInArea(string tag, Vector2 cornerBottomLeftF, Vector2 RightUpperCornerF)
    {
        cornerBottomLeft = cornerBottomLeftF;
        RightUpperCorner = RightUpperCornerF;


        // Check if there is an object with the tag in the rectangular screen area
        Collider2D[] objectsInArea = Physics2D.OverlapAreaAll(cornerBottomLeft, RightUpperCorner, LayerMask.GetMask("Default"));

        // Iterate over the objects in the area and check if they have the specified tag
        foreach (Collider2D _object in objectsInArea)
        {
            if (_object.CompareTag(tag))
            {
                Debug.Log("Find Enemy " + _object.name +" at " + _object.transform.position + ". Finding new position...");
                return true; // Return true if an object with the tag is found in the area
            }
        }
        //Debug.Log("Don't find enemy");
        return false; // Return false if no object with the tag is found in the area
    }

    void OnDrawGizmos()
    {
        // Draw the rectangular area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((cornerBottomLeft + RightUpperCorner) / 2, new Vector3(Mathf.Abs(RightUpperCorner.x - cornerBottomLeft.x), Mathf.Abs(RightUpperCorner.y - cornerBottomLeft.y), 0));
    }

}
