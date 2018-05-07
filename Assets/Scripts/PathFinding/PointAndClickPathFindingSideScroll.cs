using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickPathFindingSideScroll : MonoBehaviour {

	private Animator myAnimator;
	private bool myWalkingRight;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log(Physics.Raycast(ray, out hit, 100));
		}
	}
}
