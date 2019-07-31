using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GM : MonoBehaviour
{
    //public variables
    public int score;
    public int health;
    public TextMesh scoreText;
    public TextMesh healthText; 
    public static GM instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            HealthLoss();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            HealthGain();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ScoreGain();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }

    }

    public void HealthLoss()
    {
        health--;
        healthText.text = " " + health;
    }

    public void HealthGain()
    {
        health++;
        healthText.text = " " + health;
    }

    public void ScoreGain ()
    {
        score++;
        scoreText.text = " " + score;
    }

    public void Exit ()
    {
        //are u sure you want to quit
        Application.Quit();
    }
}
