using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RK4Position : MonoBehaviour
{
    [SerializeField]
    private float velocity = 1000;
    private Vector2 direction = new Vector2(1, 1);

    private void FixedUpdate()
    {
        RK4();
    }

    private void RK4()
    {
       
        float k1x = velocity * Time.deltaTime * direction.x;
        float k1y = velocity * Time.deltaTime * direction.y;

        
        float k2x = velocity * (k1x * Time.deltaTime / 2) * direction.x;
        float k2y = velocity * (k1y * Time.deltaTime / 2) * direction.x;

        
        float k3x = velocity * (k2x * Time.deltaTime / 2) * direction.x;
        float k3y = velocity * (k2y * Time.deltaTime / 2) * direction.x;
        
        float k4x = velocity * Time.deltaTime * direction.x * k3x;
        float k4y = velocity * Time.deltaTime * direction.y * k3y;
  

        float finalX = gameObject.transform.position.x + Time.deltaTime / 6 * (k1x + 2 * k2x + 2 * k3x + k4x);
        float finalY = gameObject.transform.position.y + Time.deltaTime / 6 * (k1y + 2 * k2y + 2 * k3y + k4y);

        gameObject.transform.position = new Vector2(finalX, finalY);
    }



}
