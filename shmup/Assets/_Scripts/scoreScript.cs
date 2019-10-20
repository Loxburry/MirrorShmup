using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    
    public Text Score;
    public Text HighScore;

    private int score;
    private static int highScore;

    public static scoreScript S;
    
    public void Awake(){
        score = 0;
        S = this;

        highScore = PlayerPrefs.GetInt("highScore", highScore);
        HighScore.text = highScore.ToString();
    }
    
    public void UpdateScore(int points){
        score+= points;
        Score.text = score.ToString();
    }

    public void Update(){
        if(score > highScore){
            highScore = score;
            HighScore.text = highScore.ToString();

            PlayerPrefs.SetInt("highScore", highScore);
        }
    }
}
