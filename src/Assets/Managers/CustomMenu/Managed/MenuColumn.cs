using UnityEngine;
using System.Collections;
using CustomController;

namespace CustomMenu {
	public class MenuColumn : MenuContainer{

		// call base constructor
		public MenuColumn (string spriteLoc, Rect position, params MenuSelectable[] items) : base(spriteLoc, position, items) {	
		}

		public override void controllerSupport(){
			if(Controller.Tap(controllerButton.Down)){
				selected++;
			}
			else if(Controller.Tap(controllerButton.Up)){
				selected--;
			}

			if(selected >= menuItems.Length)
				selected = 0;
			else if(selected < 0)
				selected = menuItems.Length-1;

			foreach(MenuSelectable selectable in menuItems){
				selectable.setSelected(false);
			}

			menuItems [selected].setSelected (true);

			if(Controller.Tap(controllerButton.A))
				menuItems[selected].runCommand();
		}

	}
}