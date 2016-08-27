using UnityEngine;
using System.Collections;
using System.Linq;

public class Trailer : MonoBehaviour
{
    private static float _speed;

    public RailGridController Grid;



    Vector3 _currentWaypoint;
    Vector3 _nextWaypoint;

    float _lastWaypointTime;

	void Start ()
    {
        _lastWaypointTime = Time.time;
        _currentWaypoint = transform.position;
        _nextWaypoint = GetNextWaypoint();
	}

    private void CalculatePosition()
    {
        transform.position = Vector3.Lerp(_currentWaypoint, _nextWaypoint, (Time.time - _lastWaypointTime) *  _speed);
        transform.rotation = Quaternion.LookRotation(_nextWaypoint - _currentWaypoint, Vector3.up);

        if(Vector3.Distance(transform.position, _nextWaypoint) < 0.01)
        {
            _currentWaypoint = _nextWaypoint;
            _nextWaypoint = GetNextWaypoint();
        }
    }

    private Vector3 GetNextWaypoint()
    {
        var wp = Grid.railGrid[Mathf.RoundToInt(transform.position.z), Mathf.RoundToInt(transform.position.x)].GetWaypoints();

        return new Vector3();
    }
	
	void Update ()
    {
        CalculatePosition();
	}
}
