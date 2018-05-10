using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToNextScene : MonoBehaviour {
	public string nextScene;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			FadeManager.Instance.Fade(true, 1.5f, 1, nextScene);
		}
	}
}
