using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertControllerCS : MonoBehaviour {

	private VersionController VersionController;

	#if !UNITY_ADS // If the Ads service is not enabled...
		public string gameId; // Set this value from the inspector.
		public bool enableTestMode = false;
	#endif

	// Use this for initialization
	void Awake () {
		VersionController = GameObject.Find("VersionController").GetComponent<VersionController>();

		if(VersionController.paidVersion == false){

			#if !UNITY_ADS // If the Ads service is not enabled...
			    if (Advertisement.isSupported) { // If runtime platform is supported...
			        Advertisement.Initialize(gameId, enableTestMode); // ...initialize.
			    }
		    #endif
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//--show an ad if spacebar is pressed
		if(Input.GetKey("space") ) {
			ShowAdvert();
		}
	}

	public void ShowAdvert(){
		//--show a normal ad - at the end of a round
		if(VersionController.paidVersion == false){
			Debug.Log("show ad");
			if(Advertisement.isInitialized && Advertisement.IsReady()) {
				Advertisement.Show();
			}
		}
	}

	void ShowRewardedAd(){
		// var options : ShowOptions = new ShowOptions();
	 //        options.resultCallback = HandleShowResult;
	}

}





