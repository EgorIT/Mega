using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadsProcessor : MonoBehaviour {
	public static RoofProcessor inst;
    private List<Material> materialsOld = new List<Material>();
    private List<Color> colorsOld = new List<Color>();
    private List<Material> materialsNew = new List<Material>();
	private List<Color> colorsNew = new List<Color>();
    //public GameObject glass;
	
	void Start () {
		List<MeshRenderer> mrlOld = new List<MeshRenderer>();
		List<MeshRenderer> mrlNew = new List<MeshRenderer>();
		GameObject goOld = GameObject.FindWithTag("OldRoads");
		GameObject goNew = GameObject.FindWithTag("NewRoads");
		mrlOld = goOld.GetComponentsInChildren<MeshRenderer>().ToList();
		mrlNew = goNew.GetComponentsInChildren<MeshRenderer>().ToList();

		mrlOld.ForEach(x => {
			x.materials.ToList().ForEach(y => {
				materialsOld.Add(y);
				colorsOld.Add(y.color);
				//Debug.Log(y.name+" "+y.GetInstanceID());
			});

		});
		
		mrlNew.ForEach(x => {
			x.materials.ToList().ForEach(y => {
				materialsNew.Add(y);
				colorsNew.Add(y.color);
				//Debug.Log(y.name+" "+y.GetInstanceID());
			});

		});

	}
	
	private IEnumerator ToNew () {
		float time = 0;
		float time2 = .3f;
		while(time < time2) {
			float temp = Mathf.Lerp(1, 0, time / time2);

			foreach(Material material in materialsOld) {
				Color mColor = material.color;
				Color newColor = new Color(mColor.r, mColor.g, mColor.b, temp);
				material.SetColor("_Color", newColor);
				
			}

			for (int i = 0; i < materialsNew.Count; i++)
			{
				Color mColor = materialsNew[i].color;
				Color newColor = Color.Lerp(colorsNew[i], mColor, temp);
				materialsNew[i].SetColor("_Color", newColor);
			}
			
			time += Time.deltaTime;
			yield return null;
		}

		foreach(Material material in materialsOld) {
			Color mColor = material.color;
			Color newColor = new Color(mColor.r, mColor.g, mColor.b, 0);
			material.SetColor("_Color", newColor);
		}
		
		for (int i = 0; i < materialsNew.Count; i++)
		{
			materialsNew[i].SetColor("_Color", colorsNew[i]);
		}
		
		AllCaps.allCaps.Refresh();
		yield return null;
	}
	
	private IEnumerator ToOld () {
		float time = 0;
		float time2 = .3f;
		while(time < time2) {
			float temp = Mathf.Lerp(1, 0, time / time2);

			foreach(Material material in materialsNew) {
				Color mColor = material.color;
				Color newColor = new Color(mColor.r, mColor.g, mColor.b, temp);
				material.SetColor("_Color", newColor);
				
			}

			for (int i = 0; i < materialsOld.Count; i++)
			{
				Color mColor = materialsOld[i].color;
				Color newColor = Color.Lerp(colorsOld[i], mColor, temp);
				materialsOld[i].SetColor("_Color", newColor);
			}
			
			time += Time.deltaTime;
			yield return null;
		}

		foreach(Material material in materialsNew) {
			Color mColor = material.color;
			Color newColor = new Color(mColor.r, mColor.g, mColor.b, 0);
			material.SetColor("_Color", newColor);
		}
		
		for (int i = 0; i < materialsOld.Count; i++)
		{
			materialsOld[i].SetColor("_Color", colorsOld[i]);
		}
		
		AllCaps.allCaps.Refresh();
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
