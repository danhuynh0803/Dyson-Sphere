using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int maxScore = 12;
    public GameObject[] houseProgressionModels;
    public Material p1Color, p2Color;

    private int currScore = 0;

    void Start()
    {
        currScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currScore >= maxScore)
        {
            // Setting this to true will trigger win scene
            // TODO maybe make a win panel instead
            GameController.instance.isGameOver = true;
        }
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
            }
            else if (controllingPlayer == Player.P2)
            {
                houseProgressionModels[currScore].GetComponent<Renderer>().material = p2Color;
            }

            Destroy(other.gameObject);
            houseProgressionModels[currScore].SetActive(true);

            currScore++;
        }
    }
}
