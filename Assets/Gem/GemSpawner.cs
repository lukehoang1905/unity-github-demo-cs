using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{



    // Start is called before the first frame update
    public float timePerSpawn = 3.0f;
    public float timer = 0;
    public GameObject gemPrefap;


    void Start()
    {
        GameManager.OnGameStateChanged += EventSubGameStateChangeMethod;
    }

    private void EventSubGameStateChangeMethod(GameState state)
    {
        if (state == GameState.OVER)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= EventSubGameStateChangeMethod;

    }
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= timePerSpawn)
        {
            float randomX = Random.Range(-5.5f, 5.5f);
            Vector3 spawnPosition = new Vector3(randomX, 5.8f, 0f);

            Instantiate<GameObject>(gemPrefap, spawnPosition, Quaternion.identity);

            Debug.Log("Spawners");
            timer = 0;
        }

    }

}
