using UnityEngine;
using System.Collections;
using CustomController;

namespace CustomMenu {
	public class MenuContainer : MenuItem {

		protected MenuSelectable[] menuItems;
		protected int selected = 0;

		// pass in an array of MenuItems 
		public MenuContainer (string spriteLoc, Rect position, params MenuSelectable[] items) : base(spriteLoc, position) {
			// save the array of items
			menuItems = items;
			menuItems [0].setSelected (true);
		}

		// Update is called once per frame
		public override void update () {
			controllerSupport ();

			for(int i = 0; i < menuItems.Length; i++){
				if(menuItems[i].checkMouseSelect()){
					selected = i;

					// if clicked on button, run command
					if(Input.GetMouseButton(0) && menuItems[i] is MenuButton)
						menuItems[i].runCommand();

				}else if(selected != i)
					menuItems[i].setSelected(false);

			}
		}

		public virtual void controllerSupport(){
			// none for base class.
		}

		public virtual MenuSelectable getSelected(){
			return menuItems[selected];
		}

		public virtual void draw(){
			base.draw ();

			foreach(MenuItem item in menuItems){
				item.draw();
			}
		}
	}
}