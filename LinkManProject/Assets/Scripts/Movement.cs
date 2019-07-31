using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    Vector2 inputDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();

        move();

    }

    void setDirection()
        {

            if (Input.GetKeyDown(KeyCode.UpArrow)) {

                transform.localScale = new Vector3(1,1,1);
                inputDirection = Vector2.up;
                transform.localRotation = Quaternion.Euler(0,0,90);

            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.localScale = new Vector3(1, 1, 1);
                inputDirection = Vector2.down;
                transform.localRotation = Quaternion.Euler(0, 0, -90);

        }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                inputDirection = Vector2.left;
                transform.localRotation = Quaternion.Euler(0, 0, 180);

        }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.localScale = new Vector3(1, 1, 1);
                inputDirection = Vector2.right;
                transform.localRotation = Quaternion.Euler(0, 0, 0);

        }





        }

    private void move()
        {

        transform.localPosition += (Vector3) inputDirection * moveSpeed * Time.deltaTime;

        }
    

    
    }

