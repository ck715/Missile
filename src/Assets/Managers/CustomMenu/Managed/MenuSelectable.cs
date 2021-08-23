using UnityEngine;
using System.Collections;

namespace CustomMenu {
	public class MenuSelectable : MenuItem {

		protected bool mouseHover;
		protected bool selected;

		public delegate void Command();
		protected Command[] commands;

		protected Texture2D texActive, texInactive;

		public MenuSelectable(string textureBase, Rect position, bool mouseHoverable = true, params Command[] com) : base(textureBase, position){
			texActive = Resources.Load (textureBase + "_Active") as Texture2D;
			texInactive = Resources.Load (textureBase + "_Inactive") as Texture2D;

			mouseHover = mouseHoverable;
			selected = false;

			commands = com;
		}

		/*
		public override void update () {

		}*/

		public bool getSelected(){
			return selected;
		}

		public void setSelected(bool select){
			selected = select;
		}

		public void setMouseHoverable(bool hoverable){
			mouseHover = hoverable;
		}

		public bool getMouseHoverable() {
			return mouseHover;
		}

		public void setCommands(params Command[] com){
			commands = com;
		}

		// run all commands in order
		public virtual void runCommand(){
			for(int i = 0; i < commands.Length; i++)
				commands[i]();
		}

		public bool checkMouseSelect(){
			float x = Input.mousePosition.x / Screen.width;
			float y = 1- Input.mousePosition.y / Screen.height;

			if(x >= pos.x && x <= pos.xMax && y >= pos.y && y <= pos.yMax){
				if(mouseHover){
					selected = true;
					return true;
				}
				// require clicking on item
				else if(Input.GetMouseButton(0)){
					selected = true;
					return true;
				}
			}

			return false;
		}

		public override void draw(){
			if(selected)
				sprite = texActive;
			else
				sprite = texInactive;

			base.draw ();
		}
	}
}