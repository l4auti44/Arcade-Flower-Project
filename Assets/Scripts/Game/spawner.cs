using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    public static spawner Instance;

    public GameObject bullet;
    public float spawnTimeBullet = 0.5f;
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
                GameObject.Instantiate(bullet, bullet.transform.position, this.transform.rotation);
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
                _spawnTimeAnodeBeetle = spawnTimeAnodeBeetle;
            }

            if (_spawnTimePellet <= 0)
            {
                GameObject.Instantiate(pellet, pellet.transform.position, pellet.transform.rotation);
                _spawnTimePellet = spawnTimePellet;
            }
        }
        
    }

}
