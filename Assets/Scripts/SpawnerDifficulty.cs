using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDifficulty : MonoBehaviour
{
    private spawner spawner;
    
    private Bullet bullet;
    //Difficulty

    //BULLET (limitTimeBullet, minSpawnRateBullet)
    [SerializeField] private bool enableBulletDifficulty = true;
    [SerializeField] private float bulletLimitTime = 60f;
    private float bulletDefaultSpawnRate;
    [SerializeField] private float bulletMinSpawnRate = 0.2f;

    //BULLET SPEED V(0, 3.2f)
    [SerializeField] private float bulletDefaultSpeed = 3.2f;
    [SerializeField] private float bulletMaxSpeed = 5f;
    

    private float timerDiffBul = 0f, pBullet, pBulletVel;

    //WATERY BLOWHOG
    [SerializeField] private bool enableWateryDifficulty = true;
    [SerializeField] private float wateryLimitTime = 60f;
    private float wateryDefaultSpawnTime;
    [SerializeField] private float wateryMinSpawnRate = 1f;

    private float timerDiffWat = 0f, pWatery;
    // Start is called before the first frame update
    void Start()
    {
        
        spawner = GetComponent<spawner>();
        bullet = spawner.bullet.GetComponent<Bullet>();
        bullet.speed = bulletDefaultSpeed;
        bulletDefaultSpawnRate = spawner.spawnTimeBullet;
        wateryDefaultSpawnTime = spawner.spawnTimeWatery;
        


        //p=((A.x-V.x)^(2))/(A.y-V.y)/(4)
        pBulletVel = (Mathf.Pow((bulletLimitTime - 0), 2) / (bulletMaxSpeed - bulletDefaultSpeed)) / 4f;
        pBullet = (Mathf.Pow((bulletLimitTime - 0), 2) / (bulletMinSpawnRate - 1)) / 4f;
        pWatery = (Mathf.Pow((wateryLimitTime - 0), 2) / (wateryMinSpawnRate - wateryDefaultSpawnTime)) / 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableBulletDifficulty)
        {
            timerDiffBul += Time.deltaTime;
            if (timerDiffBul <= bulletLimitTime)
            {

                spawner.spawnTimeBullet = ParabolaWith3Points(pBullet, new Vector2(0, bulletDefaultSpawnRate), timerDiffBul);
                bullet.speed = ParabolaWith3Points(pBulletVel, new Vector2(0, bulletDefaultSpeed), timerDiffBul);
            }
        }
        if (enableWateryDifficulty)
        {
            timerDiffWat += Time.deltaTime;
            if (timerDiffWat <= wateryLimitTime)
            {
                spawner.spawnTimeWatery = ParabolaWith3Points(pWatery, new Vector2(0, wateryDefaultSpawnTime), timerDiffWat);
            }
        }
    }

    public float ParabolaWith3Points(float p, Vector2 ver, float A)
    {


        //var p = (Mathf.Pow((A.x - ver.x), 2) / (A.y - ver.y)) / 4f;

        //y=(x-x(V))^(2)*((1)/(4 p))+y(V)
        float y = (Mathf.Pow(A - ver.x, 2) * (1f / (4f * p))) + ver.y;
        return y;
    }

}
