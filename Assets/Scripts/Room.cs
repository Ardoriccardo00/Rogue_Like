using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    [SerializeField] GatePoint[] gatePoint;

    void Start()
    {
        gatePoint = GetComponentsInChildren<GatePoint>();
    }

    void Update()
    {
        transform.name = ("" + transform.position.x + ";" + transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
