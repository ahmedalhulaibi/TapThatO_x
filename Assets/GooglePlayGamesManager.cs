using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;

public class GooglePlayGamesManager : MonoBehaviour {
	public static GooglePlayGamesManager gpgManager;
	public bool IsSignedIn = false;

	public string[] NonIncrementAchievements_NAME;
	public string[] NonIncrementAchievements_GPGVAL;

	public string[] IncrementAchievements_NAME;
	public string[] IncrementAchievements_GPGVAL;

	public string[] Leaderboards_NAME;
	public string[] Leaderboards_GPGVAL;

	public string[] Events_NAME;
	public string[] Events_GPGVAL;

	public Dictionary<string,string> NonIncrementAchievements = new Dictionary<string, string> ();
	public Dictionary<string,string> IncrementAchievements = new Dictionary<string, string> ();
	public Dictionary<string,string> Leaderboards = new Dictionary<string, string> ();
	public Dictionary<string,string> Events = new Dictionary<string, string> ();

	// Use this for initialization
	void Start () {
		gpgManager = this;

		for (int i = 0; i < NonIncrementAchievements_NAME.GetLength(0); i++) {
			NonIncrementAchievements.Add(NonIncrementAchievements_NAME[i],NonIncrementAchievements_GPGVAL[i]);
		}

		for (int i = 0; i < IncrementAchievements_NAME.GetLength(0); i++) {
			IncrementAchievements.Add(IncrementAchievements_NAME[i],IncrementAchievements_GPGVAL[i]);
		}
		
		for (int i = 0; i < Leaderboards_NAME.GetLength(0); i++) {
			Leaderboards.Add(Leaderboards_NAME[i],Leaderboards_GPGVAL[i]);
		}
		
		for (int i = 0; i < Events_NAME.GetLength(0); i++) {
			Events.Add (Events_NAME [i], Events_GPGVAL [i]);
		}
		PlayGamesPlatform.Activate ();

		Social.localUser.Authenticate((bool success) => {
			if(success)
			{
				IsSignedIn = true;
			}else
			{
				IsSignedIn = false;
			}
		});	
	}

	//for NonIncrementAchievements
	public void UnlockAchievement(string name)
	{
		Social.ReportProgress(NonIncrementAchievements[name],100.0,(bool success) => {});

	}

	public void IncrementAchievement(string name, int num)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(IncrementAchievements[name],num,(bool success) => {
		});
	}

	public void UploadScoreToLeaderboard(int score, string leaderboardName)
	{
		Social.ReportScore(score, Leaderboards[leaderboardName], (bool success) => {
			// handle success or failure
		});
	}

	public void IncrementEvent(string name)
	{
		PlayGamesPlatform.Instance.Events.IncrementEvent (Events [name], 1);
	}

	public void ShowLeaderboard(string name)
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI (Leaderboards [name]);
	}

	public void ShowAchievements()
	{
		Social.ShowAchievementsUI ();
	}

}
