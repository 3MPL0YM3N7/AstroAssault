using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        ShowScore();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
    }

    public int GetScore()
    {
        return score;
    }

    void ShowScore()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
