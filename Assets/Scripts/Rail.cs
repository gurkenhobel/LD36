using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rail : MonoBehaviour
{
    protected Vector3 position;
    protected RailDirection direction;
    private readonly GameObject cellObject;

     public enum RailDirection
    {
        Left = 1,
        Right = 2,
        Top = 4,
        Bottow = 8
    }

    public Rail(Transform parent, GameObject prefab, Vector2 pos)
    {
        position = new Vector3(pos.x, 0, pos.y);
        cellObject = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity) as GameObject;
        cellObject.transform.SetParent(parent);
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public List<Waypoint> GetWaypoints()
    {
        var waypoints = gameObject.GetComponentsInChildren<Waypoint>();
        return waypoints.ToList();
    }
}

