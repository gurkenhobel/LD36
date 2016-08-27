using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Train : MonoBehaviour
{
    #region prefabs
    public GameObject TrailerPrefab;
    #endregion
    public Trailer Lock;

    public int Length;

    private List<Trailer> Trailer;

    private const float TRAILER_LENGTH = 8.5f;

	// Use this for initialization
	void Start ()
    {
        Trailer = new List<Trailer>();
        GenerateTrailers(Length);
	}
	
    public void GenerateTrailers(int count)
    {
        var trailer1 = Instantiate<GameObject>(TrailerPrefab).GetComponent<Trailer>();
        trailer1.transform.position = Lock.transform.position + new Vector3(-TRAILER_LENGTH, 0, 0);
        Trailer.Add(trailer1);

        for(int i = 1; i < count; i++)
        {
            var trailer = Instantiate<GameObject>(TrailerPrefab);
            trailer.transform.position = Trailer[i - 1].transform.position + new Vector3 (- TRAILER_LENGTH , 0, 0);
            Trailer.Add(trailer.GetComponent<Trailer>());
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
