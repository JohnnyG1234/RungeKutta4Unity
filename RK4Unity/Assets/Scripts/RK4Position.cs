using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RK4Position : MonoBehaviour
{
    public void Slope()
    {
        //the equasion that perdicts movment
    }
    private void UpdatePosition(Action Slope)
    {
        //do runga kutta based on given slope function
    }

    private void FixedUpdate()
    {
        UpdatePosition(Slope);
    }
}
