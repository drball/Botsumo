using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectScriptCS : MonoBehaviour {

	//--create array of the possible different player characters 
	private string[] playerCharacters = new string[] {
		"A", 
		"B", 
		"C", 
		"SpinningArms", 
		"Cog", 
		"Solar",
		"Razorback", 
		"Pooper", 
		"Flat"
	};

	public GameObject[] p1GameObjects; //--array of characters
	public GameObject[] p2GameObjects; //--array of characters
	public GameObject P1Btn;	//--ref to btn for disabling it
	public GameObject P2Btn;	//--ref to btn for disabling it
	public GameObject LoadingPanel;
	public GameObject P1WaitMsg;
	public GameObject P2WaitMsg;
	public GameObject UnlockP1Btn;
	public GameObject UnlockP2Btn;
	public GameObject UnlockModal;
	public UnlockBotButton UnlockP1BtnScript;
	public UnlockBotButton UnlockP2BtnScript;

	public bool isCogUnlocked;
	public bool isSolarUnlocked;
	public bool isRazorbackUnlocked;
	public bool isPooperUnlocked;
	public bool isFlatUnlocked;

	private int numPlayers = 2;
	private int p1VisibleChar = 0;
	private int p2VisibleChar = 0;
	private LevelsControllerCS LevelsController;
	private VersionController VersionController;
	public string UnlockingBot; //--used to remember which bot was unlocked while advert plays

	//--the selected character - these used in the next scene
	static string p1SelectedCharString; 
	static string p2SelectedCharString;

	void Start () {
	
		p1SelectedCharString = "";
		p2SelectedCharString = "";
		
		LoadingPanel.SetActive(false);

		P1WaitMsg.SetActive(false);
		P2WaitMsg.SetActive(false);

		//--check whether cogbot has been unlocked 
		isCogUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockedCog"));
		isSolarUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockedSolar"));
		isRazorbackUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockedRazorback"));
		isPooperUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockedPooper"));
		isFlatUnlocked = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockedFlat"));

		Debug.Log("solar unlocked = "+isSolarUnlocked);

		if(GameObject.Find("LevelsController")){
			LevelsController = GameObject.Find("LevelsController").GetComponent<LevelsControllerCS>(); //--loading in menu. Persistant
			
		}else {
			Debug.Log("not found levelscontroller");
		}
		
		if(GameObject.Find("VersionController")){
			VersionController = GameObject.Find("VersionController").GetComponent<VersionController>();
		} else {
			Debug.Log("not found VersionController");
		}

		//--hide all the characters apart from the 1st
		showOnlyP1Character(p1VisibleChar);
		showOnlyP2Character(p2VisibleChar);

		closeUnlockModal();
	}

	public void selectCharacter(int playerNum) {
		//--when the "select" button pressed, set the variable & disable the btn
		
		if(playerNum == 1) {
			Debug.Log("p1VisibleChar] = "+p1VisibleChar);
			Debug.Log("playerCharacters[p1VisibleChar] = "+playerCharacters[p1VisibleChar]);
			p1SelectedCharString = playerCharacters[p1VisibleChar];
			LevelsController.p1SelectedCharString = p1SelectedCharString;
			P1Btn.SetActive(false);
			P1WaitMsg.SetActive(true);
		}else {
			p2SelectedCharString = playerCharacters[p2VisibleChar];
			LevelsController.p2SelectedCharString = p2SelectedCharString;
			P2Btn.SetActive(false);
			P2WaitMsg.SetActive(true);
		}

		//--load next level if both selected
		if((p1SelectedCharString.Length > 0) && (p2SelectedCharString.Length > 0)){
			Debug.Log("both ready!");

			//--report the character which has been chosen to analytics
			gameObject.SendMessage("ChoseCharacter", p1SelectedCharString);
			gameObject.SendMessage("ChoseCharacter", p2SelectedCharString);
			
			//--show loading panel because there's a delay
			LoadingPanel.SetActive(true);
			
			//--load the main level
			LevelsController.LoadSelectedLevel();
		}
	}

	void showOnlyP1Character (int charToShow) {

		//--hide all characters
		for(int i = 0; i < p1GameObjects.Length; i++){
			// Debug.Log("p1 setting "+i+" to inactive");
			p1GameObjects[i].SetActive(false);
		}
		
		//--show the selected char
		p1GameObjects[charToShow].SetActive(true);

		if((charToShow == 4) && (!isCogUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p1 has selected cog");
			P1Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP1Btn.SetActive(true);
			UnlockP1BtnScript.selectedBot = "Cog";

		} else if((charToShow == 5) && (!isSolarUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p1 has selected solar");
			P1Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP1Btn.SetActive(true);
			UnlockP1BtnScript.selectedBot = "Solar";

		} else if((charToShow == 6) && (!isRazorbackUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p1 has selected razorback");
			P1Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP1Btn.SetActive(true);
			UnlockP1BtnScript.selectedBot = "Razorback";

		} else if((charToShow == 7) && (!isPooperUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p1 has selected pooper");
			P1Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP1Btn.SetActive(true);
			UnlockP1BtnScript.selectedBot = "Pooper";

		} else if((charToShow == 8) && (!isFlatUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p1 has selected pooper");
			P1Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP1Btn.SetActive(true);
			UnlockP1BtnScript.selectedBot = "Flat";

		} else {
			UnlockP1Btn.SetActive(false);
		}

		// if(charToShow == 6){
		// 	P1Btn.GetComponent.<Button>().interactable = false; //--this bots btn should be disabled because it's a tease
		// }
	}

	void showOnlyP2Character (int charToShow) {

		//--hide all characters
		for(int i = 0; i < p2GameObjects.Length; i++){
			// Debug.Log("p2 setting "+i+" to inactive");
			p2GameObjects[i].SetActive(false);
		}

		Debug.Log("show char "+charToShow);
		
		//--show the selected char
		p2GameObjects[charToShow].SetActive(true);

		if((charToShow == 4) && (!isCogUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p2 has selected cog");
			P2Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP2Btn.SetActive(true);
			UnlockP2BtnScript.selectedBot = "Cog";

		} else if((charToShow == 5) && (!isSolarUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p2 has selected solar");
			P2Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP2Btn.SetActive(true);
			UnlockP2BtnScript.selectedBot = "Solar";

		} else if((charToShow == 6) && (!isRazorbackUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p2 has selected razorback");
			P2Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP2Btn.SetActive(true);
			UnlockP2BtnScript.selectedBot = "Razorback";

		} else if((charToShow == 7) && (!isPooperUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p2 has selected pooper");
			P2Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP2Btn.SetActive(true);
			UnlockP2BtnScript.selectedBot = "Pooper";

		} else if((charToShow == 8) && (!isFlatUnlocked) && (VersionController.paidVersion == false)){
			Debug.Log("p2 has selected flat");
			P2Btn.GetComponent<Button>().interactable = false; //--this bots btn should be disabled
			//--show unlock button 
			UnlockP2Btn.SetActive(true);
			UnlockP2BtnScript.selectedBot = "Flat";

		} else {
			UnlockP2Btn.SetActive(false);
		}

		// if(charToShow == 6){
		// 	P2Btn.GetComponent.<Button>().interactable = false; //--this bots btn should be disabled because it's a tease
		// }
	}

	public void NextCharacter (int playerNum) {

		Debug.Log("next character. p"+playerNum);
	
		if (playerNum == 1) {
			p1VisibleChar++;

			P1Btn.GetComponent<Button>().interactable = true; //--enable because "coming soon" bot might have disabled it
			
			//-- show only next  character (or reset)
			if(p1VisibleChar >= p1GameObjects.Length) {
				p1VisibleChar = 0;
			}
			showOnlyP1Character(p1VisibleChar);

		} else {
			p2VisibleChar++;

			P2Btn.GetComponent<Button>().interactable = true; //--enable because "coming soon" bot might have disabled it
			
			if(p2VisibleChar >= p2GameObjects.Length) {
				p2VisibleChar = 0;
			}
			showOnlyP2Character(p2VisibleChar);
		}
	}

	public void PrevCharacter (int playerNum) {
	
		if (playerNum == 1) {
			p1VisibleChar--;

			P1Btn.GetComponent<Button>().interactable = true; //--enable because "coming soon" bot might have disabled it
			
			if(p1VisibleChar < 0) {
				p1VisibleChar = p1GameObjects.Length - 1;
			}
			showOnlyP1Character(p1VisibleChar);
		}else {
			p2VisibleChar--;

			P2Btn.GetComponent<Button>().interactable = true; //--enable because "coming soon" bot might have disabled it
			
			if(p2VisibleChar < 0) {
				p2VisibleChar = p2GameObjects.Length - 1;
			}
			showOnlyP2Character(p2VisibleChar);
		}
	}

	public void showUnlockModal(string selectedCharacterName){

		//--unlock the currently selected character
		Debug.Log("Showing modal for "+selectedCharacterName);
		UnlockingBot = selectedCharacterName;
		UnlockModal.SetActive(true);
	}

	public void closeUnlockModal(){
		UnlockModal.SetActive(false);
	}


	void unlockSelectedBot(){
		//--called by a sendmessage from RewardedAd
		Debug.Log("unlocking "+UnlockingBot);
		PlayerPrefs.SetInt("Unlocked"+UnlockingBot, 1);
		P1Btn.GetComponent<Button>().interactable = true;
		P2Btn.GetComponent<Button>().interactable = true;

		//--set the variables
		if(UnlockingBot == "Cog"){
			isCogUnlocked = true;
		} else if (UnlockingBot == "Solar"){
			isSolarUnlocked = true;
		} else if (UnlockingBot == "Razorback"){
			isRazorbackUnlocked = true;
		} else if (UnlockingBot == "Pooper"){
			isRazorbackUnlocked = true;
		} else if (UnlockingBot == "Flat"){
			isRazorbackUnlocked = true;
		}

		if (UnlockP1BtnScript.selectedBot == UnlockingBot){
			UnlockP1Btn.SetActive(false);
		};

		if (UnlockP2BtnScript.selectedBot == UnlockingBot){
			UnlockP2Btn.SetActive(false);
		};
	}

	public void ToggleSinglePlayer(bool singlePlayerSelection){
		// Debug.Log("----------Toggle is "+singlePlayerSelection);
		LevelsController.singlePlayer = singlePlayerSelection;
	}
	
}





















