using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickPathFinding : MonoBehaviour {

	public	GameObject		clickPoint;
	private Transform		clickPointT;

	private Ray				ray;
	private RaycastHit		hit;
	private LayerMask		layerMask;
	private RaycastHit2D	hit2D;
	private Transform		t;
	private Rigidbody2D		rb;
	private Vector2			destination;
	private bool			destinationInitalzied;
	private float			directionX;

	public float			speed;
	public float			marginOfError;

	// Use this for initialization
	void Start () {
		clickPointT =	clickPoint.GetComponent<Transform>();
		t =				GetComponent<Transform>();
		rb =			GetComponent<Rigidbody2D>();
		layerMask =		1 << 9;
		layerMask =		~layerMask;
		destinationInitalzied = false;
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
			findDirectionAndDestination();
		
		if (destinationInitalzied)
		{
			float distanceX = Mathf.Abs(t.position.x - destination.x);
			float distanceY = Mathf.Abs(t.position.y - destination.y);

			if (distanceX > marginOfError)
			{
				if (distanceX > distanceY)
					transform.Translate(speed * directionX * Time.deltaTime, 0, 0);
			}
			else destinationInitalzied = false;
		}
	}

	private void findDirectionAndDestination()
	{
		if (Physics.Raycast(ray, out hit, 100))
		{
			clickPointT.position = hit.point;
			hit2D = Physics2D.Raycast(hit.point, Vector2.down, 100, layerMask);
			if (hit2D)
			{
				directionX = hit2D.point.x / Mathf.Abs(hit2D.point.x);
				destination = new Vector2(hit2D.point.x, t.position.y);
				destinationInitalzied = true;
			}
		}
	}
}
