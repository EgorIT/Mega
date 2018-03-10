using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesEditor : MonoBehaviour {

    [MenuItem("ScenesEditor/DoCaps")]
    static void DoCaps () {
        EditorSceneManager.OpenScene("Assets/Mega/Scenes/Caps.unity");
        GameObject caps = GameObject.FindWithTag("Caps");
        GameObject capsSalt = GameObject.FindWithTag("CapsSalt");

        ShopPreviews previews = caps.GetComponentInChildren<ShopPreviews>();
        List<ShopCap> saltList = capsSalt.GetComponentsInChildren<ShopCap>().ToList();

        Debug.Log(caps.transform.childCount);
        Debug.Log(saltList.Count);

        int i = 0;
        while(i < 134) {
            /*Debug.Log(caps.transform.GetChild(i).GetComponent<ShopCap>().name +" "+ caps.transform.GetChild(i).name);
			Debug.Log(capsSalt.transform.GetChild(i).GetComponent<ShopCap>().name +" "+ capsSalt.transform.GetChild(i).name);
			Debug.Log("");*/
            //Debug.Log(i);
            bool ex = false;
            foreach(Sprite sprite in previews.sprites) {
                if(capsSalt.transform.GetChild(i).GetComponent<ShopCap>() != null)
                    if(sprite.name == capsSalt.transform.GetChild(i).GetComponent<ShopCap>().name) {
                        ex = true;
                    }
            }
            if(!ex) {
                if(capsSalt.transform.GetChild(i).GetComponent<ShopCap>() != null) {
                    Debug.Log(capsSalt.transform.GetChild(i).GetComponent<ShopCap>().name);
                    Debug.Log(capsSalt.transform.GetChild(i).name);
                }


            }

            i++;
        }
        /*capsList.ForEach(x =>
		{
			capsList.ForEach(y =>
			{
				if (x.gameObject.name == y.gameObject.name && x.gameObject.GetInstanceID()!=y.gameObject.GetInstanceID())
				{
					Debug.Log(x.gameObject.name);
				}
			});
			
		});*/
        /*
		capsList.ForEach(x =>
		{
			saltList.ForEach(y =>
			{
				if (x.gameObject.name==y.gameObject.name)
			});
			
		});*/
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
    }

    [MenuItem("ScenesEditor/DoRoof")]
    static void DoRoof () {
        List<string> materialPaths = new List<string>();
        EditorSceneManager.OpenScene("Assets/Scenes/New.unity");
        GameObject go = GameObject.FindWithTag("Roof");
        List<Material> mt = new List<Material>();
        int length = go.GetComponentsInChildren<MeshRenderer>().Length;

        MeshRenderer[] mr = new MeshRenderer[length];
        mr = go.GetComponentsInChildren<MeshRenderer>();
        Debug.Log(mr[1] + " " + mr.Length);

        foreach(MeshRenderer meshRenderer in mr) {
            foreach(Material meshRendererSharedMaterial in meshRenderer.sharedMaterials) {
                if(!mt.Contains(meshRendererSharedMaterial)) {
                    mt.Add(meshRendererSharedMaterial);
                }
            }
        }

        List<Material> mt2 = new List<Material>();
        Debug.Log(mt.Count);
        foreach(Material material in mt) {
            Debug.Log(material);
            Debug.Log(AssetDatabase.GetAssetPath(material));
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(material), "Assets/Materials/Roof/" + material.name + ".mat");
            //AssetDatabase.("Assets/Materials/Roof/" + material.name + ".mat");
            materialPaths.Add("Assets/Materials/Roof/" + material.name + ".mat");
            AssetDatabase.ImportAsset("Assets/Materials/Roof/" + material.name + ".mat");
        }

        foreach(MeshRenderer meshRenderer in mr) {
            Material[] matArray = new Material[meshRenderer.sharedMaterials.Length];
            for(int i = 0; i < meshRenderer.sharedMaterials.Length; i++) {
                Material a = mt.First(x => x.name == meshRenderer.sharedMaterials[i].name);
                Debug.Log(AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Roof/" + a.name + ".mat"));
                //meshRenderer.sharedMaterials[i] = AssetDatabase.
                Material matmat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Roof/" + a.name + ".mat");
                Debug.Log("1" + matmat.name + " " + AssetDatabase.GetAssetPath(matmat));
                //meshRenderer.sharedMaterials[i] = matmat;//AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Roof/" + a.name + ".mat");
                matArray[i] = matmat;
                Debug.Log(meshRenderer.sharedMaterials[i].name);
                Debug.Log(AssetDatabase.GetAssetPath(meshRenderer.sharedMaterials[i]));
            }
            meshRenderer.sharedMaterials = matArray;
        }

        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

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

    [MenuItem("ScenesEditor/CheckMaterials")]
    static void CheckMaterials () {
        EditorSceneManager.OpenScene("Assets/Scenes/New 2.unity");
        GameObject go = GameObject.FindWithTag("Roof");
        //List <Material> mt = new List<Material>();
        int length = go.GetComponentsInChildren<MeshRenderer>().Length;

        MeshRenderer[] mr = new MeshRenderer[length];
        mr = go.GetComponentsInChildren<MeshRenderer>();
        Debug.Log(mr[0] + " " + mr.Length);

        foreach(MeshRenderer meshRenderer in mr) {
            foreach(Material meshRendererSharedMaterial in meshRenderer.sharedMaterials) {
                Debug.Log(meshRendererSharedMaterial.name);
                Debug.Log(AssetDatabase.GetAssetPath(meshRendererSharedMaterial));
            }
        }
    }
}
