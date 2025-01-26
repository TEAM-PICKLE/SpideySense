using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int score = 0;


    private void Awake()
    {
       
    }

    private void Update()
    {
        
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public int GetScore() { return score; }
}
