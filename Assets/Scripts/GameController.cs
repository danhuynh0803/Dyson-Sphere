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
            MenuEvents.instance.LoadScene("WinScene");
         }
    }

}
