using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class InfoText
{
    public string text;
    public float timer;

    public InfoText(string text, float timer)
    {
        this.text = text;
        this.timer = timer;
    }
};

public class InfoBoard : MonoBehaviour
{
    public Text timedText;         // Text that is displayed for a brief moment
    public Text displayText;       // Text that is displayed when a condition is met
    public float letterDelay = 0.1f;

    #region Singleton
    public static InfoBoard instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private Queue<InfoText> infoTextQueue = new Queue<InfoText>();
    private bool isDisplayingText = false;

    private void Start()
    {
        // For testing
        //TestBoard();
    }

    private void TestBoard()
    {
        AddText("Welcome");
        AddText("Current Mission:", 5.0f);
        AddText("Reach the goal", 3.0f);
        AddText("Defeat the enemy", 2.0f);
        AddText("Survive..", 10.0f);
    }

    private void Update()
    {
        if (infoTextQueue.Count > 0)
        {
            // Get new text only when current text is done being displayed, based on their timers
            if (!isDisplayingText)
            {
                InfoText infoText = infoTextQueue.Dequeue();
                StartCoroutine(DisplayAndWait(infoText));
            }
        }
    }

    public void AddText(string text)
    {
        InfoText newInfoText = new InfoText(text, 3.0f);
        infoTextQueue.Enqueue(newInfoText);
    }

    public void AddText(string text, float timer)
    {
        InfoText newInfoText = new InfoText(text, timer);
        infoTextQueue.Enqueue(newInfoText);
    }

    public void DisplayText(string text)
    {
        displayText.text = text;
    }

    private IEnumerator DisplayAndWait(InfoText infoText)
    {
        // Clear any old text before display the new text
        timedText.text = "";
        // Display the text letter-by-letter
        isDisplayingText = true;
        foreach (char letter in infoText.text)
        {
            // TODO: play some sound when a new letter is appended
            timedText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(infoText.timer);

        // Clear the text after the specified time
        timedText.text = "";
        isDisplayingText = false;
    }
}
