using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour {

	public LevelsControllerCS LevelsController;
	public GameObject unlockModal; 
	public string levelSelectedToUnlock; 
	public GameObject[] levelButtons;
	private VersionController VersionController;

	// Use this for initialization
	void Start () {
		LevelsController = GameObject.Find("LevelsController").GetComponent<LevelsControllerCS>();
		// VersionController = GameObject.Find("VersionController").GetComponent<VersionController>(); //--hide for now 16:00
		HideUnlockModal();

		CheckIfLevelsUnlocked();
	}

	void CheckIfLevelsUnlocked(){
		foreach(GameObject obj in levelButtons) {
			// Debug.Log("check "+obj.name);
			obj.SendMessage("CheckIfLocked");
		}
	}

	public void LoadMainLevel(){
		LevelsController.SelectLevel("main");
	}

	public void ShowUnlockModal(string levelName){
		Debug.Log("loading modal for "+levelName);
		levelSelectedToUnlock = levelName;
		unlockModal.SetActive(true);
	}

	public void HideUnlockModal(){
		unlockModal.SetActive(false);
	}

	public void DownloadPaidVersionBtn (){
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.BotSumoBattleArena");
	}

	public void UnlockLevelBtn (){
		//--unlock button action
		Debug.Log("user chose to unlock level "+levelSelectedToUnlock);

		SendMessage("StartRewardedAd");
		HideUnlockModal();
	}

	public void UnlockCurrentLevel(){
		Debug.Log("unlock level "+levelSelectedToUnlock);

		PlayerPrefs.SetInt(levelSelectedToUnlock+"Unlocked",1);

		CheckIfLevelsUnlocked();
	}
}








