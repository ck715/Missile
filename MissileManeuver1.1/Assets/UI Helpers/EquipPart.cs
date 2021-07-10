using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipPart : MonoBehaviour {

	public string partName;
	public partType type;
	public AudioSource source;

	public int cost = 500;
	public string description = "Part Description";

	public enum partType {
		Engine,
		Body
	};


	void Start(){
		setText(transform.Find("Name"), partName);
		if(hasPart() || cost == 0)
			setText(transform.Find("Cost"), "Sold!");
		else
			setText(transform.Find("Cost"), "" + cost);
		setText(transform.Find("Description"), description);

		setImage(transform.Find("Image"), partName);
		GetComponent<AudioSource>().Stop ();
	}


	public void equip(){
		if(!hasPart()){
			buyPart();

		}

		if(hasPart()){
			Debug.Log("Equipping Part: " + partName);
			GetComponent<AudioSource>().Play();
			if(type == partType.Engine)
				PlayerPrefs.SetString("Engine Equipped", partName);

			else if(type == partType.Body)
				PlayerPrefs.SetString("Body Equipped", partName);

			Missile.equipParts();




		}
		else{ // should never be called unless player could not afford part
			Debug.Log("Missing Part: " + partName);
		}
	}

	public void buyPart(){
		if(!hasPart()){
			if(PlayerPrefs.GetInt("Points", 0) >= cost){
				Debug.Log("Buy part: " + partName);
				PlayerPrefs.SetInt(partName, 1);

				PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - cost);
				setText(transform.Find("Cost"), "Sold!");
				source.Play();
			}
		}
		else
			Debug.Log("Has part: " + partName);
	}

	public bool hasPart(){
		return PlayerPrefs.GetInt(partName, 0) == 1;
	}

	private void setText(Transform obj, string text){
		obj.GetComponent<Text>().text = text;
	}

	private void setImage(Transform obj, string imageName){
		Sprite img = null;

		if(type == partType.Engine)
			img = Resources.Load("Sprites/Engine/"+imageName, typeof(Sprite)) as Sprite;
		else if(type == partType.Body)
			img = Resources.Load("Sprites/Body/"+imageName, typeof(Sprite)) as Sprite;

		if(img != null)
			obj.GetComponent<Image>().sprite = img;
		else
			Debug.Log("Null image Sprites/Body/" + imageName);
	}
}
