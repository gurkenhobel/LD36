using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rail
{
    protected Vector3 position;
    protected RailDirection direction;
    private readonly GameObject cellObject;

    protected enum RailDirection
    {
        Left,
        Right,
        Top,
        Bottow
    }

    public Rail(Transform parent, GameObject prefab, Vector2 pos)
    {
        position = new Vector3(pos.x, 0, pos.y);
        cellObject = Object.Instantiate(prefab, position, Quaternion.identity) as GameObject;
        cellObject.transform.SetParent(parent);
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public List<Vector3> GetWaypoints()
    {
        var waypointTransforms = cellObject.GetComponents<Transform>();
        return waypointTransforms.Select(waypoint => waypoint.position).ToList();
    }
}

public class SwitchRail : Rail
{
    public SwitchRail(Transform parent, GameObject prefab, Vector2 pos) : base(parent, prefab, pos)
    {

    }

    public void RotateSwitch()
    {
        direction += 1 % 4;
    }
}