using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RK4Position : MonoBehaviour
{
    public float speed;
    public Vector2 direction = new Vector2(1, 1);

    private void FixedUpdate()
    {
        RK4();
    }

    public void RK4()
    {
        direction.Normalize();
        float k1x = speed * Time.fixedDeltaTime * direction.x;
        float k1y = speed * Time.fixedDeltaTime * direction.y;

        
        float k2x = speed * (k1x * Time.fixedDeltaTime / 2);
        float k2y = speed * (k1y * Time.fixedDeltaTime / 2);

        
        float k3x = speed * (k2x * Time.fixedDeltaTime / 2);
        float k3y = speed * (k2y * Time.fixedDeltaTime / 2);
        
        float k4x = speed * Time.fixedDeltaTime * k3x;
        float k4y = speed * Time.fixedDeltaTime * k3y;
  

        float finalX = gameObject.transform.position.x + Time.fixedDeltaTime / 6 * (k1x + 2 * k2x + 2 * k3x + k4x);
        float finalY = gameObject.transform.position.y + Time.fixedDeltaTime / 6 * (k1y + 2 * k2y + 2 * k3y + k4y);

        Vector2 newPos = new Vector2(finalX, finalY);
        newPos = newPos.IsUnityNull() == false ? newPos : gameObject.transform.position;
        gameObject.transform.position = newPos;
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (transform.position + new Vector3(direction.x, direction.y, 0)));
    }


}
