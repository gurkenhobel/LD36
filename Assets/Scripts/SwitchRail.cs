using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchRail : Rail
{

    public List<Waypoint> SwitchPositions;



    public SwitchRail(Transform parent, GameObject prefab, Vector2 pos) : base(parent, prefab, pos)
    {

    }

    public void RotateSwitch()
    {
        direction += 1 % 4;
    }
}