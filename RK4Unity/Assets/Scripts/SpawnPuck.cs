using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPuck : MonoBehaviour
{
    public GameObject puck;
    
    public void spawnPuck()
    {
        Instantiate(puck, transform.position, transform.rotation);
    }
}
