using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	public GameObject LoadingPanel;
	public GameObject LogoFree; 
	public GameObject LogoPaid; 
	public GameObject MenuPopup;
	public GameObject CreditsPopup;
	public GameObject NoAdsPopup;
	public GameObject MorePopup;
	private VersionController VersionController;

	void Awake(){
		VersionController = GameObject.Find("VersionController").GetComponent<VersionController>();

		if(VersionController.paidVersion == true){
			LogoFree.SetActive(false);
			LogoPaid.SetActive(true);
		} else {
			LogoFree.SetActive(true);
			LogoPaid.SetActive(false);
		}
	}

	void Start(){
		LoadingPanel.SetActive(false);
		CloseAllPopups();

		// var timesPlayed : int = PlayerPrefs.GetInt("timesPlayed");

		// if(timesPlayed > 1){
		// 	MenuPopup.SetActive(true);
		// } else {
		// 	MenuPopup.SetActive(false);
		// }
	}

	public void ShowPopup(GameObject thePopup){
		//--show one popup and close all the others
		CloseAllPopups();
		thePopup.SetActive(true);

	}
	
	public void StartGame() {
		//--show loading panel because there's a delay
		LoadingPanel.SetActive(true);

		Application.LoadLevel ("levelSelect");
	}

	public void FacebookBtnPressed() {
		Application.OpenURL("https://www.facebook.com/davidonionball");
	}

	public void RateBtnPressed() {
		if(VersionController.paidVersion == true){
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.BotSumoBattleArena");
		}else {
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.RoboSumo");
		}
	}

	public void CloseAllPopups(){
		MenuPopup.SetActive(false);
		NoAdsPopup.SetActive(false);
		CreditsPopup.SetActive(false);
		MorePopup.SetActive(false);
	}

	public void TwitterBtnPressed() {
		Application.OpenURL("https://www.twitter.com/davidonionball");
	}

	public void LikeBtnPressed() {
		Application.OpenURL("https://www.facebook.com/BotSumoGame/");
	}

	public void BattleArenaBtnPressed() {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.BotSumoBattleArena");
	}

	public void FokkerBtnPressed() {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.ChickenFokkers");
	}

	public void ETSBtnPressed() {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.EscapeTheSector");
	}

	public void NoAdsBtnPressed(){
		//--do this here because the button can't find the versioncontroller
		Debug.Log("no ads btn pressed");
		VersionController.PurchaseNoAds();
	}
}








