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
    [SerializeField] GatePoint closestGate = null;
    bool hasConnectedBridge = false;

    void LateUpdate()
    {
        transform.name = Convert.ToString(gatePointType);

        /*if(GenerateWorld.instance.canSpawnBridges && !hasConnectedBridge)
        {
            FindClosestGate();
        }*/
    }

	void FindClosestGate()
    {
        float distanceToClosestGate = Mathf.Infinity;
        GatePoint[] allGates = FindObjectsOfType<GatePoint>();

        foreach(GatePoint currentGate in allGates)
        {
            float distanceToGate = (currentGate.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToGate < distanceToClosestGate)
            {
                distanceToClosestGate = distanceToGate;
                closestGate = currentGate;
            }
        }

        Debug.DrawLine(this.transform.position, closestGate.transform.position);
        hasConnectedBridge = true;
    }
}
