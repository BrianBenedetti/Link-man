using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    //public variables
    public int score;
    public int health;
    public TextMesh scoreText;
    public TextMesh healthText;
    public bool paused = false;
    public GameObject pausedText;
    public GameObject WinText;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject quitMenu;
    public GameObject cursor;
    public Transform yesText;
    public Transform noText;
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
        if (Input.GetKeyDown(KeyCode.Z)) //stand in UI interactions
        {
            HealthLoss();
        }

        if (Input.GetKeyDown(KeyCode.X))//stand in UI interactions
        {
            HealthGain();
        }

        if (Input.GetKeyDown(KeyCode.C))//stand in UI interactions
        {
            ScoreGain();
        }

        PauseGame();

        if (Input.GetKeyDown(KeyCode.R))
        {
            restartText.SetActive(true);
            Invoke("ReloadGame", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateQuitMenu();
        }

        if (quitMenu.activeSelf == true)
        {
            QuitChoice();
        }

    }

    public void HealthLoss()
    {
        health--;
        healthText.text = " " + health;
        
        //losing
        if (health == 0)
        {
            gameOverText.SetActive(true);
        }
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

        //winning
        if (score == 20) //20 is random number for testing
        {
            WinText.SetActive(true);
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //"!" inverts boolean so if paused is true "!" inverts it to false. if pased is faalse, "!" inverts it to true. (thats why can press same button for pause & resume)
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0f;
            pausedText.SetActive(true);
        }

        else if (!paused)
        {
            Time.timeScale = 1f;
            pausedText.SetActive(false);
        }
    }

    void ActivateQuitMenu ()
    {
        quitMenu.SetActive(true);
    }

    void ReloadGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    void QuitChoice ()
    {
        //seclecting yes to quit
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cursor.transform.position = noText.position;
        }

        //seclecting no to quit
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cursor.transform.position = yesText.position;
        }

        //not quiting
        if (cursor.transform.position == noText.transform.position && Input.GetKeyDown(KeyCode.Space))
        {
            quitMenu.SetActive(false);
        }

        //confirming quiting
        if (cursor.transform.position == yesText.transform.position && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Application.Quit();
            print("quit");
        }
    }
}
