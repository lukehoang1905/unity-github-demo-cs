using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int ROUND_TIME;
    public TextMeshProUGUI HUDScoreTime;
    public TextMeshProUGUI HUDGameOverText;
    public GameObject HUDGameOverPanel;
    public static int score = 0;
    private int time;

    void Start()
    {
        GameManager.OnGameStateChanged += EventSubGameStateChangeMethod;
        time = ROUND_TIME;
        StartCoroutine(countDownTimer());
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= EventSubGameStateChangeMethod;
    }

    private IEnumerator countDownTimer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        GameManager.GameInstance.SetGameState(GameState.OVER);
    }

    public static void AddScore(int amount)
    {
        score += amount;
    }
    // Update is called once per frame
    private void EventSubGameStateChangeMethod(GameState state)
    {
        if (state == GameState.OVER)
        {
            HUDGameOverPanel.SetActive(true);
            HUDGameOverText.text = $"Final Score: {score}";
            HUDScoreTime.text = "...";
        }
    }
    void Update()
    {
        HUDScoreTime.text = $"Score: {score} | Time: {time}";
    }
}
