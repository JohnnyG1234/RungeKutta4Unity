using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    const float RAY_DIST = 3f;
    float playerColTime = .25f;

    private void Update()
    {
        playerColTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        RK4Position mover = this.gameObject.GetComponent<RK4Position>();
        List<Vector3> faces = new List<Vector3>();

        List<RaycastHit2D> hitsUP = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsDOWN = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsLEFT = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsRIGHT = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false; 
        hitsUP.AddRange(Physics2D.RaycastAll(transform.position, transform.up, RAY_DIST));
        hitsDOWN.AddRange(Physics2D.RaycastAll(transform.position, -transform.up, RAY_DIST));
        hitsLEFT.AddRange(Physics2D.RaycastAll(transform.position, transform.right, RAY_DIST));
        hitsRIGHT.AddRange(Physics2D.RaycastAll(transform.position, -transform.right, RAY_DIST));
        float saveDist = float.MaxValue;


        if (col.gameObject.tag == "Player" & playerColTime > 0)
        {
            return;
        }
        else if (col.gameObject.tag == "Player")
        {
            playerColTime = .25f;
        }

        foreach(RaycastHit2D hit in hitsUP)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        foreach (RaycastHit2D hit in hitsDOWN)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        foreach (RaycastHit2D hit in hitsLEFT)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        foreach (RaycastHit2D hit in hitsRIGHT)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        
        Vector3 face = Vector3.zero;
        foreach(Vector3 f in faces)
        {
            face += f;
        }
        face /= faces.Count;

        Debug.Log("face: " + face);
        Vector3 current = this.gameObject.GetComponent<RK4Position>().direction;

        mover.direction = current - 2 * Vector3.Dot(current, face) * face;
        WallCollider wall = col.gameObject.GetComponent<WallCollider>();
        if(wall)
        {
            mover.speed *= 1 - wall.Friction;
        }

        mover.RK4();
    }
}
