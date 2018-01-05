﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchInput : MonoBehaviour {
	private float hold_time = 1.0f;
	private float count_time = 0.0f;
	//Vector3 plane_point;
	float mag_value = 1.0f;


	void Start() {}

	// Update is called once per frame
	void Update () {
		// Attach this script to a trackable object
		// Create a plane that matches the target plane
		Plane target_plane = new Plane (transform.up, transform.position);
		// When user touch the screen
		foreach (Touch touch in Input.touches) {
			count_time += touch.deltaTime;
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			float dist = 0.0f;
			target_plane.Raycast (ray, out dist);
			RaycastHit hit;
			//plane_point = ray.GetPoint (dist);
			if (touch.phase == TouchPhase.Ended && count_time < hold_time) {
				if (Physics.Raycast (ray, out hit, 2)) {
					if (hit.collider != null && hit.transform.GetComponent<Tile> ().state == "idle") {
						hit.transform.GetComponent<Tile> ().UncoverTile ();
						count_time = 0;
					}
				}
			}else if(touch.phase == TouchPhase.Stationary || (touch.phase == TouchPhase.Moved && touch.deltaPosition.magnitude < mag_value)){
				if (Physics.Raycast (ray, out hit, 2)) {
					if (hit.collider != null && count_time >= hold_time) {
						hit.transform.GetComponent<Tile> ().SetFlag ();
						count_time = 0;
					}
				}
			}
		}
	}
}



			    // Just to write out the coords of the touch input on the target plane
				/*float vX = planePoint.x;
				float vZ = planePoint.z;
				posText.text = "vX: " + vX.ToString () + "\n" + "vZ: " + vZ.ToString () + "\n" + "Dist: " + dist.ToString ();*/