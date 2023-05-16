using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvas : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Image blur;
    [SerializeField] private Image progress;
    [SerializeField] private Text scoreProgress;
    [SerializeField] private Text attemptProgress;
    [SerializeField] private Text levelProgress;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponentInChildren<Text>();

        Image[] images = GetComponentsInChildren<Image>();

        foreach(Image image in images)
        {
            if (image.name.Equals("blur"))
                blur = image;
            else if (image.name.Equals("progress"))
                progress = image;
        }

        Text[] components = progress.GetComponentsInChildren<Text>();

        foreach(Text text in components)
        {
            if (text.name.Equals("Level"))
                levelProgress = text;
            else if (text.name.Equals("Attempt"))
                attemptProgress = text;
            else if (text.name.Equals("Score"))
                scoreProgress = text;
        }
    }

    //sets the score
    public void SetScore(int score)
    {
        if (this.score == null)
            throw new System.ArgumentNullException("", "there is no score object");

        this.score.text = "Score: " + score;
    }

    //makes the image blury
    public void SetBlur()
    {
        blur.transform.position = new Vector3(320, 480, 0);
    }

    //makes the progress visible
    public void SetProgress()
    {
        progress.transform.position = new Vector3(320, 480, 0);
    }

    //sets the level in the progress
    public void SetLevel(int level)
    {
        levelProgress.text = "Level " + level;
    }

    //sets the score in the progress
    public void SetScoreProgress(int score)
    {
        scoreProgress.text = "Score: " + score;
    }

    //sets the lives in the progress
    public void SetLives(int lives)
    {
        attemptProgress.text = "Lives: " + lives;
    }


    //resets the canvas
    public void Reset()
    {
        progress.transform.position = new Vector3(10000, 10000, 0);
        blur.transform.position = new Vector3(10000, 10000, 0);
    }

}
