using UnityEngine;
using System.Collections;

namespace CustomMenu{
	public class MenuCheckBox : MenuSelectable {
		protected bool checkbox;

		protected Command[] checkedCommands;
		protected Command[] unCheckedCommands;

		public MenuCheckBox(string textureBase, Rect position, bool check = false, bool mouseHoverable = true) : base (textureBase, position, mouseHoverable){
			checkbox = check;

			checkedCommands = new Command[0];
			unCheckedCommands = new Command[0];
		}

		public bool isChecked(){
			return checkbox;
		}

		public void setCheckedCommands(params Command[] com){
			checkedCommands = com;
		}

		public void setUncheckedCommands(params Command[] com){
			unCheckedCommands = com;
		}

		public override void runCommand(){
			checkbox = !checkbox;

			if(checkbox){
				for(int i = 0; i < checkedCommands.Length; i++)
					checkedCommands[i]();
			}
			else{
				for(int i = 0; i < unCheckedCommands.Length; i++)
					unCheckedCommands[i]();
			}
		}

		public override void draw(){
			selected = checkbox;
			
			base.draw ();
		}
	}
}