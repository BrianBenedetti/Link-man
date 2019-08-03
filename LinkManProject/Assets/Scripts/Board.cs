using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    private static int width = 28;
    private static int height = 36;

    public int lives = 3;

    public GameObject[,] board = new GameObject[width,height];
    // Start is called before the first frame update
    void Start()
    {
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

    public void Restart() {
        lives--;

        GameObject link = GameObject.FindGameObjectWithTag("Link");

        link.GetComponent<Movement>().Restart();

        GameObject[] o = GameObject.FindGameObjectsWithTag("Chuchu");

        foreach (GameObject chuchu in o)
        {
            chuchu.transform.GetComponent<Ghost>().Restart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
