using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsControllerCS : MonoBehaviour {

	public string currentLevel;
	public GameObject LoadingDialog; 
	public bool singlePlayer;
	public string p1SelectedCharString; //--so we can access from anywhere
	public string p2SelectedCharString;

	void Awake () {

		//--because this is a singleton, we want only onee
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			//--destroy others like this
			Debug.Log("destroying this duplicate of LevelsController");
			Destroy(gameObject);
		}

		DontDestroyOnLoad(this);
	}

	void Start() {
		Debug.Log("LevelsController has started");
		
		HideLoadingDialog();
	}

	public void SelectLevel(string destinationTitle){
		//--select a level - then load the playerSelect screen
		LoadingDialog.SetActive(true);
		//yield WaitForSeconds(0.25f);

		currentLevel = destinationTitle;
		Debug.Log("levelscontroller is remembering level "+destinationTitle);
		Application.LoadLevel("playerSelect");
	}

	public void LoadSelectedLevel(){
		//--load the level we selected earlier
		//--called from the playerSelect screen
		ShowLoadingDialog();

		//yield WaitForSeconds(0.25f);

		Debug.Log("levelscontroller is loading level "+currentLevel);
		Application.LoadLevel(currentLevel);
	}

	void ShowLoadingDialog(){
		Debug.Log("show loading");
		LoadingDialog.SetActive(true);
	}

	void HideLoadingDialog(){
		Debug.Log("hide loading");
		LoadingDialog.SetActive(false);
	}
}