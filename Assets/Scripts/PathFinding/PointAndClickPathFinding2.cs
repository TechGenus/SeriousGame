using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickPathFinding2 : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	private LayerMask layerMask;
	private Transform t;
	private bool destinationInitalzied;
	private Vector3 destination;
	private Vector3 direction;
	private Vector3 distance;

	public float speed;
	public float marginOfError;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
		layerMask = 1 << 9;
		layerMask = ~layerMask;
		destinationInitalzied = false;
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
			findDirectionAndDestination();

		if (destinationInitalzied)
		{
			float absDistanceX = Mathf.Abs(distance.x);
			float absDistanceY = Mathf.Abs(distance.y);

			float currAbsDistanceX = Mathf.Abs(t.position.x - destination.x);
			float currAbsDistanceY = Mathf.Abs(t.position.y - destination.y);


			if (currAbsDistanceX > marginOfError || currAbsDistanceY > marginOfError)
			{
				float totalAbsDistance = (absDistanceX + absDistanceY);
				float distanceXPercentage = absDistanceX / totalAbsDistance;
				float distanceYPercentage = absDistanceY / totalAbsDistance;
				float translateX = speed * distanceXPercentage * direction.x * Time.deltaTime;
				float translateY = speed * distanceYPercentage * direction.y * Time.deltaTime;
				t.Translate(translateX, translateY, 0);
			}
			else destinationInitalzied = false;
		}
	}

	private void findDirectionAndDestination()
	{
		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
			destination = hit.point;
			destinationInitalzied = true;
			distance = hit.point - t.position;
			float directionX = distance.x / Mathf.Abs(distance.x);
			float directionY = distance.y / Mathf.Abs(distance.y);
			direction = new Vector3(directionX,	directionY);
		}
	}
}
