using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Node[] destinations;
    public Vector2[] directionOptions;

    // Start is called before the first frame update
    void Start()
    {
        directionOptions = new Vector2[destinations.Length];

        for (int i = 0; i < destinations.Length; i++) {

            Node neighbour = destinations[i];

            Vector2 addedDirOption = neighbour.transform.localPosition - transform.localPosition;

            directionOptions[i] = addedDirOption.normalized;

        }
    }

    // Update is called once per frame
    
}
