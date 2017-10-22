using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;

public class RoadsProcessor : MonoBehaviour {
	public static RoadsProcessor inst;
    private List<Material> materialsOld = new List<Material>();
    private List<Color> colorsOld = new List<Color>();
    private List<Material> materialsNew = new List<Material>();
	private List<Color> colorsNew = new List<Color>();

	public GameObject goOld;
	public GameObject goNew;
    //public GameObject glass;
	
	public void Awake() {
		inst = this;
	}
	
	void Start () {
		GameObject goOld = MainLogic.inst.parkNow;// GameObject.FindWithTag("parkNow");
		GameObject goNew = MainLogic.inst.parkAfter;//GameObject.FindWithTag("parkAfter");
		goNew.SetActive(true);
		goOld.SetActive(true);
		List<MeshRenderer> mrlOld = new List<MeshRenderer>();
		List<MeshRenderer> mrlNew = new List<MeshRenderer>();
		mrlOld = goOld.GetComponentsInChildren<MeshRenderer>().ToList();
		mrlNew = goNew.GetComponentsInChildren<MeshRenderer>().ToList();

		
		mrlOld.ForEach(x => {
			x.materials.ToList().ForEach(y => {
				materialsOld.Add(y);
				colorsOld.Add(y.color);
			});

		});
		
		mrlNew.ForEach(x => {
			x.materials.ToList().ForEach(y => {
				materialsNew.Add(y);
				colorsNew.Add(y.color);
			});
		});
		Debug.Log(materialsOld.Count);
		Debug.Log(colorsOld.Count);
		Debug.Log(materialsNew.Count);
		Debug.Log(colorsNew.Count);
		//goOld.SetActive(true);
	}

	public void ToOldDo()
	{
		goNew.SetActive(true);
		goOld.SetActive(true);
		StartCoroutine(ToOld());
	}

	public void ToNewDo()
	{
		goNew.SetActive(true);
		goOld.SetActive(true);
		StartCoroutine(ToNew());
	}

	private IEnumerator ToNew () {
		Debug.Log("ToNew");
		float time = 0;
		float time2 = .5f;
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
				Color newColor = Color.Lerp(mColor,colorsNew[i],  temp);
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
			//materialsNew[i].SetColor("_Color", new Color(materialsNew[i].color.r,materialsNew[i].color.g,materialsNew[i].color.b,1));
		}
		goNew.SetActive(false);
		//goOld.SetActive(false);
		//AllCaps.allCaps.Refresh();
		yield return null;
	}
	
	private IEnumerator ToOld () {
		Debug.Log("ToOld");
		float time = 0;
		float time2 = .5f;
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
		goOld.SetActive(false);
		//goNew.SetActive(false);
		//AllCaps.allCaps.Refresh();
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("b"))
		{
			ToNewDo();
		}

		if (Input.GetKeyDown("n"))
		{
			ToOldDo();
		}
	}
}
