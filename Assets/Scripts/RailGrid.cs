using UnityEngine;

public class RailGrid : MonoBehaviour
{
    public int width = 20;
    public int height = 10;
    public int cellSize = 10;

    public GameObject railPrefab;
    public GameObject switchPrefab;

    private Rail[,] railGrid;


    private void Start()
    {
        CreateRailGrid();
    }

    public void CreateRailGrid()
    {
        railGrid = new Rail[height, width];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                railGrid[z, x] = new Rail();
                PlaceRailObject(railPrefab, x * cellSize, z * cellSize);
            }
        }
    }

    public void PlaceRailObject(GameObject prefab, int x, int z)
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
    public RailDirection direction;
}

public class SwitchRail : Rail
{
    public void RotateSwitch()
    {
        direction += 1 % 4;
    }
}