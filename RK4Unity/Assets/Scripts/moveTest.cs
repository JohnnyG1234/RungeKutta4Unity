using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class moveTest : MonoBehaviour
{
    public Vector3 move;
    public float speed = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Time.deltaTime * move.normalized * speed;
    }
}
