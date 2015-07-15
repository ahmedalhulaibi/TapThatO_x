using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AdMobPlugin))]
public class BannerAd : MonoBehaviour {
	public static BannerAd bannerAd;
	public bool hidden = false;
	public Animator anim;

	private const string AD_UNIT_ID = "ca-app-pub-1664900632726294/7599927169";
	private AdMobPlugin admob;
	void Awake(){
		bannerAd = this;

	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		admob = GetComponent<AdMobPlugin> ();
		admob.CreateBanner (AD_UNIT_ID, AdMobPlugin.AdSize.SMART_BANNER, false);
		admob.RequestAd ();
		admob.ShowBanner ();
	}

	public void showAd()
	{
		hidden = false;
		admob.ShowBanner ();
	}

	public void hideAd()
	{
		hidden = true;
		admob.HideBanner ();
	}
	// Update is called once per frame
	void Update () {
//		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("GameOn") || anim.GetCurrentAnimatorStateInfo (0).IsName ("GameIn")) {
//			if(!hidden)
//			{
//				hideAd ();
//			}
//		} 
//		else if (hidden && !(anim.GetCurrentAnimatorStateInfo (0).IsName ("GameOn") || anim.GetCurrentAnimatorStateInfo (0).IsName ("GameIn"))) {
//			showAd();
//		}
		if (!(anim.GetCurrentAnimatorStateInfo (0).IsName ("GameOver") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Main")) && !hidden) {
			hideAd();
		} 
		else if (hidden && (anim.GetCurrentAnimatorStateInfo (0).IsName ("GameOver") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Main"))) {
			showAd();
		}
	}
}
