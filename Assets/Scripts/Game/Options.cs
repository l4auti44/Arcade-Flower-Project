using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] private TMP_InputField watery, beetle, bullet, pellet;
 
    // Start is called before the first frame update
    void Start()
    {
        watery.text = spawner.Instance.spawnTimeWatery.ToString();
        beetle.text = spawner.Instance.spawnTimeAnodeBeetle.ToString();
        bullet.text = spawner.Instance.spawnTimeBullet.ToString();
        pellet.text = spawner.Instance.spawnTimePellet.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveParameters()
    {
        
        spawner.Instance.spawnTimeWatery = float.Parse(watery.text);
        spawner.Instance.spawnTimeAnodeBeetle = float.Parse(beetle.text);
        spawner.Instance.spawnTimeBullet = float.Parse(bullet.text);
        spawner.Instance.spawnTimePellet = float.Parse(pellet.text);
        Debug.Log("save Correctly");
    }
}
