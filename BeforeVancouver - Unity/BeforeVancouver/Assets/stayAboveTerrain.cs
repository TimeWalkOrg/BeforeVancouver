using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayAboveTerrain : MonoBehaviour
{
    public float yAddition;
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + yAddition;
        transform.position = pos;
    }
}


