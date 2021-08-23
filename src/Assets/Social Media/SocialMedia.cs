using UnityEngine;
using System.Collections;

public class SocialMedia : MonoBehaviour {

	private const string FACEBOOK_APP_ID = "1573315642906246"; // app
	//private const string FACEBOOK_APP_ID = "157335109570966"; // test
	private const string FACEBOOK_URL = "http://facebook.com/dialog/feed";
	private const string FACEBOOK_APP_LINK = "http://facebook.com/MissileManeuver";

	private const string TWITTER_URL = "http://twitter.com/intent/tweet";
	private const string TWEET_LANG = "en";

	private const string IMAGE_URL = "amatsumarine.com/MissileManeuver/Icon.png";


	public void ShareToFacebook(){
		ShareToFacebook(FACEBOOK_APP_LINK, "I made a new run on Missile Maneuver!", "I scored " + PlayerPrefs.GetInt("Score",0) + " points!", "Test your skills on the Mobile App now.", IMAGE_URL);
	}

	private static void ShareToFacebook(string link, string name, string caption, string description, string pictureURL){
		Application.OpenURL(FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID + 
			//"&link=" + WWW.EscapeURL(link) + 
			"&name=" + WWW.EscapeURL(name) + 
			"&caption=" + WWW.EscapeURL(caption) +
			"&description=" + WWW.EscapeURL(description) + 
			"&picture=" + WWW.EscapeURL(pictureURL) + 
			"&redirect_uri=" + WWW.EscapeURL("http://www.facebook.com"));
	}


	public void ShareToTwitter(){
		ShareToTwitter("I scored " + PlayerPrefs.GetInt("Score",0) + " points in Missile Maneuver!");
	}

	private static void ShareToTwitter(string tweet){
		Application.OpenURL(TWITTER_URL + 
			"?text=" + WWW.EscapeURL(tweet) + 
			"&amp;lang=" + WWW.EscapeURL(TWEET_LANG));
	}
}
