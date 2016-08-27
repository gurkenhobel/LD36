using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Camera _camera;
	public float _speed = 15.0f;

	float _fieldOfView;

	float _minFieldOfView = 15f;
	float _maxFieldOfView = 90f;

	float _sensitivity = 10f;




	private bool _bDragging = false;
	Vector3 _oldPos;
	Vector3 _panOrigin;

	private float _panSpeed = 20f;

	private Vector3 ResetCamera;
	// Use this for initialization
	void Start ()
	{
		_camera = GetComponent<Camera> ();
		ResetCamera = _camera.transform.position;
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

		_fieldOfView = Camera.main.fieldOfView;

		_fieldOfView -= Input.GetAxis ("Mouse ScrollWheel") * _sensitivity;
		_fieldOfView = Mathf.Clamp (_fieldOfView, _minFieldOfView, _maxFieldOfView);
		Camera.main.fieldOfView = _fieldOfView;


		if (Input.GetMouseButtonDown (0)) 
		{
			_bDragging = true;
			_oldPos = transform.position;

			//get the ScreenVector (Vector3D, Mouse clicked)
			_panOrigin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		}

		if (Input.GetMouseButton (0)) 
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition) - _panOrigin;

			transform.position = _oldPos + -pos * _panSpeed;
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			_bDragging = false;
		}





	}

	void FixedUpdate()
	{

	}

	void LateUpdate()
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			Camera.main.transform.position = ResetCamera;
		}
	}

}
