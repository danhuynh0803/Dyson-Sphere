using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Do something when pointer enters
        // e.g. play a sound
    }

    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
    }

}
