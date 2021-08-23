using UnityEngine;
using System.Collections;

namespace CustomMenu {
	public class MenuItem {

		protected Rect pos;
		protected Texture2D sprite;

		public MenuItem(string spriteLoc, Rect position){
			pos = position;
			sprite = Resources.Load(spriteLoc, typeof(Texture2D)) as Texture2D;
		}

		public void setPosition(Rect setPos){
			pos = setPos;
		}
		
		public Rect getPosition() { return pos; }

		// Update is called once per frame
		public virtual void update () {
		
		}

		public virtual void draw(){
			Color col = GUI.color;
			GUI.color = Color.white;
			
			GUI.DrawTexture(new Rect(pos.x * Screen.width, pos.y * Screen.height, pos.width * Screen.width, pos.height * Screen.height), sprite);
			
			GUI.color = col;
		}
	}
}