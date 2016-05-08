using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms.GameCenter;

public class SocialIntegration : MonoBehaviour {

	public static long deaths = 0;

	private const string leaderboard_deaths = "leaderboard_deaths";

	// Use this for initialization
	void Start () {
		this.Authenticate ();
	}

	private void Authenticate() {
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate (success => {
				if ( success ) {
					Debug.Log("Authenticated as " + Social.localUser.userName + ".");
				} else {
					Debug.Log("Social authentication failed.");
				}
			});
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void SubmitDeathsScore() {
		if (!Social.localUser.authenticated) {
			return;
		}

		Social.ReportScore (deaths, leaderboard_deaths, success => {
			if ( success ) {
				Debug.Log("Submitted deaths score.");

				Social.ShowLeaderboardUI();
			} else {
				Debug.Log("Could not submit deaths score.");
			}
		});
	}
}
