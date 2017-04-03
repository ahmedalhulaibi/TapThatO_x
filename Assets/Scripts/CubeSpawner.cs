using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum DifficultyLevel
{
	Easy,
	Medium,
	Hard,
	O_xtreme
};


[RequireComponent(typeof(Timer))]
public class CubeSpawner : MonoBehaviour {
	public static CubeSpawner cubeSpawner;
	public CubeScript xcube, ocube, bcube;

	public Vector3 SpawnPoint1;
	public Vector3 SpawnPoint2;

	public DifficultyLevel difficulty = DifficultyLevel.Easy;
	public float CubeSpeed = 10.0f;

	public List<CubeScript> cubes = new List<CubeScript>();

	public Vector2[] difficultyInfo;

	public Timer timer;
	public Animator anim;
	public int score = 0;
	public int HighScoreEasy = 0;
	public int HighScoreMed = 0;
	public int HighScoreHard = 0;
	public int HighScoreO_x = 0;

	public Text scorePageText;
	public Text ingameText;

	public bool gameStarted = false;
	public bool gameOver = false;

	void Awake()
	{
		cubeSpawner = this;
		timer = this.GetComponent<Timer> ();
		if (!timer) {
			gameObject.AddComponent<Timer>();
			timer = this.GetComponent<Timer> ();			
		}
		anim = this.GetComponent<Animator> ();

		if (PlayerPrefs.HasKey ("HighScoreEasy")) {
			HighScoreEasy = PlayerPrefs.GetInt ("HighScoreEasy");
		} else {
			HighScoreEasy = 0;
			PlayerPrefs.SetInt("HighScoreEasy",HighScoreEasy);
		}

		if (PlayerPrefs.HasKey ("HighScoreMed")) {
			HighScoreMed = PlayerPrefs.GetInt ("HighScoreMed");
		} else {
			HighScoreMed = 0;
			PlayerPrefs.SetInt("HighScoreMed",HighScoreMed);
		}

		if (PlayerPrefs.HasKey ("HighScoreHard")) {
			HighScoreHard = PlayerPrefs.GetInt ("HighScoreHard");
		} else {
			HighScoreHard = 0;
			PlayerPrefs.SetInt("HighScoreHard",HighScoreHard);
		}

		if (PlayerPrefs.HasKey ("HighScoreO_x")) {
			HighScoreO_x = PlayerPrefs.GetInt ("HighScoreO_x");
		} else {
			HighScoreO_x = 0;
			PlayerPrefs.SetInt("HighScoreO_x",HighScoreO_x);
		}



	}


	public void StartGame(string difficultyLevel)
	{
		anim.ResetTrigger ("GameOverTrigger");
		score = 0;
		switch (difficultyLevel) {
		case "Easy":
			difficulty = DifficultyLevel.Easy;
			CubeSpeed = difficultyInfo[0].x;
			break;
		case "Medium":
			difficulty = DifficultyLevel.Medium;
			CubeSpeed = difficultyInfo[1].x;
			break;
		case "Hard":
			difficulty = DifficultyLevel.Hard;
			CubeSpeed = difficultyInfo[2].x;
			break;
		case "O_xtreme":
			difficulty = DifficultyLevel.O_xtreme;
			CubeSpeed = difficultyInfo[3].x;
			break;
		}

		timer.StartTimer (3.0f, false, "StartSpawnTimer");
		timer.OnTimerEvent["StartSpawnTimer"] += StartSpawnTimer;

	}

	public void StartSpawnTimer()
	{
		gameOver = false;
		if (!gameStarted) {
			gameStarted = true;
			if (timer.OnTimerEvent.ContainsKey ("StartSpawnTimer")) {
				timer.OnTimerEvent ["StartSpawnTimer"] -= StartSpawnTimer;
			}
			timer.StopTimer ("StartSpawnTimer");
			Debug.Log ("Game is started!");
			switch (difficulty) {
			case DifficultyLevel.Easy:
				//InvokeRepeating("Spawn",0.0f,difficultyInfo [0].y);
//				timer.StartTimer (difficultyInfo [0].y, true, "Spawn");
				break;
			case DifficultyLevel.Medium:
				//InvokeRepeating("Spawn",0.0f,difficultyInfo [1].y);
//				timer.StartTimer (difficultyInfo [1].y, true, "Spawn");
				break;
			case DifficultyLevel.Hard:
				//InvokeRepeating("Spawn",0.0f,difficultyInfo [2].y);
//				timer.StartTimer (difficultyInfo [2].y, true, "Spawn");
				break;
			case DifficultyLevel.O_xtreme:
				//InvokeRepeating("Spawn",0.0f,difficultyInfo [3].y);
//				timer.StartTimer (difficultyInfo [3].y, true, "Spawn");
				break;
			}
//			if (timer.OnTimerEvent.ContainsKey ("Spawn")) {
//				timer.OnTimerEvent ["Spawn"] += Spawn;
//			}
		}
	}

	public void GameOver()
	{
		//CancelInvoke ("Spawn");
		gameStarted = false;
		if (!gameOver) {
			gameOver = true;
//			if (timer.OnTimerEvent.ContainsKey ("Spawn")) {
//				timer.OnTimerEvent["Spawn"]-= Spawn;
//			}
//			timer.StopTimer ("Spawn");
			anim.SetTrigger ("GameOverTrigger");
			foreach (var item in cubes) {
				if(item)
				{
					item.animator.SetTrigger("PopIt");
				}
			}
			cubes.Clear ();
			string newText = "";
			switch (difficulty) {
			case DifficultyLevel.Easy:
                    GameOverEasy(ref newText);
				break;
			case DifficultyLevel.Medium:
                    GameOverMedium(ref newText);
				break;
			case DifficultyLevel.Hard:
                    GameOverHard(ref newText);
				break;
			case DifficultyLevel.O_xtreme:
                    GameOverO_xtreme(ref newText);
				break;
			}
            AchievementUpdate();
			scorePageText.text = newText;
			PlayerPrefs.Save ();
		}
	}

    void GameOverEasy(ref string newText)
    {
        newText += "EASY\nSCORE\n";
        newText += score.ToString() + "\n";
        if (score > HighScoreEasy)
        {
            HighScoreEasy = score;
            PlayerPrefs.SetInt("HighScoreEasy", HighScoreEasy);
            newText += "NEW HIGH SCORE\n" + HighScoreEasy.ToString();
        }
        else
        {
            newText += "HIGH SCORE\n" + HighScoreEasy.ToString();
        }
        if (GooglePlayGamesManager.gpgManager.IsSignedIn)
        {
            GooglePlayGamesManager.gpgManager.UploadScoreToLeaderboard(score,
                                                                       GooglePlayGamesManager.gpgManager.Leaderboards_NAME[0]);
            if (score == 2)
            {
                GooglePlayGamesManager.gpgManager.UnlockAchievement("2 E-Z 5 U");
            }
        }
    }

    void GameOverMedium(ref string newText)
    {
        newText += "MEDIUM\nSCORE\n";
        newText += score.ToString() + "\n";
        if (score > HighScoreMed)
        {
            HighScoreMed = score;
            PlayerPrefs.SetInt("HighScoreMed", HighScoreMed);
            newText += "NEW HIGH SCORE\n" + HighScoreMed.ToString();
        }
        else
        {
            newText += "HIGH SCORE\n" + HighScoreMed.ToString();
        }
        if (GooglePlayGamesManager.gpgManager.IsSignedIn)
        {
            GooglePlayGamesManager.gpgManager.UploadScoreToLeaderboard(score,
                                                                       GooglePlayGamesManager.gpgManager.Leaderboards_NAME[1]);
        }
    }

    void GameOverHard(ref string newText)
    {
        newText += "HARD\nSCORE\n";
        newText += score.ToString() + "\n";
        if (score > HighScoreHard)
        {
            HighScoreHard = score;
            PlayerPrefs.SetInt("HighScoreHard", HighScoreHard);
            newText += "NEW HIGH SCORE\n" + HighScoreHard.ToString();
        }
        else
        {
            newText += "HIGH SCORE\n" + HighScoreHard.ToString();
        }
        if (GooglePlayGamesManager.gpgManager.IsSignedIn)
        {
            GooglePlayGamesManager.gpgManager.UploadScoreToLeaderboard(score,
                                                                       GooglePlayGamesManager.gpgManager.Leaderboards_NAME[2]);
        }
    }

    void GameOverO_xtreme(ref string newText)
    {
        newText += "O_x\nSCORE\n";
        newText += score.ToString() + "\n";
        if (score > HighScoreO_x)
        {
            HighScoreO_x = score;
            PlayerPrefs.SetInt("HighScoreO_x", HighScoreO_x);
            newText += "NEW HIGH SCORE\n" + HighScoreO_x.ToString();
        }
        else
        {
            newText += "HIGH SCORE\n" + HighScoreO_x.ToString();
        }
        if (GooglePlayGamesManager.gpgManager.IsSignedIn)
        {
            GooglePlayGamesManager.gpgManager.UploadScoreToLeaderboard(score,
                                                                       GooglePlayGamesManager.gpgManager.Leaderboards_NAME[3]);
            if (score >= 100)
            {
                GooglePlayGamesManager.gpgManager.UnlockAchievement("100 O_x");
                GooglePlayGamesManager.gpgManager.IncrementAchievement("21st Century Ox", 1);
            }
        }
    }

    void AchievementUpdate()
    {
        if (GooglePlayGamesManager.gpgManager.IsSignedIn)
        {
            GooglePlayGamesManager.gpgManager.IncrementEvent("Number of plays");
            if (score == 2)
            {
                GooglePlayGamesManager.gpgManager.IncrementAchievement("21st 2 E-Z 5 U", 1);
            }
            if (score == 12)
            {
                GooglePlayGamesManager.gpgManager.IncrementAchievement("Chicken P-Ox", 1);
            }
            if (score == 101)
            {
                GooglePlayGamesManager.gpgManager.UnlockAchievement("101 Ox-matians");
            }
            if (score >= 100)
            {
                //increment 21st century achievement
                GooglePlayGamesManager.gpgManager.IncrementAchievement("21st Century", 1);
            }
            if (score >= 300)
            {
                GooglePlayGamesManager.gpgManager.UnlockAchievement("This is O_x!");
            }
            if (score >= 1000)
            {
                GooglePlayGamesManager.gpgManager.UnlockAchievement("M'player *Tips fedora*");
            }

            GooglePlayGamesManager.gpgManager.IncrementAchievement("Olly Olly O_xen Free!", score);
            GooglePlayGamesManager.gpgManager.IncrementAchievement("Strong Like Ox", score);
            GooglePlayGamesManager.gpgManager.IncrementAchievement("Ox-y Cleopatra", score);
            GooglePlayGamesManager.gpgManager.IncrementAchievement("The Year of The Ox", score);
        }
    }

    public void Spawn()
	{
		Debug.LogWarning("Spawn() event fired!  " + Time.time.ToString());
		int randomCube = Random.Range (0, 3);
			switch (randomCube) {
			case 0:
				cubes.Add (SpawnCube ("Xcube", SpawnPoint1));
				break;
			case 1:
				cubes.Add (SpawnCube ("Bcube", SpawnPoint1));
				break;
			case 2:
				cubes.Add (SpawnCube ("Ocube", SpawnPoint1));
				break;
			}
			switch (cubes [cubes.Count - 1].tag) {
			case "Xcube":
				cubes.Add (SpawnCube ("Bcube", SpawnPoint2));
				break;
			case "Ocube":
				cubes.Add (SpawnCube ("Bcube", SpawnPoint2));
				break;
			case "Bcube":
				randomCube = Random.Range (0, 2);
				switch (randomCube) {
				case 0:
					cubes.Add (SpawnCube ("Xcube", SpawnPoint2));
					break;
				case 1:
					cubes.Add (SpawnCube ("Ocube", SpawnPoint2));
					break;
				}
				break;
			}
			cubes [cubes.Count - 1].sibling = cubes [cubes.Count - 2];
			cubes [cubes.Count - 2].sibling = cubes [cubes.Count - 1];

			cubes [cubes.Count - 1].speed = CubeSpeed;
			cubes [cubes.Count - 2].speed = CubeSpeed;
		cleanUp ();
	}

	private CubeScript SpawnCube(string type, Vector3 pos)
	{
		switch (type) {
		case "Xcube":
			return (CubeScript)GameObject.Instantiate (xcube, pos, xcube.transform.rotation);
		case "Ocube":
			return (CubeScript)GameObject.Instantiate (ocube, pos, ocube.transform.rotation);
		case "Bcube":
			return (CubeScript)GameObject.Instantiate (bcube, pos, bcube.transform.rotation);
		}

		return null;
	}

	public void showLeaderboard()
	{
		switch (difficulty) {
		case DifficultyLevel.Easy:
			GooglePlayGamesManager.gpgManager.ShowLeaderboard(GooglePlayGamesManager.gpgManager.Leaderboards_NAME[0]);
			break;
		case DifficultyLevel.Medium:
			GooglePlayGamesManager.gpgManager.ShowLeaderboard(GooglePlayGamesManager.gpgManager.Leaderboards_NAME[1]);
			break;
		case DifficultyLevel.Hard:
			GooglePlayGamesManager.gpgManager.ShowLeaderboard(GooglePlayGamesManager.gpgManager.Leaderboards_NAME[2]);
			break;
		case DifficultyLevel.O_xtreme:
			GooglePlayGamesManager.gpgManager.ShowLeaderboard(GooglePlayGamesManager.gpgManager.Leaderboards_NAME[3]);
			break;
		}
	}

	void cleanUp()
	{
		for (int i = this.cubes.Count - 1; i >= 0; i--) {
			if(cubes[i] == null)
			{
				cubes.RemoveAt(i);
			}
		}
	}

	void Update()
	{
		ingameText.text = score.ToString ();
		if (gameOver && cubes.Count > 0) {
			cubes.Clear();
		}
	}
}
