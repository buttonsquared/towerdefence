using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Startup : MonoBehaviour {
	[SerializeField] public Slider progressBar;
	public Camera mainCamera;

	void Start() {
		mainCamera.aspect = 3F / 2F;
	}

	void Awake() {
		Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
	}
	void OnDestroy() {
		Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
	}

	private void OnManagersProgress(int numReady, int numModules) {
		float progress = (float)numReady / numModules;
		//progressBar.value = progress;
	}

	private void OnManagersStarted() {
		//progressBar.transform.gameObject.SetActive (false);
		Debug.Log("setting status");
	}
}



