using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class SavedState {
	private int _level = 0;
	private float _volume = 1f;
	private float _musicVolume = 1f;
	private Dictionary<int, int> _stars = new Dictionary<int, int>();
	private bool _gameStarted;

	public int level	{
		set { this._level = value; }
		get { return this._level; }
	}
	public bool gameStarted {
		set { this._gameStarted = value; }
		get { return this._gameStarted; }
	}
	
	public void setStars(int level, int st) {
		
		if (_stars == null) {
			_stars = new Dictionary<int, int>();
		}
		if (_stars.ContainsKey (level)) {
			_stars.Remove(level);
		}
		_stars.Add(level, st);
	}
	
	public int getStars(int level) {
		int result = 0;
		if (_stars != null && _stars.ContainsKey (level)) {
			result = _stars[level];
		}
		return result;
	}
	
	public float volume	{
		set { this._volume = value; }
		get { return this._volume; }
	}
	
	public float musicVolume	{
		set { this._musicVolume = value; }
		get { return this._musicVolume; }
	}
}

