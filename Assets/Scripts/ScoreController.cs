using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text p1Text, p2Text;
    private int p1Score, p2Score;

    #region Singleton
    public static ScoreController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        p1Score = 0;
        p2Score = 0;
    }

    public void AddP1Score(int value)
    {
        p1Score += value;
        p1Score = Mathf.Max(p1Score, 0);
    }

    public void AddP2Score(int value)
    {
        p2Score += value;
        p2Score = Mathf.Max(p2Score, 0);
    }

}
