using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject link;

    private static int width = 28;
    private static int height = 36;

    Object[] grass;

    AudioSource[] sounds;

    AudioSource deathSound;
    AudioSource winSound;

    int scoreCounter;

    int uncollectedGrass;

    public int lives = 3;

    public GameObject[,] board = new GameObject[width,height];
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();

        deathSound = sounds[0];
        winSound = sounds[1];

        scoreCounter = 0;

        link = GameObject.FindGameObjectWithTag("Link");

        grass = GameObject.FindObjectsOfType(typeof(Tile));

        Object [] objects = GameObject.FindObjectsOfType(typeof(GameObject));

            foreach (GameObject o in objects) {

                Vector2 pos = o.transform.position;

            if (o.name != "LinkPlaceHolder" && o.name != "NodeHolder" && o.name!= "GrassHolder" && o.name!="WallHolder" && o.name!="InteractableObjects" && o.tag != "Chuchu")
            {

                board[(int)pos.x, (int)pos.y] = o;

            }

            else {

                Debug.Log("Found Player" + pos);
            }

        }
    }

    void Update()
    {
        if (lives <= 0)
        {
            print("GAME ENDED; Player Loses, can add end screen at this point");
        }

        uncollectedGrass = 0;
        scoreCounter = 0;

        foreach (Tile o in grass)
        {
            if (!(o.collected))
            {
                uncollectedGrass++;

            }

            else if (o.isRupee == true) {

                scoreCounter = scoreCounter + 6;

            }

            else
            {
                scoreCounter++;
            }
        }

        print("SCORE COUNTER: " + scoreCounter * 50);

        if (uncollectedGrass == 0)
        {
            print("GAME ENDED; PLAYER WINS!!");

        }

    }

    public void startDeath()
    {
        link.GetComponent<Movement>().Death();
        Destroy(link.GetComponent<Movement>());

        deathSound.Play(0); 

        StartCoroutine(death());
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(2);
        Restart();
    }

    public void Restart() {
        lives--;

        GameObject link = GameObject.FindGameObjectWithTag("Link");

        GameObject[] o = GameObject.FindGameObjectsWithTag("Chuchu");

        link.transform.position = new Vector3(2f,8f,0);
        link.AddComponent<Movement>();

        foreach (GameObject chuchu in o)
        {
            chuchu.transform.GetComponent<Ghost>().Restart();
        }
    }

}
