using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class MeshCombiner : MonoBehaviour {

        public MeshFilter finalMeshNow;
        public MeshRenderer meshRendererNow;

        public void Clear() {
            meshRendererNow = GetComponent<MeshRenderer>();
            meshRendererNow.sharedMaterials = new Material[0];
            //Destroy(finalMeshNow);
            finalMeshNow = null;
        }

        public void CombineSubMeshes () {
            meshRendererNow = GetComponent<MeshRenderer>();
            Transform oldParet = transform.parent;
            Vector3 oldPos = transform.localPosition;
            Vector3 oldEul = transform.localEulerAngles;
            transform.parent = null;
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;

            meshRendererNow.sharedMaterials = new Material[0];

            finalMeshNow = null;

            for(int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            MeshFilter[] allMeshfilters = GetComponentsInChildren<MeshFilter>();
            List<Material> materials = new List<Material>();
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

            foreach(var renderer in renderers) {
                if(renderer.transform == transform) {
                    continue;
                }
                Material[] localMats = renderer.sharedMaterials;
                foreach(var localMat in localMats) {
                    if(!materials.Contains(localMat)) {
                        materials.Add(localMat);
                    }
                }
            }

            List<Mesh> submeshes = new List<Mesh>();
            for(int i = 0; i < materials.Count; i++) {
                List<CombineInstance> combiners = new List<CombineInstance>();
                foreach(var meshFilter in allMeshfilters) {
                    MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();
                    if(meshRenderer == null) {
                        continue;
                    }
                    Material[] localMaterials = meshRenderer.sharedMaterials;
                    for(int j = 0; j < localMaterials.Length; j++) {
                        if(localMaterials[j] != materials[i]) {
                            continue;
                        }
                        CombineInstance ci = new CombineInstance();
                        ci.mesh = meshFilter.sharedMesh;
                        ci.subMeshIndex = j;
                        ci.transform = meshFilter.transform.localToWorldMatrix;
                        combiners.Add(ci);
                    }
                }
                Mesh subMesh = new Mesh();
                Debug.Log(combiners.Count);
                subMesh.CombineMeshes(combiners.ToArray(), true);
                submeshes.Add(subMesh);
                Unwrapping.GenerateSecondaryUVSet(subMesh);
            }

            List<CombineInstance> finalCombiners = new List<CombineInstance>();
            for(int i = 0; i < submeshes.Count; i++) {
                CombineInstance ci = new CombineInstance();
                ci.mesh = submeshes[i];
                ci.subMeshIndex = 0;
                ci.transform = Matrix4x4.identity;
                finalCombiners.Add(ci);
            }
            Mesh finalMesh = new Mesh();
            finalMesh.CombineMeshes(finalCombiners.ToArray(), false);
			Unwrapping.GenerateSecondaryUVSet(finalMesh);
            GetComponent<MeshFilter>().sharedMesh = finalMesh;
            GetComponent<MeshRenderer>().sharedMaterials = materials.ToArray();

            transform.parent = oldParet;
            transform.localEulerAngles = oldEul;
            transform.localPosition = oldPos;

            for(int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}