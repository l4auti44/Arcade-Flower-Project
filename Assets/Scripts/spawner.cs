using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;

    private float targetTime = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        targetTime -= Time.deltaTime;
        
        if (targetTime <= 0.0f)
        {
            GameObject.Instantiate(enemy, enemy.transform.position, this.transform.rotation);
            targetTime = 1f;
        }

    }
}
