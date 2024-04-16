using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualizator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private void OnEnable()
    {
        GameScoreCounter.OnGameScoreChanged += ChangeScore;
    }

    private void OnDisable()
    {
        GameScoreCounter.OnGameScoreChanged -= ChangeScore;
    }

    private void ChangeScore(int score)
    {
        text.text = score.ToString();
    }

}
