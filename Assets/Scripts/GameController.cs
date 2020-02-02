using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    public static GameController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public bool isGameOver = false;

    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
         if (isGameOver)
         {
            //enuEvents.instance.LoadScene("WinScene");
            // TODO transition to win. Have the camera zoom in on the finished building
            // Display either player1 or p2's vacuum
         }
    }

}
