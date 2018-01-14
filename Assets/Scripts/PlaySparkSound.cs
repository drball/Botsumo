using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySparkSound : MonoBehaviour {

	public AudioSource Sfx;

	// Use this for initialization
	void Start () {
	
		Sfx.pitch = Random.Range(0.8f, 1.25f);
		Sfx.Play();
	}

	
}
