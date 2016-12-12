using UnityEngine;
using System.Collections;

public class SelectionScript : MonoBehaviour {
	public Camera camera;
	public GameObject tower;
	public GameObject menu;
	GameObject newTower;
	int selectedTower = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			Debug.Log ("touched");
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				RaycastHit hit;
				if (Physics.SphereCast (camera.ScreenPointToRay (Input.mousePosition), .5f, out hit)) {
					if (!hit.transform.gameObject.GetComponent<ObsticleScript> ()) {
						Vector3 hitPos = hit.point;
						newTower = Instantiate (tower) as GameObject;
						newTower.transform.position = new Vector3 (hitPos.x, .4f, hitPos.z);
						AstarPath.active.Scan ();
					} else {
						Vector3 hitPos = camera.WorldToScreenPoint (hit.point);
						menu.transform.position = new Vector3 ((hitPos.x - 60f), hitPos.y, hitPos.z);
						menu.SetActive (true);
					}
				}
			} 
			if (Input.GetTouch (0).phase == TouchPhase.Moved){
				RaycastHit hit;
				if (Physics.SphereCast (camera.ScreenPointToRay (Input.mousePosition), .5f, out hit)) {
					if (!hit.transform.gameObject.GetComponent<ObsticleScript> ()) {
						Vector3 hitPos = hit.point;

						newTower.transform.position = new Vector3 (Mathf.Round (hitPos.x), .4f, Mathf.Round (hitPos.z));
						AstarPath.active.Scan ();
					}
				}
			}

			if ((Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch (0).phase == TouchPhase.Canceled)) {

				Debug.Log ("Touch Released from : ");
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			if (Physics.SphereCast (camera.ScreenPointToRay (Input.mousePosition), .5f, out hit)) {
				if (!hit.transform.gameObject.GetComponent<ObsticleScript> () && selectedTower >= 0) {
					Vector3 hitPos = hit.point;
					newTower = Instantiate (tower) as GameObject;
					newTower.transform.position = new Vector3 (hitPos.x, .4f, hitPos.z);
					AstarPath.active.Scan ();
				} else if (hit.transform.gameObject.GetComponent<ObsticleScript> ())  {
					selectedTower = -1;
					Vector3 hitPos = camera.WorldToScreenPoint (hit.point);
					menu.transform.position = new Vector3 ((hitPos.x - 60f), hitPos.y, hitPos.z);
					menu.SetActive (true);
				}
			}
				
		} else if (Input.GetMouseButton (0) && selectedTower >= 0) {
			RaycastHit hit;
			if (Physics.SphereCast (camera.ScreenPointToRay (Input.mousePosition), .5f, out hit)) {
				if (!hit.transform.gameObject.GetComponent<ObsticleScript> ()) {
					Vector3 hitPos = hit.point;

					newTower.transform.position = new Vector3 (Mathf.Round (hitPos.x), .4f, Mathf.Round (hitPos.z));
					AstarPath.active.Scan ();
				}
			}
		}

	}

	public void setSelectedTower(int index) {
		Debug.Log ("select tower set");
		selectedTower = index;
	}
}



