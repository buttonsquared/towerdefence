﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(UIManager))]
public class Managers : MonoBehaviour {
	public static PlayerManager Player {get; private set;}
	public static StateManager State {get; private set;}
	public static MissionManager Mission {get; private set;}
	public static DataManager Data {get; private set;}
	public static UIManager Ui {get; private set;}

	private List<IGameManager> _startSequence;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);

		Player = GetComponent<PlayerManager>();
		State = GetComponent<StateManager>();
		Ui = GetComponent<UIManager>();
		Mission = GetComponent<MissionManager>();
		Data = GetComponent<DataManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(State);
		_startSequence.Add(Ui);
		_startSequence.Add(Mission);
		_startSequence.Add(Data);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		NetworkService network = new NetworkService();
		
		foreach (IGameManager manager in _startSequence) {
			manager.Startup(network);
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;
		
		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			
			foreach (IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			
			if (numReady > lastReady) {
				Debug.Log("Progress: " + numReady + "/" + numModules);
				Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
			}
			
			yield return null;
		}

		Debug.Log("All managers started up");
		Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
	}
}
