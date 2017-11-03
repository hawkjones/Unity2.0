﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	static LevelManager instance = null;
    private int thisIndex;

    struct Stats
    {
        int dronesDestroyed;
        int sceneIndex;
        int score;
        float finishTime;
        string playerInitials;
    }
    Stats level_01, level_02, level_03, total, unused;

    Dictionary<int, Stats> levelMap = new Dictionary<int, Stats>();
    
	// TODO: droneStore sprites? private SpriteRenderer ball1, ball2, ball3, ball4, ball5;

	// TODO working on structure to expunge relic effects REE
	private ArrayList deadEffects = new ArrayList();
	private Color offColor = new Color (0f, 0f, 0f, 0f), onColor = new Color (1f, 1f, 1f, 0.667f);
	private Text scoreBoard;

    void Start()
    {
        if (instance != null && instance != this) { Destroy(gameObject); }
        else { instance = this; GameObject.DontDestroyOnLoad(gameObject); }
        levelMap.Add(2, level_01);
        levelMap.Add(3, level_02);
        levelMap.Add(4, level_03);

        //		scoreBoard = GameObject.Find ("ScoreBoard").GetComponent<Text>();
        //		ShowMyBalls ();
    }

    public void OrbCollected()
    {
        Stats stats = FetchStats();
        

    }

    private Stats FetchStats()
    {
        thisIndex = SceneManager.GetActiveScene().buildIndex;
     //   if (thisIndex == 1) return levelMap.
        if (thisIndex == 2) return level_01;
        if (thisIndex == 3) return level_02;
        if (thisIndex == 4) return level_03;
        return total; // TODO: doing this on the fly, may be unnec.
    }

	//public void BrickCountMinus () { brickCount--; BrickDestroyed(); }
	//public void BrickCountPlus () { brickCount++; }
	//public void BrickDestroyed() { if (brickCount <= 0) LoadNextLevel(); }
	//public int BrickGetNumRemaining () { return brickCount; } 
	public void EffectAdd (GameObject preDE) { deadEffects.Add (preDE); }
	//public int GetSceneIndex () { return sceneIndex; }
	//public void HasStartedFalse() { hasStarted = false; }
	//public bool HasStartedReturn () { return hasStarted; }
	//public void HasStartedToggle() { hasStarted = !hasStarted; }
	//public void HasStartedTrue() { hasStarted = true; }

    void Update () {
		ExpungeDeadEffects();
	//	if (!scoreBoard) scoreBoard = GameObject.Find ("ScoreBoard").GetComponent<Text>();
	//	if (scoreBoard) scoreBoard.text = ("High: " + score + "  -  [Highest: " + PlayerPrefsManager.GetTopscore() + "]");
	//	else Debug.LogError ("Levelmanager.cs Update() Unable to update Scoreboard");
	}

	public void BallDown() {
	//	if (ballCount-- <= 0) {
	//		brickCount = 0;
	//		if (PlayerPrefsManager.GetTopscore () < score) PlayerPrefsManager.SetTopscore (score);
			LoadLevel("Game Over");
	//	}
		ShowMyBalls ();
	}

	public void  CalculateScoreFactor () {
	//	if (PlayerPrefsManager.GetTrails()) scoreFactor = 1.25f;
	//	if (PlayerPrefsManager.GetFireBalls()) scoreFactor = 1.3f;
	//	if (PlayerPrefsManager.GetFireBalls() && PlayerPrefsManager.GetTrails()) scoreFactor = 2.0f;
	//	if (PlayerPrefsManager.GetEasy()) scoreFactor = (scoreFactor * .7f);
	//	if (PlayerPrefsManager.GetAutoplay()) scoreFactor = (scoreFactor * 0.2f);
	}

	void ConfigureAnyLevel () {
		Cursor.visible = true;
	//	brickCount = 0;
	//	hasStarted = false;
	}

	void ConfigureLevelOne () {
	//	ballCount = 2;
		Cursor.visible = false;
	//	sceneIndex = 1;
	//	score = 0;
		PlayerPrefsManager.SetAward(0);
	}

	// TODO this is not working as advertised.... the used game objects linger in the effects "folder" game object **some scenes are okay?
	void ExpungeDeadEffects () {
		foreach (GameObject de in deadEffects) { // more stuff for REE
			if (de && !de.GetComponent<ParticleSystem>().IsAlive()) {
				Destroy (de);
			}
		}
	}

	public void FreeBallin () { // set reward levels where free plays are granted
		if (PlayerPrefsManager.GetAward() == 0) {
	//		if (score > 2000 && score < 10000) { // TODO tweak these score thresholds id needed
			//	ballCount++;
				ShowMyBalls();
				PlayerPrefsManager.SetAward(1);
	//		}
		}
		else if (PlayerPrefsManager.GetAward() == 1) {
	//		if (score > 10000 && score < 25000) {
			//	ballCount++;
				ShowMyBalls();
				PlayerPrefsManager.SetAward(2);
	//		}
		}
		else if (PlayerPrefsManager.GetAward() == 2) {
	//		if (score > 25000) {
			//	ballCount++;
				ShowMyBalls();
				PlayerPrefsManager.SetAward(3);
	//		}
		}
	}

	public void LoadLevel(string name){
		StoreHighs();
		ConfigureAnyLevel();
		if (name == "Level_01") ConfigureLevelOne (); 
		SceneManager.LoadScene(name);
	}
	
	void LoadNextLevel() {
		StoreHighs();
        // brickcount = 0;
		//hasStarted = false;
		//sceneIndex++;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}

	public void ShowMyBalls () {
	/*	if (GameObject.FindGameObjectWithTag ("ball1")) {
			ball1 = GameObject.FindGameObjectWithTag ("ball1").GetComponent<SpriteRenderer>();
			if (ballCount > 0) ball1.color = onColor;
			if (ballCount < 1) ball1.color = offColor;
		}
		if (GameObject.FindGameObjectWithTag ("ball2")) {
			ball2 = GameObject.FindGameObjectWithTag ("ball2").GetComponent<SpriteRenderer>();
			if (ballCount > 1) ball2.color = onColor;
			if (ballCount < 2) ball2.color = offColor;
		}
		if (GameObject.FindGameObjectWithTag ("ball3")) {
			ball3 = GameObject.FindGameObjectWithTag ("ball3").GetComponent<SpriteRenderer>();
			if (ballCount > 2) ball3.color = onColor;
			if (ballCount < 3) ball3.color = offColor;
		}
		if (GameObject.FindGameObjectWithTag ("ball4")) {
			ball4 = GameObject.FindGameObjectWithTag ("ball4").GetComponent<SpriteRenderer>();
			if (ballCount > 3) ball4.color = onColor;
			if (ballCount < 4) ball4.color = offColor;
		}
		if (GameObject.FindGameObjectWithTag ("ball5")) {
			ball5 = GameObject.FindGameObjectWithTag ("ball5").GetComponent<SpriteRenderer>();
			if (ballCount > 4) ball5.color = onColor;
			if (ballCount < 5) ball5.color = offColor;
		}
        */
	}

	void StoreHighs () {
		//if (PlayerPrefsManager.GetTopscore () < score) PlayerPrefsManager.SetTopscore (score);
	}
}
