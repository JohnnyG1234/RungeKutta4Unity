using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    const float RAY_DIST = 0.5f;
    float playerColTime = .25f;

    private void Update()
    {
        playerColTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        RK4Position mover = this.gameObject.GetComponent<RK4Position>();
        List<KeyValuePair<float, Vector3>> faces = new List<KeyValuePair<float, Vector3>>(); 

        List<RaycastHit2D> hitsV = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsVX = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsH = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsHX = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false; 
        hitsV.AddRange(Physics2D.RaycastAll(transform.position + (transform.up).normalized * RAY_DIST, -transform.up.normalized, RAY_DIST * 2, ~(1 << 3)));
        hitsVX.AddRange(Physics2D.RaycastAll(transform.position + (transform.up + transform.right).normalized * RAY_DIST, -(transform.up + transform.right).normalized, RAY_DIST * 2, ~(1 << 3)));
        hitsH.AddRange(Physics2D.RaycastAll(transform.position + (transform.right).normalized * RAY_DIST, -transform.right.normalized, RAY_DIST * 2, ~(1 << 3)));
        hitsVX.AddRange(Physics2D.RaycastAll(transform.position + (transform.up + -transform.right).normalized * RAY_DIST, -(transform.up - transform.right).normalized, RAY_DIST * 2, ~(1 << 3)));
        

        if (col.gameObject.tag == "Player" & playerColTime > 0)
        {
            //return;
        }
        else if (col.gameObject.tag == "Player")
        {
            playerColTime = .25f;
        }

        foreach (RaycastHit2D hit1 in hitsV)
        {
            if (hit1.collider.gameObject != gameObject)
            {
                faces.Add(new KeyValuePair<float, Vector3>(hit1.distance, hit1.normal));
                //Debug.Log(hit1.normal);
                //break;
            }
        }

        foreach (RaycastHit2D hit2 in hitsH)
        {
            if (hit2.collider.gameObject != gameObject)
            {
                faces.Add(new KeyValuePair<float, Vector3>(hit2.distance, hit2.normal));
                //Debug.Log(hit2.normal);
                //break;
            }
        }

        foreach (RaycastHit2D hit3 in hitsVX)
        {
            if (hit3.collider.gameObject != gameObject)
            {
                faces.Add(new KeyValuePair<float, Vector3>(hit3.distance, hit3.normal));
                //Debug.Log(hit3.normal);
                //break;
            }
        }

        foreach (RaycastHit2D hit4 in hitsHX)
        {
            if (hit4.collider.gameObject != gameObject)
            {
                faces.Add(new KeyValuePair<float, Vector3>(hit4.distance, hit4.normal));
                //Debug.Log(hit4.normal);
                //break;
            }
        }

        KeyValuePair<float, Vector3> save = new KeyValuePair<float, Vector3>(float.MaxValue, Vector3.zero);
        Vector3 face = Vector3.zero;
        foreach(KeyValuePair<float, Vector3> f in faces)
        {
            Debug.Log(f.Key);
            if(save.Key > f.Key && (f.Key > 0 || save.Key == float.MaxValue))
            {
                save = f;
            }
        }
        face = save.Value;

        Debug.Log("f: " + face + " dir: " + mover.direction);

        Vector3 current = this.gameObject.GetComponent<RK4Position>().direction;
        mover.direction = current - 2 * Vector3.Dot(current, face) * face;

        WallCollider wall = col.gameObject.GetComponent<WallCollider>();
        if(wall)
        {
            mover.speed *= 1 - wall.Friction;
        }
        for(int i = 0; i < 1; i++)
        {
            mover.RK4();
        }
    }
}
