using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        moveTest mover = this.gameObject.GetComponent<moveTest>();
        Vector3 face = Vector3.zero;
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(transform.position, mover.move))
        {
            Debug.Log("hit");
            if(hit.collider.gameObject != gameObject)
            {
                face = hit.normal;
            }
        }
        Vector3 current = this.gameObject.GetComponent<moveTest>().move;

        mover.move = current - 2 * Vector3.Dot(current, face) * face;
        WallCollider wall = col.gameObject.GetComponent<WallCollider>();
        if(wall)
        {
            mover.speed *= 1 - wall.Friction;
        }
    }
}
