using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPreviews : MonoBehaviour
{

	public List<Sprite> sprites;
	public Sprite def;
	public Sprite vacant;

	public Sprite FindPreview(string name)
	{
		/*if (name == "VACANT")
		{
			return vacant;
		}*/
		Sprite need = sprites.FirstOrDefault(x => x.name == name);
		if (need == null)
		{
			need = def;
		}

		return need;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
