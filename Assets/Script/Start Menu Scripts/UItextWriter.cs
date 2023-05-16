using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UItextWriter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] string welcometext;
    [SerializeField] string commandtext;
    [SerializeField] string jokeText;

    //all courantines needed for the code to work.
    IEnumerator welcomeCoroutine;
    IEnumerator commandCoroutine;
    IEnumerator jokeCoroutine;
    bool welcomeCoroutineStarted;
    bool commandCoroutineStarted;
    bool jokeCoroutineStarted;

    void Start()
    {
        Screen.SetResolution(640, 960, true);

        welcomeCoroutine = WriteAndCommand(0.1f, welcometext);
        commandCoroutine = Write(0.1f, commandtext);
        jokeCoroutine = WriteAndCommand(0.1f, jokeText);

        StartCoroutine(welcomeCoroutine);
        welcomeCoroutineStarted = true;
    }

    //simply writes a text and then stops one of the courantines that has started writing it.
    public IEnumerator Write(float speed, string text)
    {
        yield return new WaitForSeconds(2);
        textMesh.text = "";

        foreach (char ch in text)
        {
            textMesh.text += ch;
            yield return new WaitForSeconds(speed);
        }

        if (welcomeCoroutineStarted)
            welcomeCoroutineStarted = false;

        else if (commandCoroutineStarted)
            commandCoroutineStarted = false;

        else if (jokeCoroutineStarted)
            jokeCoroutineStarted = false;
    }

    //Writes a text and then executes the command coroutine.
    public IEnumerator WriteAndCommand(float speed, string text)
    {
        yield return new WaitForSeconds(1);
        textMesh.text = "";

        foreach (char ch in text)
        {
            textMesh.text += ch;
            yield return new WaitForSeconds(speed);
        }

        if (welcomeCoroutineStarted)
            welcomeCoroutineStarted = false;

        else if (commandCoroutineStarted)
            commandCoroutineStarted = false;

        else if (jokeCoroutineStarted)
            jokeCoroutineStarted = false;

        StartCoroutine(commandCoroutine);
        commandCoroutineStarted = true;
    }

    //about buttons:
    public void OnPressStart()
    {
        StopCoroutine(welcomeCoroutine);
        StopCoroutine(commandCoroutine);
        welcomeCoroutineStarted = false;
        commandCoroutineStarted = false;
        GlobalSceneLoader.LoadNextScene();
    }

    public void OnPressQuit()
    {
        StopCoroutine(welcomeCoroutine);
        StopCoroutine(commandCoroutine);
        welcomeCoroutineStarted = false;
        commandCoroutineStarted = false;
        GlobalSceneLoader.QuitGame();
    }
    
    public void OnPressJoke()
    {
        if (!commandCoroutineStarted && !welcomeCoroutineStarted && !jokeCoroutineStarted)
        {
            jokeCoroutineStarted = true;
            textMesh.text = "";
            StartCoroutine(jokeCoroutine);
        }
    }
}
