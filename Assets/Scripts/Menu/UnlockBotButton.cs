using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--this stores the current selected string for this bot - used by playerSelectScript when pressed

public class UnlockBotButton : MonoBehaviour {

	public string selectedBot;
	public int playerNum;
	public PlayerSelectScriptCS PlayerSelectScript;

	// Use this for initialization
	void Start () {
		selectedBot = "Cog";
		playerNum = 1;
	}
	
	public void PressedUnlockButton() {
		Debug.Log("p"+playerNum+"pressed unlock button for "+selectedBot);
		PlayerSelectScript.showUnlockModal(selectedBot); 
	}
}