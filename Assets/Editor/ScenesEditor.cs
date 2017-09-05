using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesEditor : MonoBehaviour {

	[MenuItem("ScenesEditor/CheckRoofMaterials")]
	static void CheckRoofMaterials()
	{
		List<string> materialPaths = new List<string>();
		EditorSceneManager.OpenScene("Assets/Scenes/New 2.unity");
		GameObject go = GameObject.FindWithTag("Roof");
		List <Material> mt = new List<Material>();
		int length = go.GetComponentsInChildren<MeshRenderer>().Length;
		
		MeshRenderer[] mr = new MeshRenderer[length];
		mr = go.GetComponentsInChildren<MeshRenderer>();
		Debug.Log(mr[1]+" "+ mr.Length);
		
		foreach (MeshRenderer meshRenderer in mr)
		{
			foreach (Material meshRendererSharedMaterial in meshRenderer.sharedMaterials)
			{
				if (!mt.Contains(meshRendererSharedMaterial))
				{
					mt.Add(meshRendererSharedMaterial);
				}
			}
		}
		
		List <Material> mt2 = new List<Material>();
		Debug.Log(mt.Count);
		foreach (Material material in mt)
		{
			Debug.Log(material);
			Debug.Log(AssetDatabase.GetAssetPath(material));
			AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(material), "Assets/Materials/Roof/"+material.name+".mat");
			materialPaths.Add("Assets/Materials/Roof/"+material.name+".mat");
		}

		foreach (MeshRenderer meshRenderer in mr)
		{
			foreach (Material meshRendererMaterial in meshRenderer.materials)
			{
				
			}
		}
		
		/*foreach (string materialPath in materialPaths)
		{
			Material openedAsset = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
			Debug.Log(materialPath+ " " + openedAsset.GetFloat("_Mode"));
			if (openedAsset.GetFloat("_Mode") == 0f)
			{
				openedAsset.SetFloat("_Mode",2f);
			}
		}*/
		
	}
}
