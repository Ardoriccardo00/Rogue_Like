using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GatePointType
{
    top,
    bottom,
    left,
    right,
    none
}

[ExecuteInEditMode]
public class GatePoint : MonoBehaviour
{
    public GatePointType gatePointType = GatePointType.none;

    void Update()
    {
        transform.name = Convert.ToString(gatePointType);
    }
}
