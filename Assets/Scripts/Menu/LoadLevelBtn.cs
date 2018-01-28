using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelBtn : MonoBehaviour {

	private VersionController VersionController;
	public bool unlocked = false;
	public string levelName;
	private LevelSelectScript LevelSelectScript;
	private LevelsControllerCS LevelsController;
	public GameObject LockedPanel;

	void Awake () {

		// VersionController = GameObject.Find("VersionController").GetComponent<VersionController>(); //--a singleton
		LevelsController = GameObject.Find("LevelsController").GetComponent<LevelsControllerCS>(); //--a singleton
		LevelSelectScript = GameObject.Find("GameController").GetComponent<LevelSelectScript>();
	}

	void CheckIfLocked (){
		// if(VersionController.paidVersion == true)
		// {
		// 	unlocked = true;
		// 	Debug.Log("this is the paid version - unlocked "+levelName);

		// } else {
		// 	Debug.Log(levelName+" check if unlocked");
		// 	//--now check if this particular level has been unlocked
		// 	Debug.Log("level "+levelName+"unlocked = "+PlayerPrefs.GetInt(levelName+"Unlocked"));
		// 	if(PlayerPrefs.GetInt(levelName+"Unlocked") == 1){
		// 		unlocked = true;
		// 		Debug.Log(levelName+" is unlocked");
		// 	} else {
		// 		Debug.Log(levelName+" is locked");
		// 	}
		// }

		// if( unlocked == true){
		// 	UnlockButton();
		// }
	}

	void UnlockButton (){
		//--hide the padlock overlay for this button
		Debug.Log("unlock button for "+levelName);

		LockedPanel.SetActive(false);
	}

	public void PressButton(){
		//--button action
		if(unlocked){
			LevelsController.SelectLevel(levelName);
		} else {
			LevelSelectScript.ShowUnlockModal(levelName);
		}
	}
}










