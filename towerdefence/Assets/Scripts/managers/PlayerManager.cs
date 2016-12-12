using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	public Text goldText;
	public Text healthText;
	public Text scoreText;

	public int health {get; private set;}
	public int maxHealth {get; private set;}
	public int gold { get; private set; }
	public int score { get; private set; }

	private NetworkService _network;

	public void Startup(NetworkService service) {
		Debug.Log("Player manager starting...");

		_network = service;

		UpdateData(20, 20, 80, 0);

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth, int gold, int score) {
		Debug.Log ("updateDate&************************************************");
		this.health = health;
		this.maxHealth = maxHealth;
		this.gold = gold;
		this.score = score;

	}

	public void ChangeHealth(int value) {
		health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		if (health == 0) {
			Messenger<StateEnum>.Broadcast(GameEvent.STATUS, StateEnum.LevelFailed);
		}
	}

	void Update () {
		goldText.text = "Gold: " + gold;
		healthText.text = "Health: " + health;
		scoreText.text = "Score: " + score;
	}

	public void Respawn() {
		UpdateData(20, 20, 80, 0);
	}
}
