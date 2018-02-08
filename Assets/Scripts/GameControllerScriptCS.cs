using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScriptCS : MonoBehaviour {

	public GameObject ScoreModal;
	public GameObject Player1;
	public GameObject Player2;
	private PlayerMovementCS Player1Movement;
	private PlayerMovementCS Player2Movement;
	public bool roundActive = true;
	public GameObject Player1ScoreText;
	public GameObject Player2ScoreText;
	public GameObject PlayAgainBtn;
	public bool SinglePlayer = false;
	public GameObject RBtn; //--used for removing this when in single player mode
	public GameObject RInstruction;
	private PlayerScriptCS Player1Script;
	private PlayerScriptCS Player2Script;

	private int winningScore = 5;
	private string defaultPlayer = "Cog"; //A B C Cog, SpinningArms, Solar
	private AdvertControllerCS AdvertController;
	private LevelsControllerCS LevelsController; 


	void Awake () {

		LevelsController = GameObject.Find("LevelsController").GetComponent<LevelsControllerCS>();

		//--hide the score modal so we can show it later
		ScoreModal.SetActive(false);
		
		Player1 = LoadPlayer("Player1Dummy", 1);
		Player2 = LoadPlayer("Player2Dummy", 2);
		Player1Script = Player1.GetComponent<PlayerScriptCS>();
		Player2Script = Player2.GetComponent<PlayerScriptCS>();
		Player1Movement = Player1.GetComponent<PlayerMovementCS>();
		Player2Movement = Player2.GetComponent<PlayerMovementCS>();
		
		//--hide the "play again" button initially, so we can show it later
		PlayAgainBtn.SetActive(false);
		
		//--set the players to know what they are 
		if(LevelsController.p1SelectedCharString.Length > 0){
			Player1Script.playerCharacter = LevelsController.p1SelectedCharString;
		}else {
			Player1Script.playerCharacter = defaultPlayer;
		}
		
		if(LevelsController.p2SelectedCharString.Length > 0){
			Player1Script.playerCharacter = LevelsController.p2SelectedCharString;
		}else {
			Player1Script.playerCharacter = defaultPlayer;
		}

		//--if the game is single player, disable the normal player movement script
		//--and activate the object containing single player scripts
		if(LevelsController.singlePlayer) {
			Debug.Log("this is single player mode");
			Player2.GetComponent<PlayerMovementCS>().enabled = false;
			Player2.transform.Find("PlayerSeeker").gameObject.SetActive(true); 
			RBtn.SetActive(false);
			RInstruction.SetActive(false);

		}else {
			Debug.Log("disable playerseeker");
			Player2.transform.Find("PlayerSeeker").gameObject.SetActive(false);
			RBtn.SetActive(true);
		}

		Debug.Log("start the level");
		gameObject.SendMessage("StartLevel",0);

		//--get the advert script - load last
		AdvertController = GetComponent<AdvertControllerCS>(); 
	}

	GameObject LoadPlayer(string dummyObjName, int playerNum){

		//--load the chosen player dynamically based on what was chosen
		GameObject PlayerDummy = GameObject.Find(dummyObjName);
		string playerToLoad = "Player" + playerNum;
		
		//--we should get the chosen player from playerSelection screen, if not load a default
		if((LevelsController.p1SelectedCharString.Length > 0) && (LevelsController.p2SelectedCharString.Length > 0))
		{
			//--build the string of the player to replace the dummy with
			if(playerNum == 1){
				playerToLoad += LevelsController.p1SelectedCharString;
			}else {
				playerToLoad += LevelsController.p2SelectedCharString;
			}
		}else{
			playerToLoad = "Player" + playerNum + defaultPlayer;
			
			//--for debug - if we load this scene without the player selection
			if(playerNum == 1){
				playerToLoad = "Player1Cog";
			}
		}
		
		Destroy(PlayerDummy,0);
		Debug.Log("destroyed player dummy");
		
		//--load from "resources"
		GameObject playerInstance = Instantiate(Resources.Load(playerToLoad, typeof(GameObject)),
			PlayerDummy.transform.position, 
			PlayerDummy.transform.rotation
		) as GameObject;

		// Debug.Log("loaded "+playerInstance.name);
		
		return playerInstance;
	}


	void Reset(){

		// Debug.Log("resetting scene -----------------------------------------");

		//--reset physics
		Player1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		Player2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		
		Player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		
		//--reset position
		Player1Script.Respot();
		Player2Script.Respot();

		//--reset their local variables
		Player1Script.alive = true;
		Player2Script.alive = true;
		
		roundActive = true;

		//--send message to all scripts, including level-specific ones (like UnstablePlatform level)
		gameObject.SendMessage("ResetLevel",0);


		Invoke("HideScoreUI",3f);
	}

	void HideScoreUI(){
		//--make sure these are hidden so we can activate them later
		ScoreModal.SetActive(false);
		PlayAgainBtn.SetActive(false);
	}

	IEnumerator ShowScoreUI(){

		Debug.Log("ending round in 2");
		yield return new WaitForSeconds(1.0f);
		Debug.Log("ending round in 1");
		yield return new WaitForSeconds(1.0f);

		//--show modal
		ScoreModal.SetActive(true);
		
		//--determine who won
		if(!Player1Script.alive) {
			Player2Script.score++;
		}
		
		if(!Player2Script.alive) {
			Player1Script.score++;
		}
		
		//--update leaderboard after a few seconds
		yield return new WaitForSeconds(1.5f);
		
		Player1ScoreText.GetComponent<Text>().text = Player1Script.score.ToString();
		Player2ScoreText.GetComponent<Text>().text = Player2Script.score.ToString();
		
		yield return new WaitForSeconds(1.5f);
		
		if((Player1Script.score >= winningScore) || (Player2Script.score >= winningScore)){
			//--someone has won
			
			Debug.Log("someone has won");
			
			//--show "play again" button
			PlayAgainBtn.SetActive(true);

			//--show advert after a few seconds 
			yield return new WaitForSeconds(1.0f);

			AdvertController.ShowAdvert();
			
		}else {
			//--keep playing
			
			//--animate out
			ScoreModal.GetComponent<Animator>().Play("PanelSlideOut");
			
			Reset();
		}
	}


	public void EndRound() {

		roundActive = false;

		StartCoroutine(ShowScoreUI());
		
	}


	public void PlayAgain (){

		//--a full reset
		Debug.Log("play again");
		
		Reset();
		
		Player1Script.score = 0;
		Player2Script.score = 0;
		
		//--animate these away
		PlayAgainBtn.GetComponent<Animator>().Play("PanelSlideOut");
		ScoreModal.GetComponent<Animator>().Play("PanelSlideOut");
		
		//--reset the text boxes
		Player1ScoreText.GetComponent<Text>().text = "0";
		Player2ScoreText.GetComponent<Text>().text = "0";
		
		//--reload entire scene
		LevelsController.LoadSelectedLevel();
		
	}

	public void MovePlayer1(bool moving){
		//--called when the button is pressed or stopped pressing - pass this to player
		Player1Movement.Move(moving);
	}

	public void MovePlayer2(bool moving){
		//--called when the button is pressed or stopped pressing - pass this to player
		Player2Movement.Move(moving);
	}


	//--for debug
	void FixedUpdate () {
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
}