using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static bool paused = false;
	public static bool Paused {
		get { return paused; }
		set { setPause (value); }
	}

	private static GameManager manager;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		
		if(manager != null)
			Destroy(gameObject);
		else
			manager = this;

		this.enabled = false;
	}

	private static GameManager getManager(){
		// create one if it doesn't exist
		if(manager == null){
			GameObject obj = new GameObject("Game Manager");
			return obj.AddComponent<GameManager>();
		}
		
		return manager;
	}

	private static void setPause(bool value){
		paused = value;
	}
}
