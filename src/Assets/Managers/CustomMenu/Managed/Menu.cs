using UnityEngine;
using System.Collections;

using CustomController;

namespace CustomMenu {
	public class Menu {

		protected MenuItem[] items;
		protected MenuContainer container;

		private bool closable = true;
		
		public bool Closable{
			get { return closable; }
			set { closable = value; }
		}

		// Each menu must contain a container that manages buttons or else it is just a splash screen.
		public Menu(MenuContainer cont, params MenuItem[] itemParams){
			container = cont;
			items = itemParams;
		}

		// Update is called once per frame
		public virtual void update () {
			foreach(MenuItem item in items){
				item.update();
			}

			container.update ();

			// Enable exiting menu
			if((Input.GetKeyUp(KeyCode.Escape) || Controller.Tap(controllerButton.B)) && closable){
				MenuManager.backMenu();
			}
			// Select
			else if(Input.GetKeyUp(KeyCode.Return) || Controller.Tap(controllerButton.A)){
				runCommand();
			}
		}

		public virtual MenuSelectable getSelected(){
			return container.getSelected();
		}

		public virtual void runCommand(){
			container.getSelected().runCommand();
		}

		public virtual void draw(){
			for(int i = 0; i < items.Length; i++){
				items[i].draw();
			}

			container.draw ();
		}
	}
}