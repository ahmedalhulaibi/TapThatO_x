using UnityEngine;
using System.Collections;

public class SpawnCubes : MonoBehaviour {
	public float speed;
	public float ySpawnTriggerLocation = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate(){
	if (CubeSpawner.cubeSpawner.gameStarted) {
			switch(CubeSpawner.cubeSpawner.difficulty)
			{
			case DifficultyLevel.Easy:
				ySpawnTriggerLocation = CubeSpawner.cubeSpawner.difficultyInfo[0].y;
				break;
			case DifficultyLevel.Medium:
				ySpawnTriggerLocation = CubeSpawner.cubeSpawner.difficultyInfo[1].y;
				break;
			case DifficultyLevel.Hard:
				ySpawnTriggerLocation = CubeSpawner.cubeSpawner.difficultyInfo[2].y;
				break;
			case DifficultyLevel.O_xtreme:
				ySpawnTriggerLocation = CubeSpawner.cubeSpawner.difficultyInfo[3].y;
				break;
			}
			speed = Mathf.Max (0, CubeSpawner.cubeSpawner.CubeSpeed);
			transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
			if (transform.position.y <= ySpawnTriggerLocation) {
				CubeSpawner.cubeSpawner.Spawn ();
				this.transform.position = CubeSpawner.cubeSpawner.SpawnPoint1;
			}
		} else {
			this.transform.position = CubeSpawner.cubeSpawner.SpawnPoint1;
		}
		
	}
	// Update is called once per frame
	void Update () {

	}
}
