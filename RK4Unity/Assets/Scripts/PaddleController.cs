using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    private Vector2 _direction;

    public bool player1;

    private void FixedUpdate()
    {
        float finalX = gameObject.transform.position.x + _direction.x * speed * Time.fixedDeltaTime;
        float finalY = gameObject.transform.position.y + _direction.y * speed * Time.fixedDeltaTime;

        gameObject.transform.position = new Vector2(finalX, finalY);
    }

    private  void Update()
    {
        //_direction.x = Input.GetAxis("Horizontal");
        //_direction.y = Input.GetAxis("Vertical");

        if(player1)
        {
            if(Input.GetKey(KeyCode.D))
            {
                _direction.x = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _direction.x = -1;
            }
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                _direction.x = 0;
            }

            if (Input.GetKey(KeyCode.W))
            {
                _direction.y = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _direction.y = -1;
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                _direction.y = 0;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _direction.x = 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _direction.x = -1;
            }
            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                _direction.x = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                _direction.y = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _direction.y = -1;
            }
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                _direction.y = 0;
            }
        }
    }
}
