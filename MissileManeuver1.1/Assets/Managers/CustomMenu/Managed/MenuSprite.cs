using UnityEngine;
using System.Collections;

namespace CustomMenu {
	public class MenuSprite {

		protected Rect pos;
		protected Texture2D sprite;
		
		public MenuSprite(string spriteLoc, Rect setPos){
			pos = setPos;
			
			sprite = Resources.Load(spriteLoc, typeof(Texture2D)) as Texture2D;
		}
		
		public void setPosition(Rect setPos){
			pos = setPos;
		}
		
		public Rect getPosition() { return pos; }
		
		public void draw(){
			Color col = GUI.color;
			GUI.color = Color.white;
			
			GUI.DrawTexture(new Rect(pos.x * Screen.width, pos.y * Screen.height, pos.width * Screen.width, pos.height * Screen.height), sprite);
			
			GUI.color = col;
		}
	}
}