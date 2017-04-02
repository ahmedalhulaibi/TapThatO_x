using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	public Vector2 offScreenPos = new Vector2(0,30);
	//public bool isActive = false;
	public float speed = 0.0f;
	public CubeScript sibling;
	public Animator animator;
	public RaycastHit2D hitInfo;

	public BoxCollider2D boxCollider;
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		animator = GetComponent<Animator> ();
		animator.speed = speed /5.0f;
	}

	void FixedUpdate(){
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Spawn")) {
			speed = Mathf.Max (0, CubeSpawner.cubeSpawner.CubeSpeed);
			transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
		}
	}


	// Update is called once per frame
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Static")) {
			for (int i = 0; i <  Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position), Vector2.zero);
					processHitInfo(hitInfo);
				}
			}
			
			if (Input.GetMouseButtonDown (0)) {
				hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				processHitInfo(hitInfo);
			}
		}
		if (this.transform.position.y <= -10.0f) {
			animator.SetTrigger ("PopIt");
			sibling.animator.SetTrigger("PopIt");
			AudioManager.aMgr.PlayFailSound ();
			CubeSpawner.cubeSpawner.GameOver();
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("EmptyDestroy")) {
			Destroy(gameObject);
		}

	}
	void processHitInfo(RaycastHit2D HitInfo)
	{
		if (HitInfo) {
			if (HitInfo.collider == boxCollider) {
					
				animator.SetTrigger ("PopIt");
				sibling.animator.SetTrigger("PopIt");
				
				if (tag == "Xcube") {
					AudioManager.aMgr.PlayFailSound ();
					CubeSpawner.cubeSpawner.GameOver ();
					if(GooglePlayGamesManager.gpgManager.IsSignedIn) {
						GooglePlayGamesManager.gpgManager.UnlockAchievement("Tapped out");
					}
					} else if (tag == "Ocube") {
						AudioManager.aMgr.PlayPopSound ();
						CubeSpawner.cubeSpawner.score++;
					} else {
						if (sibling.tag == "Ocube") {
							AudioManager.aMgr.PlayFailSound ();
							CubeSpawner.cubeSpawner.GameOver ();
							if(GooglePlayGamesManager.gpgManager.IsSignedIn) {
								GooglePlayGamesManager.gpgManager.UnlockAchievement("Tapped out");
							}
							} else {
								AudioManager.aMgr.PlayPopSound ();
								CubeSpawner.cubeSpawner.score++;
							}
						}
					}
				}
		}
	}
	void LateUpdate()
	{
	}
}
