using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int maxScore = 12;
    public GameObject[] houseProgressionModels;
    public Material p1Color, p2Color;

    private int currScore = 0;
    private int redPoint = 0;
    private int bluePoint = 0;

    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject cameraThree;
    public GameObject playerRed;
    public GameObject playerBlue;
    public GameObject redWins;
    public GameObject blueWins;
    public GameObject blueWinner;
    public GameObject redWinner;

    void Start()
    {
        currScore = 0;
        cameraThree.SetActive(false);
        redWins.SetActive(false);
        redWinner.SetActive(false);
        blueWins.SetActive(false);
        blueWinner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currScore >= maxScore)
        {
            // Setting this to true will trigger win scene
            // TODO maybe make a win panel instead
            GameController.instance.isGameOver = true;
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        // Absorb fragments
        // TODO refactor so that it doesnt need to
        // check that the object was fired
        if (other.gameObject.GetComponent<Orbiting>() != null && currScore < maxScore)
        {
            // TODO play a construction noise (hammers, saws, etc)
            // Add a wall segment and change the color
            Player controllingPlayer = other.gameObject.GetComponent<Orbiting>().controllingPlayer;
            if (controllingPlayer == Player.P1)
            {
                houseProgressionModels[currScore].GetComponent<Renderer>().material = p1Color;
                redPoint++;
            }
            else if (controllingPlayer == Player.P2)
            {
                houseProgressionModels[currScore].GetComponent<Renderer>().material = p2Color;
                bluePoint++;
            }

            Destroy(other.gameObject);
            houseProgressionModels[currScore].SetActive(true);

            SoundController.Play(5, 0.1f); //play point noise
            currScore++;

            if (currScore >= maxScore)
            {
                EndGame();
            }
        }
    }
        void EndGame()
    {
        cameraOne.SetActive(false);
        cameraTwo.SetActive(false);
        cameraThree.SetActive(true);
        playerRed.SetActive(false);
        playerBlue.SetActive(false);
        if (redPoint > bluePoint)
        {
            redWins.SetActive(true);
            redWinner.SetActive(true);
        }
        else
        {
            blueWins.SetActive(true);
            blueWinner.SetActive(true);
        }

    }
}
