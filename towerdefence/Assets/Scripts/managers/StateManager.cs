using UnityEngine;
using System.Collections;
using System;

public class StateManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}
	StateEnum state = StateEnum.SplashScreen;
	private NetworkService _network;

	void Start() {
		Messenger<StateEnum>.AddListener (GameEvent.STATUS, setStatus);
		StartCoroutine(FSM ());
	}


	public void Startup(NetworkService service) {
		Debug.Log("State manager starting...");

		_network = service;


		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	IEnumerator FSM() {
		while (true) {
			Messenger<StateEnum>.Broadcast(GameEvent.CONTROLLERSTATUS, state, MessengerMode.DONT_REQUIRE_LISTENER);
			if (StateEnum.SplashScreen == state)
			{
				yield return StartCoroutine(SplashScreen());
			} else if (StateEnum.StartNewGame == state) {
				yield return StartCoroutine (StartNewGame ());
			} else if (StateEnum.RestartLevel == state) {
				yield return StartCoroutine (RestartLevel ());
			} else if (StateEnum.StartNextLevel == state) {
				yield return StartCoroutine (StartNextLevel ());
			} else if (StateEnum.RestartGame == state) {
				yield return StartCoroutine (RestartGame ());
			} else if (StateEnum.GameOver == state) {
				yield return StartCoroutine (GameOver ());
			} else if (StateEnum.LevelComplete == state) {
				yield return StartCoroutine (LevelComplete ());
			} else if (StateEnum.Win == state) {
				yield return StartCoroutine (Win ());
			}
		}
	}

	public IEnumerator SplashScreen()
	{
		yield return null;
	}

	public IEnumerator StartNewGame() {
		yield return null;
	}

	public IEnumerator RestartLevel() {
		yield return null;
	}

	public IEnumerator StartNextLevel() {
		yield return null;
	}

	public IEnumerator RestartGame() {
		yield return null;
	}

	public IEnumerator GameOver() {
		yield return null;
	}

	public IEnumerator LevelComplete() {
		yield return null;
	}

	public IEnumerator Win() {
		yield return null;
	}

	public void setStatus(StateEnum state) {
		this.state = state;
	}

	public void setState(StateEnum st) {
		this.state = st;
	}

	public void setState(string st)
	{
		this.state = (StateEnum)Enum.Parse(typeof(StateEnum), st, true);
	}
}
