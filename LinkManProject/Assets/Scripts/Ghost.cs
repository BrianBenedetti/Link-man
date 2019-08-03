using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float mSpeed = 1f;

    public Node startPos;

    public int scatterModeTimer1 = 7;
    public int chaseModeTimer1 = 20;
    public int scatterModeTimer2 = 7;
    public int chaseModeTimer2 = 20;
    public int scatterModeTimer3 = 5;
    public int chaseModeTimer3 = 20;
    public int scatterModeTimer4 = 5;

    private int modeChangeIterations = 1;
    private float modeChangeTimer = 0;

    public enum Mode {
        Chase,
        Scatter
    }

    Mode currentMode = Mode.Scatter;
    Mode prevMode;

    private GameObject link;

    private Node currentNode;
    private Node targetNode;
    private Node PreviousNode;

    private Vector2 direction;
    private Vector2 nextDirection;

    // Start is called before the first frame update
    void Start()
    {
        link = GameObject.FindGameObjectWithTag("Link");

        Node node = startPos;

        print("____________Saatwik DEBUG___________");
        print("StartNode: " + node);

        if (node != null) {
            currentNode = node;
        }

        direction = Vector2.right;

        PreviousNode = currentNode;

        Vector2 linkPos = link.transform.position;

        Vector2 targetTile = new Vector2(Mathf.RoundToInt(linkPos.x), Mathf.RoundToInt(linkPos.y));

        targetNode = getNodeAtPosition(targetTile);
    }

    // Update is called once per frame
    void Update()
    {
        ModeUpdate();

        Move();
    }

    public void Move()
    {
        print("Target Node: "+targetNode);

        if (targetNode != currentNode && targetNode != null)
        {
            if (OverShotTarget())
            {

                currentNode = targetNode;

                transform.localPosition = currentNode.transform.position;

                targetNode = chooseNextNode();

                PreviousNode = currentNode;

                currentNode = null;
            }

            else
            {
                transform.position += (Vector3)direction * mSpeed * Time.deltaTime;
            }
        }
    }

    void ModeUpdate() {
        modeChangeTimer += Time.deltaTime;
        if (modeChangeIterations == 1)
        {
            if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer1)
            {
                changeMode(Mode.Chase);
                modeChangeTimer = 0;
            }

            if (currentMode == Mode.Chase && modeChangeTimer > scatterModeTimer1)
            {
                modeChangeIterations = 2;
                changeMode(Mode.Scatter);
                modeChangeTimer = 0;
            }
        }
        else if (modeChangeIterations == 2)
        {
            if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer2)
            {
                changeMode(Mode.Chase);
                modeChangeTimer = 0;
            }

            if (currentMode == Mode.Chase && modeChangeTimer > scatterModeTimer2)
            {
                modeChangeIterations = 3;
                changeMode(Mode.Scatter);
                modeChangeTimer = 0;
            }
        }
        else if (modeChangeIterations == 3)
        {
            if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer3)
            {
                changeMode(Mode.Chase);
                modeChangeTimer = 0;
            }

            if (currentMode == Mode.Chase && modeChangeTimer > scatterModeTimer3)
            {
                modeChangeIterations = 4;
                changeMode(Mode.Scatter);
                modeChangeTimer = 0;
            }
        }
        else if (modeChangeIterations == 4)
        {
            if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer4)
            {
                changeMode(Mode.Chase);
                modeChangeTimer = 0;
            }
        }
    }

    public void changeMode(Mode m) {
        currentMode = m;
    }

    public Node chooseNextNode() {
        Vector2 targetTile = Vector2.zero;

        Vector2 linkPos = link.transform.position;
        targetTile = new Vector2(Mathf.RoundToInt(linkPos.x), Mathf.RoundToInt(linkPos.y));

        Node moveToNode = null;

        Node[] foundNodes = new Node[4];
        Vector2[] foundNodesDirection = new Vector2[4];

        int nodeCounter = 0;

        for (int i = 0; i < currentNode.destinations.Length; i++)
        {
            if (currentNode.directionOptions[i] != direction * -1)
            {
                foundNodes[nodeCounter] = currentNode.destinations[i];
                foundNodesDirection[nodeCounter] = currentNode.directionOptions[i];
                nodeCounter++;
            }
        }

        if (foundNodes.Length == 1)
        {
            moveToNode = foundNodes[0];
            direction = foundNodesDirection[0];
        }

        if (foundNodes.Length > 1)
        {
            float leastDistance = 1000000f;
            for (int i = 0; i < foundNodes.Length; i++)
            {
                

                if (foundNodesDirection[i] != Vector2.zero)
                {
                    float distance = shortestRoute(foundNodes[i].transform.position, targetTile);

                    if (distance < leastDistance)
                    {
                        leastDistance = distance;
                        moveToNode = foundNodes[i];
                        direction = foundNodesDirection[i];
                    }
                }
            }
        }

        return moveToNode;
    }


    public Node getNodeAtPosition(Vector2 pos)
    {
        print("Position:"+pos);

        GameObject tile = GameObject.Find("GameBoard").GetComponent<Board>().board[(int)pos.x, (int)pos.y];

<<<<<<< HEAD
        print("Tile:" +tile);

        if (tile != null)
        {
            return tile.GetComponent<Node>();
            print("Tile Found" + tile.GetComponent<Node>());
=======

        print("Node at Position "+ pos+":" +tile);

        if (tile != null)
        {
            print("Tile Name: " + tile.name);
            print("Node Components: " + tile.GetComponent<Node>().destinations);
            return tile.gameObject.GetComponent<Node>();
>>>>>>> 9c82f3cf5bf0d68daea67d1c0cdcc979d0f95000
        }
        print("no tile");
        return null;
    }

    public float LengthFromNode(Vector2 targetPos)
    {
        Vector2 vec = targetPos - (Vector2)PreviousNode.transform.position;
        return vec.sqrMagnitude;
    }

    bool OverShotTarget() {
        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float nodeToSelf = LengthFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float shortestRoute(Vector2 posA, Vector2 posB)
    {
        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        float distance = Mathf.Sqrt(dx * dx + dy * dy);
        return distance;
    }


}
