using UnityEngine;
using System.Collections;

namespace CustomMenu{
	public class MenuButton : MenuSelectable {

		protected string text;
		protected GUIStyle fontStyle;

		public MenuButton(string textureBase, Rect position, string text, bool mouseHoverable = true, params Command[] com) : base(textureBase, position, mouseHoverable, com){
			this.text = text;

			fontStyle = new GUIStyle ();
			fontStyle.fontSize = 20;
			fontStyle.fontStyle = FontStyle.Bold;
			fontStyle.alignment = TextAnchor.MiddleCenter;
			fontStyle.normal.textColor = Color.black;
			fontStyle.hover.textColor = Color.black;
		}

		public override void draw(){
			base.draw ();

			Rect drawRec = new Rect(pos.x * Screen.width, pos.y * Screen.height, pos.width * Screen.width, pos.height * Screen.height);
			GUI.Label (drawRec, text, fontStyle);
		}
	}
}