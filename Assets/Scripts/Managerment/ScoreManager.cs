using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private float totalScore;

    public void Score(float score){
        totalScore += score;
    }
    public float DisplayScore(){
        return totalScore;
    }
}
