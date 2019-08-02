using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;

    Vector2 inputDirection = Vector2.zero;
    Vector2 nextDirection;


    private Node currentNode,previousNode, endPointNode;

    // Start is called before the first frame update
    void Start()
    {
        Node node = getNodePosition(transform.localPosition);

        Debug.Log("before assignment "+node);

        if (node != null) {

            currentNode = node;

            Debug.Log("current node" + currentNode);
        }

        inputDirection = Vector2.left;
        changeDirection(inputDirection);

     
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();

        move();


        collectGrass();
    }

    void setDirection()
        {



            if (Input.GetKeyDown(KeyCode.UpArrow)) {

                transform.localScale = new Vector3(1,1,1);
                transform.localRotation = Quaternion.Euler(0,0,90);

            changeDirection(Vector2.up);
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
            

                transform.localScale = new Vector3(1, 1, 1);
                transform.localRotation = Quaternion.Euler(0, 0, -90);

                changeDirection(Vector2.down);
        }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                

                transform.localScale = new Vector3(-1, 1, 1);
                transform.localRotation = Quaternion.Euler(0, 0, 180);

                changeDirection(Vector2.left);
        }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {   

                transform.localScale = new Vector3(1, 1, 1);
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            changeDirection(Vector2.right);
        }
        }

    void changeDirection(Vector2 newDirection) {

        if (newDirection != inputDirection) {

            nextDirection = newDirection;

            Node destinationNode = canMove(newDirection);

            if (destinationNode != null) {

                inputDirection = newDirection;
                endPointNode = destinationNode;
                previousNode = currentNode;
                currentNode = null;

            }

        }

    }

    Node canMove(Vector2 direction) {

        Node newDestination = null;

        for (int i = 0; i < currentNode.destinations.Length;i ++) {

            if (currentNode.directionOptions[i] == direction) {


                newDestination = currentNode.destinations[i];
                break;
            }


           
        }


        return newDestination;

    }

    void moveToNode(Vector2 chosenDirection) {

        Node destinationNode = canMove(chosenDirection);

        if (destinationNode != null){

            transform.localPosition = destinationNode.transform.position;
            currentNode = destinationNode;

            
        }

    }

    private void move()
        {

        if(endPointNode != currentNode && endPointNode != null)
        {

            if (nextDirection == inputDirection * -1) {

                inputDirection *= -1;

                Node tempNode = endPointNode;

                endPointNode = previousNode;

                previousNode = tempNode;
            }

            if (overShotDestination())
            {

                currentNode = endPointNode;

                transform.localPosition = currentNode.transform.position;

                Node moveToNode = canMove(nextDirection);

                if (moveToNode != null)
                {

                    inputDirection = nextDirection;

                }

                if (moveToNode == null)
                {

                    moveToNode = canMove(inputDirection);

                }

                if (moveToNode != null)
                {

                    endPointNode = moveToNode;
                    previousNode = currentNode;
                    currentNode = null;

                }

                else { inputDirection = Vector2.zero; }

            }

            else { transform.localPosition += (Vector3)(inputDirection * moveSpeed) * Time.deltaTime;


            }

        }

       

        }

    Node getNodePosition(Vector2 position) {

        GameObject tile = GameObject.Find("GameBoard").GetComponent<Board>().board [(int) position.x,(int) position.y];

        Debug.Log(tile);

        if (tile != null) {

            Debug.Log("returning");
               

            return tile.GetComponent<Node>();

            
        }
        Debug.Log("no tile");

        return null;
    }

    void collectGrass() {

        GameObject o = getTilePosition(transform.position);


        if (o != null) {

            Tile tile = o.GetComponent<Tile>();

            if (tile != null) {

                if (!tile.collected && tile.isGrass) {

                    o.GetComponent<SpriteRenderer>().enabled = false;
                    tile.collected = true;

                }


            }

        }

    }

    GameObject getTilePosition(Vector2 pos) {

        int tileX = Mathf.RoundToInt(pos.x);
        int tileY = Mathf.RoundToInt(pos.y);

        GameObject tile = GameObject.Find("GameBoard").GetComponent<Board>().board[tileX,tileY];

       

        if (tile != null) {
            return tile;
        }

        return null;

    }

    float distanceToNode(Vector2 targetNode) {

        Vector2 temp = targetNode - (Vector2) previousNode.transform.position;


        return temp.sqrMagnitude;
    }

    bool overShotDestination() {

        float nodeToTarget = distanceToNode(endPointNode.transform.position);
        float nodeToSelf = distanceToNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }
    

    
    }

