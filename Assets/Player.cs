using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// 1) declaration mouse sensitivity (default 5)
	public float SensitivityX = 5f;
	public float SensitivityY = 5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// 1) Head rotation with mouse
		if (Input.GetKey(KeyCode.LeftControl))
		{
			var curr = transform.eulerAngles;
			var mouseX = Input.GetAxis("Mouse X") * SensitivityX;
			var mouseY = Input.GetAxis("Mouse Y") * SensitivityY;
			transform.eulerAngles = new Vector3(curr.x - mouseY, curr.y + mouseX, curr.z);
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red, 1f);

			if (Input.GetMouseButtonDown(0))
			{
				//3) Teleport
				var teleport = hit.collider.gameObject.GetComponent<WaypointScript>();
				if (teleport != null)
				{
					Teleport(teleport.transform);
				}
			}
		}
	}

	private Transform lastTeleporter;
	
	private void Teleport(Transform teleporter)
	{
		transform.position = teleporter.transform.position;

		if(lastTeleporter != null)
			lastTeleporter.gameObject.SetActive(true);
		teleporter.gameObject.SetActive(false);
		lastTeleporter = teleporter;
	}
}
