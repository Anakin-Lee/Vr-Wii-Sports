using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private int currentScore = 360;
    private int strokes = 0;
    public TMP_Text scoreText;
    public TMP_Text strokeText;


    void Start()
    {
        UpdateScoreUI();
        UpdateStrokesUI();
    }

    public void AddScore(int amount)
    {
        currentScore -= amount;
        UpdateScoreUI();
    }
    public void strokeCounter()
    {
        strokes = strokes + 1;
        UpdateStrokesUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
    }
    private void UpdateStrokesUI()
    {
        if (strokes != null) strokeText.text = "Strokes: " + strokes;
    }
}

