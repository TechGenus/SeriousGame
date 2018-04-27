using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickPathFindingMI : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	private LayerMask layerMask;
	private Transform t;
	private bool destinationInitalzied;
	private bool interactable;
	private string interactableObjName;
	private Vector3 destination;
	private Vector3 direction;
	private Vector3 distance;

	public float speed;
	public float marginOfError;

	public GameObject playerBody;
    private Animator pbAnim;

	public GameObject waypoint;
	private Transform waypointT;

    // Use this for initialization
    void Start () {
		t = GetComponent<Transform>();
		layerMask = 1 << 9;
		layerMask = ~layerMask;
		destinationInitalzied = false;
        pbAnim = playerBody.GetComponent<Animator>();

		waypointT = waypoint.GetComponent<Transform>();
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
                // going to pos
                float totalAbsDistance = (absDistanceX + absDistanceY);
                float distanceXPercentage = absDistanceX / totalAbsDistance;
                float distanceYPercentage = absDistanceY / totalAbsDistance;
                float translateX = speed * distanceXPercentage * direction.x * Time.deltaTime;
                float translateY = speed * distanceYPercentage * direction.y * Time.deltaTime;

                setAnimation(distanceYPercentage, distanceXPercentage);

                t.Translate(translateX, translateY, 0);
            }
            else
            {
                // reached pos
                destinationInitalzied = false;
                pbAnim.SetBool("walking", false);
				waypoint.SetActive(false);

				if (interactable) {
					//put switch later probably
					if (interactableObjName.Equals ("Door to Scene1")) {
						FadeManager.Instance.Fade (true, 1.5f, 1);
					}
				}
            }
		}
	}

    private void setAnimation(float distanceYPercentage, float distanceXPercentage)
    {
        if (distanceYPercentage >= distanceXPercentage)
        {
            if (direction.y <= 0)
            {
                pbAnim.SetInteger("playerState", 2);
                if (direction.x <= 0)
                {
                    pbAnim.SetBool("wasIdleLeft", true);
                    pbAnim.SetBool("wasIdleRight", false);
                }
                    
                else
                {
                    pbAnim.SetBool("wasIdleLeft", false);
                    pbAnim.SetBool("wasIdleRight", true);
                }
            }
            else if (direction.y > 0)
            {
                pbAnim.SetInteger("playerState", 8);
                if (direction.x <= 0)
                {
                    pbAnim.SetBool("wasIdleLeft", true);
                    pbAnim.SetBool("wasIdleRight", false);
                }

                else
                {
                    pbAnim.SetBool("wasIdleLeft", false);
                    pbAnim.SetBool("wasIdleRight", true);
                }
            }
        }
        else
        {
            if (direction.x <= 0)
            {
                pbAnim.SetInteger("playerState", 4);
            }
            else if (direction.x > 0)
            {
                pbAnim.SetInteger("playerState", 6);
            }
        }
        pbAnim.SetBool("walking", true);
    }

	private void findDirectionAndDestination()
	{
		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
			if (hit.collider.tag == "Interactable") {
				interactable = true;
				interactableObjName = hit.collider.name;

				destination = hit.collider.transform.GetChild (0).GetComponent<Transform> ().position;
				distance = destination - t.position;
			} else {
				destination = hit.point;
				distance = hit.point - t.position;
			}

			destinationInitalzied = true;
			float directionX = distance.x / Mathf.Abs(distance.x);
			float directionY = distance.y / Mathf.Abs(distance.y);
			direction = new Vector3(directionX,	directionY);

			waypoint.SetActive(true);
			waypointT.position = destination + new Vector3(0, 0.1f, -1f);
		}
	}
}
