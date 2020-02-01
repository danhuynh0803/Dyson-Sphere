using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int maxScore = 10;
    public GameObject[] houseProgressionModels;
    public int[] tierRequirement;

    private bool hasSwappedTier;
    private int tier = 0;

    private int currScore = 0;

    // Update is called once per frame
    void Update()
    {
        // Check that a player has enough fragments to advance a tier
        if (!hasSwappedTier)
        {
        }

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
        if (other.gameObject.GetComponent<Fragment>() != null)
        {
            currScore++;
            Destroy(other.gameObject);
        }
    }
}
