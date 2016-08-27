using System.Collections.Generic;
using UnityEngine;

public class RailGridController : MonoBehaviour
{
    public int width = 20;
    public int height = 10;

    public GameObject railPrefab;
    public GameObject switchPrefab;

    public TextAsset testLevel;

    private Rail[,] railGrid;


    private void Start()
    {
        railGrid = LevelImporter.GetRailGridFromFile(testLevel);
        CreateRailGrid();
    }

    public void CreateRailGrid()
    {
        railGrid = new Rail[height, width];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 pos = new Vector2(x, z);
                railGrid[z, x] = new Rail(railPrefab, pos);
            }
        }
    }

    private void PlaceRailObject(GameObject prefab, int x, int z)
    {
        Vector3 pos = new Vector3(x, 0, z);
        GameObject railObject = Instantiate(railPrefab, pos, Quaternion.identity) as GameObject;
        railObject.transform.SetParent(transform);
    }
}

public enum RailDirection
{
    Left,
    Right,
    Top,
    Bottow
}

public class Rail
{
    protected Vector2 position;
    protected RailDirection direction;
    protected GameObject cellObject;

    public Rail(GameObject prefab, Vector2 pos)
    {
        Vector3 cellPos = new Vector3(pos.x, pos.y);
        cellObject = Object.Instantiate(prefab, cellPos, Quaternion.identity) as GameObject;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public List<Vector3> GetWaypoints()
    {
        var waypoints = new List<Vector3>();
        // TODO get waypoints
        return waypoints;
    }
}

public class SwitchRail : Rail
{
    public SwitchRail(GameObject prefab, Vector2 pos) : base(prefab, pos)
    {

    }

    public void RotateSwitch()
    {
        direction += 1 % 4;
    }
}
