using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	public Text levelText;
	
	public int curLevel {get; private set;}
	public int maxLevel {get; private set;}
	
	private NetworkService _network;
	
	public void Startup(NetworkService service) {
		Debug.Log("Mission manager starting...");
		
		_network = service;
		
		UpdateData(1, 3);
		
		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void UpdateData(int curLevel, int maxLevel) {
		this.curLevel = curLevel;
		this.maxLevel = maxLevel;
	}

	public void ReachObjective() {
		// could have logic to handle multiple objectives
		Messenger<StateEnum>.Broadcast(GameEvent.STATUS, StateEnum.LevelComplete);
	}

	public void GoToNext() {
		if (curLevel < maxLevel) {
			curLevel++;
			
		} else {
			Debug.Log("Last level");
			Messenger<StateEnum>.Broadcast(GameEvent.STATUS, StateEnum.GameOver);
		}
	}

	public void RestartCurrent() {
		string name = "Level" + curLevel;
		Debug.Log("Loading " + name);
		Application.LoadLevel(name);
	}

	void Update() {
		levelText.text = "Level: " + curLevel;
	}
}
