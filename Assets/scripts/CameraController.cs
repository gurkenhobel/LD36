using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Camera _camera;
	public float _speed = 15.0f;
	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
		{
			transform.Translate (new Vector3 (_speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
		{
			transform.Translate (new Vector3 (-_speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (new Vector3 (0, -_speed * Time.deltaTime, 0));
		}
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (new Vector3 (0, _speed * Time.deltaTime, 0));
		}
	
	}

	void FixedUpdate()
	{

	}

	void LateUpdate()
	{
		
	}

}
