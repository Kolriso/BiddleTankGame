using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]

public class ScoreData : IComparable<ScoreData>
{
    public string name;
    public int score;

    public int CompareTo(ScoreData other)
    {
        if (other == null)
        {
            return 1;
        }
        return this.score - other.score;

        //if (this.score > other.score)
        //{
        //   return 1;
        //}
        //if (this.score < other.score)
       // {
        //    return -1;
        //}
        //return 0;
    }
       
    public static bool operator < (ScoreData firstValue, ScoreData secondValue)
    {
        return firstValue.CompareTo(secondValue) < 0;
    }

    public static bool operator > (ScoreData firstValue, ScoreData secondValue)
    {
        return firstValue.CompareTo(secondValue) > 0;
    }
}
