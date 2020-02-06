using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageHUD : MonoBehaviour
{
    public PlayerController player;
    public Image containerImageSlot;
    public Sprite empty, one, two, three;

    // Update is called once per frame
    void Update()
    {
        switch (player.GetCarriedCount())
        {
            case 0:
                containerImageSlot.sprite = empty;
                break;
            case 1:
                containerImageSlot.sprite = one;
                break;
            case 2:
                containerImageSlot.sprite = two;
                break;
            case 3:
                containerImageSlot.sprite = three;
                break;
            default:
                containerImageSlot.sprite = three;
                break;
        }
    }
}
