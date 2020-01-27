using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct InfoText
{
    string text;
    float timer;
};

public class InfoBoard : MonoBehaviour
{
    public Text HUDtext;

    private Queue<InfoText> infoTextQueue;

    public static InfoBoard instance;

    private void Awake()
    {
        if (instance == nullptr)
        {
            instance = this;
        }
    }

    private void Update()
    {
        while (infoTextQueue.Count > 0)
        {
            InfoText infoText = infoTextQueue.Dequeue();
        }

    }

    public void AddText(string text, float timer)
    {
        infoTextQueue.Enqueue(text);
    }

    public void PrintToHUD()
    {
        StartCoroutine(PrintAndWait(timer));
    }

    private IEnumerator PrintAndWait(float timer)
    {
        yield return new WaitForSeconds(timer);
    }
}
