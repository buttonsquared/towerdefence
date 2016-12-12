using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;
using System.IO;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour {
	public int level = 1;
	public float leveltimer = 100.0f;
	public  Player player;
	public Camera mainCamera;


    private StateEnum state;
	private int currentLevel = 1;
	private List<UnityIObserver> observers = new List<UnityIObserver>();
	private static int totalLevels = GameState.LevelSelector;
	private float maxleveltimer = 100.0f;
	private SavedState savedState = new SavedState();
	private bool paused;
	private GameObject beganObj;
	private GameObject currentObj;
	private Animator anim;



    IEnumerator FSM() {
		while (true) {
			notifyObservers(new GameMessage(MessageTypeEnum.State, (object) state));
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

	public void Start() {
        /* string levelDefinition = Extensions.LoadResourceTextfile("GameJSONData/Level" + level);
        JSONNode levelJson = JSON.Parse(levelDefinition);
        LevelInfo levelInfo = new LevelInfo(levelJson);
        cracks = levelInfo.cracks;
        for (int i = 0; i < levelInfo.gameObjectPostions.Count; i++)
        {
            LevelInfo.GameObjectPosition gop = levelInfo.gameObjectPostions[i];
            float x = gop.xPos;
            float y = gop.yPos;
            float xRot = gop.xRotation;
            float yRot = gop.yRotation;
            print("x=" + x + " and y =" + y + " and y rot = " + yRot);
            Vector3 pos = new Vector3(x, y);
            if (gop.name.Equals("ladyBug")) {
                GameObject newLadyBug = Instantiate(ladyBugs, pos, Quaternion.Euler(xRot, yRot, 0)) as GameObject;
            } else if (gop.name.Equals("branch2Branch"))
            {
                GameObject newBranch2Branch = Instantiate(branch2Branch, pos, Quaternion.Euler(xRot, yRot, 0)) as GameObject;
            }
            else if (gop.name.Equals("branchSingle"))
            {
                GameObject newBranchSingle = Instantiate(branchSingle, pos, Quaternion.Euler(xRot, yRot, 0)) as GameObject;
            }
            else if (gop.name.Equals("nest"))
            {
                GameObject newNest = Instantiate(nest, pos, Quaternion.Euler(xRot, yRot, 0)) as GameObject;
            }
            else if (gop.name.Equals("player"))
            {
                GameObject egg = GameObject.FindGameObjectWithTag("Player");
                egg.transform.position = pos;
            }
        }
        */
        currentLevel = level;
		mainCamera.aspect = 3F / 2F;
		maxleveltimer = leveltimer;
		GameState gameState = GameState.recalGameStats ();
		player = gameState.Player;
		savedState = GameState.loadState ();
		state = StateEnum.SplashScreen;
		StartCoroutine(FSM ());
		anim = GameObject.Find ("Menus").GetComponent<Canvas> ().GetComponent<Animator> ();
	}

	public void Awake() {
        Time.timeScale = 0;
        if (player == null) {
			player = new Player ();
		}
	}
	
	public virtual IEnumerator StartNextLevel() {
		Time.timeScale = 1;
		yield return null;
	}

    public void cleanMenus()
    {
        anim.SetTrigger("ClearMenu");
        anim.SetTrigger("GameRunning");
    }
	
	public IEnumerator RestartLevel() {
        Debug.Log("RestartLevel state = " + state);
        Time.timeScale = 1;
		state = StateEnum.StartNewGame;

        if (StateEnum.RestartLevel.Equals(state))
        {
            yield return null;
        } else
        {
            yield return true;
        }
	}
	
	public IEnumerator RestartGame() {
        Debug.Log("restartGame state = " + state);
        Time.timeScale = 0;
        Application.LoadLevel("level1");
        yield return null;
	}

    public IEnumerator SplashScreen()
    {
        Time.timeScale = 0;
        yield return null;
    }

    public IEnumerator StartNewGame() {
        if (!paused) {
			Time.timeScale = 1;
		}
        if (StateEnum.RestartLevel.Equals(state))
        {
            yield return null;
        }
        else
        {
            yield return true;
        }
	}
	
	public virtual IEnumerator GameOver() {
        Debug.Log("GameOver state = " + state);
        anim.SetTrigger ("GameOver");
		Time.timeScale = 0;
		yield return true;
	}
	
	public virtual IEnumerator LevelComplete() {
        //if (savedState.level < level) {
        //	savedState.level = level;
        //	GameState.saveState (savedState);
        //}
        Debug.Log("LevelComplete state = " + state);
        anim.SetTrigger ("LevelComplete");
		Time.timeScale = 0;
		yield return true;
	}
	
	public IEnumerator Win() {
		
		yield return true;
	}
	
	
	public IEnumerator NextPlayer() {
		yield return null;
	}
	
	public void setState(StateEnum st) {
		this.state = st;
	}

    public void setState(String st)
    {
        this.state = (StateEnum)Enum.Parse(typeof(StateEnum), st, true);
    }

    public GameObject BeganObj {
		get {
			return this.beganObj;
		}
		set {
			beganObj = value;
		}
	}
	
	public GameObject CurrentObj {
		get {
			return this.currentObj;
		}
		set {
			currentObj = value;
		}
	}

    public bool checkTouch(string name) {
		bool result = false;
		if ((BeganObj != null && BeganObj.name != null && currentObj == null && BeganObj.name.Equals (name))) {
			result = true;
		} else if (currentObj != null && currentObj.name != null && currentObj.name.Equals (name)) {
			result = true;
		}
		return result;
	}
	
	public int getMaxLevel () {
		int result = savedState.level;
		if (result == null || result == 0) {
			result = 1;
		}
		return result;
	}
	
	public void registerObserver(UnityIObserver iobserver) {
		observers.Add (iobserver);
	}
	
	public void unregisterObserver(UnityIObserver iobserver) {
		observers.Remove(iobserver);
	}
	
	public void notifyObservers(GameMessage message) {
		foreach (UnityIObserver iobserver in observers) {
			iobserver.notify(message);
		}
	}
	
	
	public void Update () 
	{
		leveltimer -= Time.deltaTime;
		float percentageTimeLeft = leveltimer / maxleveltimer;


        if (Input.touchCount > 0)
		{
			for(int i = 0; i < Input.touchCount; i++)
			{
				Touch currentTouch = Input.GetTouch(i);
				if(currentTouch.phase == TouchPhase.Began)
				{
					Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
                    print("touch begin" + v2);
					Collider2D c2d = Physics2D.OverlapPoint(v2);
					
					if(c2d != null)
					{
						beganObj = c2d.gameObject;
					}
				}
				
				if (currentTouch.phase == TouchPhase.Ended) {
                    print("touch ended");
					beganObj = null;
					currentObj = null;
				} else {
					Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
					Collider2D c2d = Physics2D.OverlapPoint(v2);                    
					
					if (c2d != null)
					{
						currentObj = c2d.gameObject;
					}
				}
				
				if (currentTouch.phase == TouchPhase.Moved)
				{
					Vector2 v2 = new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x, Camera.main.ScreenToWorldPoint(currentTouch.position).y);
					Collider2D c2d = Physics2D.OverlapPoint(v2);                    
					
					if (c2d != null)
					{
						currentObj = c2d.gameObject;
					}
				}
			}
		}
	}


}
