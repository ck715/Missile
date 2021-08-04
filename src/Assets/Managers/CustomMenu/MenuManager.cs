using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CustomMenu {
	public class MenuManager : MonoBehaviour {

		private static List<Menu> menus;
		private static bool pauseOnMenu = true;


		private static MenuManager manager;

		// Use this for initialization
		void Awake () {
			DontDestroyOnLoad (gameObject);
			
			if(manager != null)
				Destroy(gameObject);
			else
				manager = this;

			menus = new List<Menu>();
			pauseOnMenu = true;

			this.enabled = false;
		}

		public static void setPauseOnMenu(bool pause){
			pauseOnMenu = pause;
		}

		public static bool getPauseOnMenu() {
			return pauseOnMenu;
		}

		// adds a new menu on top of the list
		public static void addMenu(Menu menu){
			checkManager ();

			menus.Add(menu);
			manager.enabled = true;

			if(pauseOnMenu)
				GameManager.Paused = true;
		}

		// adds a new menu after clearing the list
		public static void openMenu(Menu menu){
			menus = new List<Menu>();

			addMenu(menu);
		}

		// remove last menu to go up a level
		public static void backMenu(){
			menus.RemoveAt(menus.Count-1);
		}

		// close all open menus and disable the manager
		public static void closeMenu(){
			menus = new List<Menu> ();

			if(pauseOnMenu)
				GameManager.Paused = false;

			manager.enabled = false;
		}

		// are all menus closed?
		public static bool isClosed(){
			checkManager ();

			return menus.Count == 0;
		}

		// ensure that the manager was instantiated
		private static void checkManager(){
			if(manager == null){
				GameObject obj = new GameObject("Game Manager");
				obj.AddComponent<MenuManager>();
			}
		}

		// Update is called once per frame
		void Update () {
			// disable if there is no menu
			if(menus == null)
				this.enabled = false;
			else if(menus.Count == 0){
				if(pauseOnMenu)
					GameManager.Paused = false;
				this.enabled = false;
			}
			else{
				menus[menus.Count-1].update();
			}
		}

		void OnGUI(){
			if(menus != null){
				for(int i = 0; i < menus.Count; i++)
					menus[i].draw();
			}
		}
	}
}