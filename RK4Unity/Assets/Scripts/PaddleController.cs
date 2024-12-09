using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    private Vector2 _direction;

    private void FixedUpdate()
    {
        float finalX = gameObject.transform.position.x + _direction.x * speed * Time.fixedDeltaTime;
        float finalY = gameObject.transform.position.y + _direction.y * speed * Time.fixedDeltaTime;

        gameObject.transform.position = new Vector2(finalX, finalY);
    }

    private  void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
    }
}
