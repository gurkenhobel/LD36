using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Train : MonoBehaviour
{
    #region prefabs
    public Trailer TrailerPrefab;
    #endregion
    public Trailer Lock;


    private List<Trailer> Trailer;

    private const float TRAILER_LENGTH = 3;

	// Use this for initialization
	void Start ()
    {
        Trailer = new List<Trailer>();
	}
	
    public void GenerateTrailers(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var trailer = Instantiate<Trailer>(TrailerPrefab);
            Trailer.Add(trailer);
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
