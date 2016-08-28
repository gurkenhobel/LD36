using System.Linq;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    private static float _speed;

    public RailGridController Grid;

    public Waypoint StartWaypoint;

    public Train Train;

    Waypoint _currentWaypoint;
    Waypoint _nextWaypoint;

    float _lastWaypointTime;


    public void Init(Waypoint spawnWaypoint, Waypoint dir)
    {
        _speed = 2;
        Grid = Train.Grid;
        transform.position = spawnWaypoint.transform.position;
        StartWaypoint = spawnWaypoint;
        _lastWaypointTime = Time.time;
        _currentWaypoint = StartWaypoint;
        _nextWaypoint = dir;
    }

    private void CalculatePosition()
    {
        transform.rotation = Quaternion.LookRotation(_nextWaypoint.transform.position - _currentWaypoint.transform.position, Vector3.up);
        Vector3 newPos = transform.position + transform.forward * Time.deltaTime * _speed;
        transform.position = newPos;

        if (Vector3.Distance(transform.position, _nextWaypoint.transform.position) < 0.01)
        {
            _lastWaypointTime = Time.time;
            var twp = _nextWaypoint;
            _nextWaypoint = GetNextWaypoint();
            _currentWaypoint = twp;
        }
    }

    private Waypoint GetNextWaypoint()
    {

        return _nextWaypoint.AdjecentWaypoints.Single(w => w != _currentWaypoint);
    }

    void Update()
    {
        CalculatePosition();
    }
}
