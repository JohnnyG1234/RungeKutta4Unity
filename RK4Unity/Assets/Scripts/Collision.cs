using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    const float RAY_DIST = 0.4f;
    float playerColTime = .25f;

    private void Update()
    {
        playerColTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        RK4Position mover = this.gameObject.GetComponent<RK4Position>();
        List<Vector3> faces = new List<Vector3>();

        List<RaycastHit2D> hitsV = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsVX = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsH = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsHX = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false; 
        hitsV.AddRange(Physics2D.RaycastAll(transform.position + (transform.up * RAY_DIST), -transform.up, RAY_DIST * 2));
        hitsVX.AddRange(Physics2D.RaycastAll(transform.position + (transform.up * RAY_DIST) + (transform.right * RAY_DIST), -(transform.up + transform.right).normalized, RAY_DIST * 2));
        hitsH.AddRange(Physics2D.RaycastAll(transform.position + (transform.right * RAY_DIST), -transform.right, RAY_DIST * 2));
        hitsVX.AddRange(Physics2D.RaycastAll(transform.position + (transform.up * RAY_DIST) + (-transform.right * RAY_DIST), -(transform.up - transform.right).normalized, RAY_DIST * 2));
        float saveDist = float.MaxValue;

        if (col.gameObject.tag == "Player" & playerColTime > 0)
        {
            //return;
        }
        else if (col.gameObject.tag == "Player")
        {
            playerColTime = .25f;
        }

        foreach(RaycastHit2D hit in hitsV)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        foreach (RaycastHit2D hit in hitsH)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        
        foreach (RaycastHit2D hit in hitsVX)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < saveDist)
            {
                faces.Add(hit.normal);
                saveDist = hit.distance;
            }
        }
        foreach (RaycastHit2D hit in hitsHX)
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

        face = new Vector3((face.x == 1f ? 0.9f : face.x), (face.y == 1f ? 0.9f : face.y), (face.z == 1f ? 0.9f : face.z));

        Debug.Log("face: " + face);
        Vector3 current = this.gameObject.GetComponent<RK4Position>().direction;

        mover.direction = current - 2 * Vector3.Dot(current, face) * face;
        WallCollider wall = col.gameObject.GetComponent<WallCollider>();
        if(wall)
        {
            mover.speed *= 1 - wall.Friction;
        }
        for(int i = 0; i < 3; i++)
        {
            mover.RK4();
        }
    }
}
