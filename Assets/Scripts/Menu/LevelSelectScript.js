// #pragma strict

// public var LevelsController : LevelsController;
// public var unlockModal : GameObject; 
// public var levelSelectedToUnlock : String; 
// public var levelButtons : GameObject[];

// private var VersionController : VersionController;

// function Start () {
// 	LevelsController = GameObject.Find("LevelsController").GetComponent.<LevelsController>();
// 	VersionController = GameObject.Find("VersionController").GetComponent.<VersionController>();
// 	HideUnlockModal();

// 	CheckIfLevelsUnlocked();
	
// }

// function CheckIfLevelsUnlocked(){
// 	for(var obj in levelButtons) {
// 		// Debug.Log("check "+obj.name);
// 		obj.SendMessage("CheckIfLocked");
// 	}
// }

// function LoadMainLevel(){
// 	// Application.LoadLevel ("main");
// 	// LevelsController.currentLevel = "main";
// 	LevelsController.SelectLevel("main");
// }

// function ShowUnlockModal(levelName : String){
// 	Debug.Log("loading modal for "+levelName);
// 	levelSelectedToUnlock = levelName;
// 	unlockModal.SetActive(true);
// }

// function HideUnlockModal(){
// 	unlockModal.SetActive(false);
// }

// function DownloadPaidVersionBtn (){
// 	Application.OpenURL("https://play.google.com/store/apps/details?id=com.DavidDickBall.BotSumoBattleArena");
// }

// function UnlockLevelBtn (){
// 	//--unlock button action
// 	Debug.Log("user chose to unlock level "+levelSelectedToUnlock);

// 	SendMessage("StartRewardedAd");
// 	HideUnlockModal();
// }

// function UnlockCurrentLevel(){
// 	Debug.Log("unlock level "+levelSelectedToUnlock);

// 	PlayerPrefs.SetInt(levelSelectedToUnlock+"Unlocked",1);

// 	CheckIfLevelsUnlocked();
// }