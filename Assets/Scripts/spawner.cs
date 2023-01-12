using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject bullet;
    public float spawnTimeBullet = 1f;
    public GameObject wateryBlowHog;
    public float spawnTimeWatery = 20f;

    private float _spawnTimeBullet, _spawnTimeWatery;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimeBullet = spawnTimeBullet;
        _spawnTimeWatery = spawnTimeWatery;
    }

    private void Update()
    {
        _spawnTimeBullet -= Time.deltaTime;
        _spawnTimeWatery -= Time.deltaTime;


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
    }
}
