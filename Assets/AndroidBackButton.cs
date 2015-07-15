using UnityEngine;
using System.Collections;

public class AndroidBackButton : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("RulesIn")){
				anim.SetTrigger("RulesOutTrigger");
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("CreditsIn")){
				anim.SetTrigger("CreditsOutTrigger");
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("DifficultyIn")){
				anim.SetTrigger("DifficultyOutTrigger");
			}
		}
	}
}
