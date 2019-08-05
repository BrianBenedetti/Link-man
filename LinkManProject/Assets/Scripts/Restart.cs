using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{

    Vector3 startpos;

    public void death() {

        GetComponent<Movement>().enabled = false;

    }

    public void restart() {

        transform.position = new Vector3 (7f,8f,0f);


        GetComponent<Movement>().enabled = true;

    }
    // Start is called before the first frame update
    void Start()
    {

        startpos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
