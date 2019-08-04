using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    

    public Vector2 orientation;

    public float moveSpeed;

    Vector2 inputDirection = Vector2.zero;
    Vector2 nextDirection;

    Animator anim;

    private Node currentNode,previousNode, endPointNode;

    private Node startPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        Node node = getNodePosition(transform.localPosition);

        Debug.Log("before assignment "+node);

        startPos = node;

        if (node != null) {

            currentNode = node;

            Debug.Log("current node" + currentNode);
        }

        inputDirection = Vector2.left;
        orientation = Vector2.left;



        changeDirection(inputDirection);

     
    }

    public void Restart() {
        /*transform.position = startPos.transform.position;
        currentNode = startPos;
        inputDirection = Vector2.left;
        orientation = Vector2.left;
        nextDirection = Vector2.left;
        changeDirection(inputDirection);
        //ABOVE STUFF IS FOR RESETTING POSITION OF THE PLAYER


        anim.SetBool("Death", true);
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        //ABOVE STUFF IS FOR SETTING PLAYER TO DEATH ANIMATION
        */

        print("____PROMPT TO RESTART SCENE/ENDGAME/LOSELIFE__");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
        }

        setDirection();

        move();

        UpdateOrientation(); 

        collectGrass();
    }

    void setDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
                
            transform.localScale = new Vector3(1,1,1);
            changeDirection(Vector2.up);
            orientation = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            transform.localScale = new Vector3(1, 1, 1);
            changeDirection(Vector2.down);
            orientation = Vector2.down;

            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.localScale = new Vector3(-1, 1, 1);
            changeDirection(Vector2.left);
            orientation = Vector2.left;

            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){   
            transform.localScale = new Vector3(1, 1, 1);
            changeDirection(Vector2.right);
            orientation = Vector2.right;
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

    void UpdateOrientation() {
        if (inputDirection == Vector2.left)
        {
            orientation = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
            
        }
        else if (inputDirection == Vector2.right)
        {
            orientation = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
            
        }
        else if (inputDirection == Vector2.up)
        {
            orientation = Vector2.up;
            transform.localScale = new Vector3(1, 1, 1);
            
        }
        else if (inputDirection == Vector2.down)
        {
            orientation = Vector2.down;
            transform.localScale = new Vector3(1, 1, 1);
            
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

