using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System;


[Serializable]
public class GameState {
	private static GameState gameState = new GameState();
	private static Memento previousState = new Memento(new Player());
	private static string filename = "savedState.gd";
	public static int LevelSelector = 5;
	
	public void Start() {
		
	}
	
	public static GameState recalGameStats() {
		return gameState;
	}
	
	public Player Player {
		get {
			return previousState.Player;
		}
	}
	
	public static void save(Player player) {
		previousState = new Memento (player);
	}
	
	public class Memento {
		
		private Player player;
		
		public Memento (Player player)
		{
			this.player = player;
		}
		
		
		public Player Player {
			get {
				return this.player;
			}
		}
	}
	
	public static void saveState(SavedState savedState) {
		string path = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//path = Application.dataPath + "/../../Documents/" + filename;
			Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
			path = System.IO.Path.Combine (Application.persistentDataPath, filename);
		} else {
			path = Application.persistentDataPath + filename;
		}
		
		if (path != null) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.OpenOrCreate);
			bf.Serialize (file, savedState);
			file.Close ();
		}
	}
	
	public static SavedState loadState() {
		string path = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//path = Application.dataPath + "/../../Documents/" + filename;
			Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
			path = System.IO.Path.Combine (Application.persistentDataPath, filename);
		} else {
			path = Application.persistentDataPath + filename;
		}
		
		SavedState result = new SavedState ();
		if (path != null) {
			try {
				if (File.Exists (path)) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file = File.Open (path, FileMode.OpenOrCreate);
					SavedState savedState = (SavedState)bf.Deserialize (file);
					file.Close ();
					result = savedState;
				}
			} catch (Exception e) {
				File.Delete (path);
			}
		}
		return result;
	}
}
