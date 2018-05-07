using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    private Transform t;
	public float rotationSpeed = 90f;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		float rotationSpeed2 = rotationSpeed * Time.deltaTime;
		t.Rotate(0, rotationSpeed2, 0, Space.World);
	}
}
