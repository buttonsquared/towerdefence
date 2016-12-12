using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	public GameObject background;
	public GameObject splashScreen;

	private NetworkService _network;

//	private BannerView bannerView = null;
//	private AdRequest request;

	private bool isBannerShown = false;
	void Awake () {
		
	}

	void Start () {
		Messenger<StateEnum>.AddListener(GameEvent.CONTROLLERSTATUS, processMessage);
	}

	public void Startup(NetworkService service) {
		Debug.Log("UI manager starting...");

		_network = service;


		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	void processMessage(StateEnum status)
	{
		if (StateEnum.GameOver.Equals (status)) {
			Time.timeScale = 0;
			showBanner ();
			showHide (true, false);
		} else if (StateEnum.LevelComplete.Equals (status)) {
			Time.timeScale = 0;
			showHide (true, false);
			showBanner ();
		} else if (StateEnum.SplashScreen.Equals (status)) {
			Time.timeScale = 0;
			showHide (true, true);
		} else if (StateEnum.LevelSelector.Equals (status)) {
			showBanner ();
			Time.timeScale = 0;
			showHide (true, false);
		} else if (StateEnum.Pause.Equals (status)) {
			Time.timeScale = 0;
			showBanner ();
			showHide (true, false);
		} else if (StateEnum.Win.Equals (status)) {
			Time.timeScale = 0;
			showBanner ();
			showHide (true, true);
		} else {
			Time.timeScale = 1;
			removeBanner ();
			showHide (false, false);
		}

	}

	private void showHide(bool backgroundOn, bool spashScreenOn) {
		background.SetActive (backgroundOn);
		splashScreen.SetActive (spashScreenOn);
	}

	public void removeBanner() {

//		if (bannerView != null && isBannerShown) {
//			print ("removeBanner" + isBannerShown);
//			bannerView.Hide ();
//			bannerView.Destroy ();
//			bannerView = null;
//		}
//		isBannerShown = false;
	}

	//Ad unit name: Ad banner
	//Ad unit ID: ca-app-pub-8279250016566441/6649265815
	public void showBanner() {

//		if (!isBannerShown) {
//			print ("showBanner" + isBannerShown);
//			if (bannerView == null) {
//				bannerView = new BannerView ("ca-app-pub-6211305006005431/3977547704", AdSize.Banner, AdPosition.Top);
//				request = new AdRequest.Builder ().Build ();
//				bannerView.LoadAd (request);
//			}
//			bannerView.Show ();
//			isBannerShown = true;
//		}
	}
}
