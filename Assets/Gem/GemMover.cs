using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GemMover : MonoBehaviour
{
    public float speed = 2.0f;
    // Start is called before the first frame update
    public int gemPoint = 1;
    void Start() { GameManager.OnGameStateChanged += EventSubGameStateChangeMethod; }

    private void EventSubGameStateChangeMethod(GameState state)
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= EventSubGameStateChangeMethod;
    }

    // Update is called once per frame
    void Update()
    {
        float fallPosition = transform.position.y - speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, fallPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            Debug.Log("player");
            AudioSource audioSource = other.GetComponent<AudioSource>();
            AudioSource audio2 = other.gameObject.GetComponents<AudioSource>()[0];
            ScoreManager.AddScore(gemPoint);
            audioSource.Play();
            audio2.Play();
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("ground");
            Destroy(gameObject);
        }
    }
}
