using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI txt;
    public SceneDeciding loadScene;
	public GameObject failTxt;
	public delegate void TimeUpdate();
	public string pickLevel;
    
    //Counts down time remaining to get out of maze
    void Update()
    {
		TimeUpdate del = UpdateTime;
        if (timer > 0)
		{
			del();
		}
        else
        {
			StartCoroutine(EndGame(failTxt));
        }
    }
	//If timer reaches 0, fail message pops up and taken back to home screen
	public IEnumerator EndGame(GameObject conditionTxt)
	{
		conditionTxt.SetActive(true);
		yield return new WaitForSeconds(3);
		loadScene.NextScene(pickLevel);
	}
	public void UpdateTime()
	{
		timer -= Time.deltaTime;
	    txt.text = (timer.ToString("F1") + "s");
	}
}
