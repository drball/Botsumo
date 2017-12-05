﻿#pragma strict
import UnityEngine.UI;

public var ScoreModal : GameObject;
public var Player1 : PlayerScript;
public var Player2 : PlayerScript;
public var Player1Movement : PlayerMovement;
public var Player2Movement : PlayerMovement;
public var roundActive : boolean = true;
public var Player1ScoreText : GameObject;
public var Player2ScoreText : GameObject;
public var PlayAgainBtn : GameObject;
public var SinglePlayer : boolean = false;
public var RBtn : GameObject; //--used for removing this when in single player mode
public var RInstruction : GameObject;

public var test : GameObject;

private var winningScore : int = 5;
private var defaultPlayer : String = "Pooper"; //A B C Cog, SpinningArms, Solar
private var AdvertController : AdvertController;
private var LevelsController : LevelsController; 

function Start () {
	//--hide the score modal so we can show it later
	ScoreModal.SetActive(false);
	
	Player1 = LoadPlayer("Player1Dummy", 1).transform.GetComponent.<PlayerScript>();
	Player2 = LoadPlayer("Player2Dummy", 2).transform.GetComponent.<PlayerScript>();
	Player1Movement = Player1.GetComponent.<PlayerMovement>();
	Player2Movement = Player2.GetComponent.<PlayerMovement>();
	
	//--hide the "play again" button initially, so we can show it later
	PlayAgainBtn.SetActive(false);
	
	//--set the players to know what they are 
	if(PlayerSelectScript.p1SelectedCharString){
		Player1.playerCharacter = PlayerSelectScript.p1SelectedCharString;
	}else {
		Player1.playerCharacter = defaultPlayer;
	}
	
	if(PlayerSelectScript.p2SelectedCharString){
		Player2.playerCharacter = PlayerSelectScript.p2SelectedCharString;
	}else {
		Player2.playerCharacter = defaultPlayer;
	}

	LevelsController = GameObject.Find("LevelsController").GetComponent.<LevelsController>();

	test = Player2.gameObject.Find("PlayerSeeker");

	//--if the game is single player, disable the normal player movement script
	//--and activate the object containing single player scripts
	if(LevelsController.singlePlayer) {
		Debug.Log("this is single player mode");
		Player2.GetComponent.<PlayerMovement>().enabled = false;
		Player2.gameObject.Find("PlayerSeeker").SetActive(true);
		RBtn.SetActive(false);
		RInstruction.SetActive(false);

	}else {
		Debug.Log("disable playerseeker");
		Player2.gameObject.Find("PlayerSeeker").SetActive(false);
		RBtn.SetActive(true);
	}

	Debug.Log("start the level");
	gameObject.SendMessage("StartLevel",0);

	//--get the advert script
	AdvertController = GetComponent.<AdvertController>(); 
}

function LoadPlayer(dummyObjName, playerNum){

	//--load the chosen player dynamically based on what was chosen
	var PlayerDummy : GameObject = GameObject.Find(dummyObjName);
	
	//--we should get the chosen player from playerSelection screen, if not load a default
	if(PlayerSelectScript.p1SelectedCharString && PlayerSelectScript.p2SelectedCharString)
	{
		//--build the string of the player to replace the dummy with
		var playerToLoad = "Player" + playerNum;
		if(playerNum == 1){
			playerToLoad += PlayerSelectScript.p1SelectedCharString;
		}else {
			playerToLoad += PlayerSelectScript.p2SelectedCharString;
		}
	}else{
		playerToLoad = "Player" + playerNum + defaultPlayer;
		
		//--for debug - if we load this scene without the player selection
		if(playerNum == 1){
			playerToLoad = "Player1Pooper";
		}
	}
	
	Destroy(PlayerDummy);
	
	//--load from "resources"
	var playerInstance : GameObject = Instantiate(Resources.Load(playerToLoad, GameObject),
		PlayerDummy.transform.position, 
		PlayerDummy.transform.rotation
	);

	Debug.Log("loaded "+playerInstance.name);
	
	return playerInstance;
}


function Reset(){

	Debug.Log("resetting scene -----------------------------------------");

	//--reset physics
	Player1.GetComponent.<Rigidbody>().angularVelocity = Vector3.zero;
	Player2.GetComponent.<Rigidbody>().angularVelocity = Vector3.zero;
	
	Player1.GetComponent.<Rigidbody>().velocity = Vector3.zero;
	Player2.GetComponent.<Rigidbody>().velocity = Vector3.zero;
	
	
	//--reset position
	Player1.Respot();
	Player2.Respot();

	//--reset their local variables
	Player1.alive = true;
	Player2.alive = true;
	
	roundActive = true;

	//--send message to all scripts, including level-specific ones (like UnstablePlatform level)
	gameObject.SendMessage("ResetLevel",0);

	yield WaitForSeconds(1);
	
	//--make sure these are hidden so we can activate them later
	ScoreModal.SetActive(false);
	PlayAgainBtn.SetActive(false);

}


function EndRound() {

	roundActive = false;
	Debug.Log("ending round in 2");
	yield WaitForSeconds(1);
	Debug.Log("ending round in 1");
	yield WaitForSeconds(1);
	
	//--show modal
	ScoreModal.SetActive(true);
	
	//--determine who won
	if(!Player1.alive) {
		Player2.score++;
	}
	
	if(!Player2.alive) {
		Player1.score++;
	}
	
	
	//--update leaderboard after a few seconds
	yield WaitForSeconds(1.5);
	
	Player1ScoreText.GetComponent.<Text>().text = Player1.score.ToString();
	Player2ScoreText.GetComponent.<Text>().text = Player2.score.ToString();
	
	yield WaitForSeconds(1.5);
	
	if((Player1.score >= winningScore) || (Player2.score >= winningScore)){
		//--someone has won
		
		Debug.Log("someone has won");
		
		//--show "play again" button
		PlayAgainBtn.SetActive(true);

		yield WaitForSeconds(1);

		AdvertController.ShowAdvert();
		
		
	}else {
		//--keep playing
		
		//--animate out
		ScoreModal.GetComponent.<Animator>().Play("PanelSlideOut");
		
		Reset();
		
	}
}

//function Update()
//{
//	if(Input.GetKeyDown(KeyCode.Escape) == true)
//	{
//		Application.LoadLevel ("menu");
//	}
//}


function PlayAgain (){

	//--a full reset

	Debug.Log("play again");
	
	Reset();
	
	Player1.score = 0;
	Player2.score = 0;
	
	//--animate these away
	PlayAgainBtn.GetComponent.<Animator>().Play("PanelSlideOut");
	ScoreModal.GetComponent.<Animator>().Play("PanelSlideOut");
	
	//--reset the text boxes
	Player1ScoreText.GetComponent.<Text>().text = "0";
	Player2ScoreText.GetComponent.<Text>().text = "0";
	
	//--reload entire scene
	LevelsController.LoadSelectedLevel();
	
}

function MovePlayer1(moving : boolean){

	//--called when the button is pressed or stopped pressing - pass this to player
	Player1Movement.Move(moving);
}

function MovePlayer2(moving : boolean){

	//--called when the button is pressed or stopped pressing - pass this to player
	Player2Movement.Move(moving);
	
}


//--for debug
function FixedUpdate () {
	if(Input.GetKey(KeyCode.LeftArrow)) {
		Player1Movement.Move(true);
	}

	if (Input.GetKeyUp(KeyCode.LeftArrow)){
		Player1Movement.Move(false);
	}
	
	if(Input.GetKey(KeyCode.RightArrow)) {
		Player2Movement.Move(true);
	}

	if (Input.GetKeyUp(KeyCode.RightArrow)){
		Player2Movement.Move(false);
	}
}

