using System;
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

    GatePoint nearestGate = null;
    Room myRoom;

    void Start()
    {
        myRoom = GetComponentInParent<Room>();
        transform.name = Convert.ToString(gatePointType);
    }

    /*public void FindClosestGate()
    {
        foreach(GatePoint gate in myRoom.gatePoints)
        {
            float nearestDistance = float.MaxValue;
            nearestGate = null;

            foreach(GatePoint closeGate in myRoom.nearestGatePoints)
            {
                if(Vector2.Distance(transform.position, gate.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector2.Distance(transform.position, gate.transform.position);
                    nearestGate = gate;
                }
                Debug.DrawLine(transform.position, nearestGate.transform.position, Color.green);
            }
            
        }
    }*/
}
