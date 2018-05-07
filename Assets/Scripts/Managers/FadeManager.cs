using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

	public static FadeManager Instance { set; get; }

	public Image fadeImage;
	public bool isInTransition;
	private float transition;
	private int build;
	public bool isShowing;
	private float duration;

	private void Awake () {
		Instance = this;
	}

	public void Fade (bool showing, float duration, int build) {
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		this.build = build;
		transition = (isShowing) ? 0 : 1;
	}

	private void Update() {
		// print (isInTransition);
		if (!isInTransition) {
			return;
		}

		transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		fadeImage.color = Color.Lerp (new Color (0, 0, 0, 0), new Color (0, 0, 0, 1), transition);

		if (transition > 1 || transition < 0) {
			// fade is done now load new scene
			isInTransition = false;
			ChangeScene ();
		}
	}

	void ChangeScene () {
		SceneManager.LoadScene ("Scene1");
	}
}