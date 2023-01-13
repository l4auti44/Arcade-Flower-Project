using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject bullet;
    public float spawnTimeBullet = 0.5f;
    public GameObject wateryBlowHog;
    public float spawnTimeWatery = 6f;
    public GameObject anodeBeetle;
    public float spawnTimeAnodeBeetle = 10f;
    public bool enableBullet, enableWateryBlowHog, enableAnoneBeetle = true;

    private float _spawnTimeBullet, _spawnTimeWatery, _spawnTimeAnodeBeetle;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimeBullet = spawnTimeBullet;
        _spawnTimeWatery = spawnTimeWatery;
        _spawnTimeAnodeBeetle = spawnTimeAnodeBeetle;
    }

    private void Update()
    {
        if (enableBullet) _spawnTimeBullet -= Time.deltaTime;
        if (enableWateryBlowHog) _spawnTimeWatery -= Time.deltaTime;
        if (enableAnoneBeetle) _spawnTimeAnodeBeetle -= Time.deltaTime;


        if (_spawnTimeBullet <= 0.0f)
        {
            GameObject.Instantiate(bullet, bullet.transform.position, this.transform.rotation);
            _spawnTimeBullet = spawnTimeBullet;
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
    }
}
