using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public float speed = 2f;
    public GameObject quitMenu;
    public GameObject cursor;
    public Transform yesText;
    public Transform noText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verTranslation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float horTranslation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(horTranslation, verTranslation, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Start" && Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            /*speed = 0f;
            print("collided");
           if ()
            {
               
            }*/
        }
       /* else
        {
            speed = 2f;/
        }*/

        if (other.gameObject.tag == "Quit" && Input.GetKey(KeyCode.Space))
        {
            ActivateQuitMenu();
            /*speed = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                
            }*/
        }
       /* else
        {
            speed = 2f;
        }*/
    }

    void ActivateQuitMenu()
    {
        quitMenu.SetActive(true);
    }

    void QuitChoice()
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
        if (cursor.transform.position == yesText.transform.position && Input.GetKeyDown(KeyCode.Space))
        {
            Application.Quit();
            print("quit");
        }
    }
}
