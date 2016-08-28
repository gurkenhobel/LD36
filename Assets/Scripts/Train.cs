using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Train : MonoBehaviour
{
    #region prefabs
    public GameObject TrailerPrefab;
    #endregion
    public RailGridController Grid;

    public Waypoint StartWaypoint;

    public Trailer Lock;

    public int Length;

    private List<Trailer> Trailer;

    private const float TRAILER_LENGTH = 0.8f;

	// Use this for initialization
	void Start ()
    {
        StartWaypoint = Grid.SpawnPoint;
        Lock.Init(StartWaypoint, StartWaypoint.AdjecentWaypoints[0]);
        Trailer = new List<Trailer>();
        //GenerateTrailers(Length);

	}
	
    public void AddTrailer(Waypoint spawnpoint, Waypoint dir)
    {
        var trailer1 = Instantiate<GameObject>(TrailerPrefab).GetComponent<Trailer>();
        trailer1.Train = this;
        trailer1.transform.position = spawnpoint.transform.position;
        trailer1.Init(spawnpoint, dir);
        Trailer.Add(trailer1);
    }


    //public void GenerateTrailers(int count)
    //{
    //    var trailer1 = Instantiate<GameObject>(TrailerPrefab).GetComponent<Trailer>();
    //    trailer1.Train = this;
    //    trailer1.transform.position = Lock.transform.position + new Vector3(-TRAILER_LENGTH, 0, 0);
    //    trailer1.Init(StartWaypoint.AdjecentWaypoints[0]);
    //    Trailer.Add(trailer1);

    //    for(int i = 1; i < count; i++)
    //    {
    //        var trailer = Instantiate<GameObject>(TrailerPrefab);
    //        var trailerBhv = trailer.GetComponent<Trailer>();
    //        trailerBhv.Train = this;
    //        trailerBhv.Init (StartWaypoint.AdjecentWaypoints[0]);
    //        trailer.transform.position = Trailer[i - 1].transform.position + new Vector3 (0, 0, -TRAILER_LENGTH);
    //        Trailer.Add(trailerBhv);
    //    }
    //}

	// Update is called once per frame
	void Update ()
    {
	
	}
}
