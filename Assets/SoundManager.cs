using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource backgroundMusic;

    private void Awake()
    {

    }
    void Start()
    {
        GameManager.OnGameStateChanged += StartOnStateChange;
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.Play();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= StartOnStateChange;
    }

    private void StartOnStateChange(GameState state)
    {
        if (state == GameState.OVER)
        {
            backgroundMusic.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
