using UnityEngine;
using System.Collections;
using System.Linq;

public class Trailer : MonoBehaviour
{
    private static float _speed;

    public RailGridController Grid;

    public Waypoint StartWaypoint;

    Waypoint _currentWaypoint;
    Waypoint _nextWaypoint;

    float _lastWaypointTime;

	void Start ()
    {
        transform.position = StartWaypoint.transform.position;
        _lastWaypointTime = Time.time;
        _currentWaypoint = StartWaypoint;
        _nextWaypoint = _currentWaypoint.AdjecentWaypoint1??_currentWaypoint.AdjecentWaypoint2;
	}

    private void CalculatePosition()
    {
        transform.position = Vector3.Lerp(_currentWaypoint.transform.position, _nextWaypoint.transform.position, (Time.time - _lastWaypointTime) *  _speed);
        transform.rotation = Quaternion.LookRotation(_nextWaypoint.transform.position - _currentWaypoint.transform.position, Vector3.up);

        if(Vector3.Distance(transform.position, _nextWaypoint.transform.position) < 0.01)
        {
            var twp = _nextWaypoint;
            _nextWaypoint = GetNextWaypoint();
            _currentWaypoint = twp;
        }
    }

    private Waypoint GetNextWaypoint()
    {

        return _nextWaypoint.AdjecentWaypoint1 == _currentWaypoint ? _nextWaypoint.AdjecentWaypoint2 : _nextWaypoint.AdjecentWaypoint1;
    }
	
	void Update ()
    {
        CalculatePosition();
	}
}
