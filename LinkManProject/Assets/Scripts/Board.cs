using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    private static int width = 28;
    private static int height = 36;

    public GameObject[,] board = new GameObject[width,height];
    // Start is called before the first frame update
    void Start()
    {
        Object [] objects = GameObject.FindObjectsOfType(typeof(GameObject));

            foreach (GameObject o in objects) {

                Vector2 pos = o.transform.position;

            if (o.name != "LinkPlaceHolder")
            {

                board[(int)pos.x, (int)pos.y] = o;

            }

            else {

                Debug.Log("Found Player" + pos);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
