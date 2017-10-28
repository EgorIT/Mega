using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class FloorController : MonoBehaviour {
        public static FloorController inst;

        //public List<MeshRenderer> allOldMeshFloor;
        public List<MeshRenderer> allMeshFloor;

        //public List<GameObject> allParentOldGoFloor;
        //public List<GameObject> allGoNewFloor;

        public Material newFloorMaterialNotAlfa;
        public Material oldFloorMaterialNotAlfa;
        public Material newFloorMaterial;
        public Material oldFloorMaterial;

        //public GameObject parentFloor;
        public bool newFloor;
        public bool oldFloor;

        public bool currentFloorIsNew;


        public void Awake () {
            inst = this;
        }

        //public void SetNew () {
        //    for(int i = 0; i < allOldMeshFloor.Count; i++) {
        //        allOldMeshFloor[i].material = newFloorMaterial;
        //    }
        //}
        //
        //public void SetOld () {
        //    for(int i = 0; i < allOldMeshFloor.Count; i++) {
        //        allOldMeshFloor[i].material = oldFloorMaterial;
        //    }
        //}

        public void Start () {
            //SetupToScale();
            SetupToAlfa();
        }

        public void SetupToAlfa () {
            //parentFloor = new GameObject();
            //parentFloor.name = "parentFloor";
            //parentFloor.transform.position = Vector3.zero + new Vector3(0, 0.01f, 0);
            //parentFloor.transform.localEulerAngles = Vector3.zero;
            //parentFloor.transform.localScale = Vector3.one;

            var allMesh = FindObjectsOfType<MeshRenderer>();
            for(int i = 0; i < allMesh.Length; i++) {
                if(allMesh[i].materials[0].name == "Floor (Instance)") {
                    allMesh[i].material = oldFloorMaterialNotAlfa;
                    allMeshFloor.Add(allMesh[i]);
                    //var newGo = Instantiate(allMesh[i].gameObject);
                    //var newMesh = newGo.GetComponent<MeshRenderer>();
                    //newMesh.material = newFloorMaterial;
                    //newGo.transform.position = allMesh[i].gameObject.transform.position;
                    //
                    //allMesh[i].material.color = Color.white;
                    //newMesh.material.color = new Color(1, 1, 1, 0);
                    //
                    //allOldMeshFloor.Add(allMesh[i]);
                    //allNewMeshFloor.Add(newMesh);
                    //
                    //newGo.transform.parent = parentFloor.transform;
                }
            }
        }

        public IEnumerator IEnumChangeAlfa (bool isNewFloor) {
            if (currentFloorIsNew == isNewFloor) {
                yield break;
            }
            currentFloorIsNew = isNewFloor;

            for(int i = 0; i < allMeshFloor.Count; i++) {
                
                if(isNewFloor) {
                    allMeshFloor[i].material = oldFloorMaterial;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 1);
                } else {
                    allMeshFloor[i].material = newFloorMaterial;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 1);
                }
            }
            float startAlfaFloor = allMeshFloor[0].material.color.a;
            float finalAlfaFloor = 0;
            float time = GlobalParams.timeFloorSwap * 0.5f;
            float currentTime = 0;
            while(currentTime < time) {
                for(int i = 0; i < allMeshFloor.Count; i++) {
                    var t = currentTime / time;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, Mathf.Lerp(startAlfaFloor, finalAlfaFloor, Mathf.Pow(t, 4f)));
                }
                currentTime += Time.deltaTime;
                yield return null;
            }
            for(int i = 0; i < allMeshFloor.Count; i++) {
                allMeshFloor[i].material.color = new Color(1, 1, 1, 0);
            }



            for(int i = 0; i < allMeshFloor.Count; i++) {
                if(isNewFloor) {
                    allMeshFloor[i].material = newFloorMaterial;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 0);
                } else {
                    allMeshFloor[i].material = oldFloorMaterial;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 0);
                }
            }
            startAlfaFloor = 0;
            finalAlfaFloor = 1;
            time = time = GlobalParams.timeFloorSwap * 0.5f;
            currentTime = 0;
            while(currentTime < time) {
                for(int i = 0; i < allMeshFloor.Count; i++) {
                    var t = currentTime / time;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, Mathf.Lerp(startAlfaFloor, finalAlfaFloor, Mathf.Pow(t, 0.25f)));
                }
                currentTime += Time.deltaTime;
                yield return null;
            }
            for(int i = 0; i < allMeshFloor.Count; i++) {
                allMeshFloor[i].material.color = new Color(1, 1, 1, 1);
            }
            for(int i = 0; i < allMeshFloor.Count; i++) {
                if(isNewFloor) {
                    allMeshFloor[i].material = newFloorMaterialNotAlfa;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 1);
                } else {
                    allMeshFloor[i].material = oldFloorMaterialNotAlfa;
                    allMeshFloor[i].material.color = new Color(1, 1, 1, 1);
                }
            }

        }


        //public IEnumerator IEnumChangeScale (float finalScale) {
        //
        //    List<float> startScales = new List<float>();
        //    for(int i = 0; i < allParentOldGoFloor.Count; i++) {
        //        startScales.Add(allParentOldGoFloor[i].transform.localScale.x);
        //    }
        //    float time = 5f;
        //    float currentTime = 0;
        //    for(int i = 0; i < allGoNewFloor.Count; i++) {
        //        if(Math.Abs(finalScale - 1) < 0.01f) {
        //        } else {
        //            allGoNewFloor[i].SetActive(true);
        //        }
        //    }
        //
        //    while(currentTime < time) {
        //        for(int i = 0; i < allParentOldGoFloor.Count; i++) {
        //            var t = currentTime / time;
        //            allParentOldGoFloor[i].transform.localScale = Vector3.Slerp(Vector3.one * startScales[i],
        //                Vector3.one * finalScale, t * t * t);
        //        }
        //        currentTime += Time.deltaTime;
        //        yield return null;
        //    }
        //    for(int i = 0; i < allParentOldGoFloor.Count; i++) {
        //        allParentOldGoFloor[i].transform.localScale = Vector3.one * finalScale;
        //    }
        //
        //    for(int i = 0; i < allGoNewFloor.Count; i++) {
        //        if(Math.Abs(finalScale - 1) < 0.01f) {
        //            allGoNewFloor[i].SetActive(false);
        //        } else {
        //        }
        //    }
        //
        //}

        public void SetQuartal (bool isNewFloor) {
            StartCoroutine(IEnumChangeAlfa(!isNewFloor));

            // if(isNewFloor) {
            //    StartCoroutine(IEnumChangeScale(1));
            // } else {
            //     StartCoroutine(IEnumChangeScale(0));
            // }
        }

        public void Update () {
            //if(newFloor) {
            //    newFloor = false;
            //    StartCoroutine(IEnumChangeScale(1));
            //}
            //
            //if(oldFloor) {
            //    oldFloor = false;
            //    StartCoroutine(IEnumChangeScale(0));
            //}
        }


        //public void SetupToScale () {
        //    parentFloor = new GameObject();
        //    parentFloor.name = "parentFloor";
        //    parentFloor.transform.position = Vector3.zero + new Vector3(0, 0.05f, 0);
        //    parentFloor.transform.localEulerAngles = Vector3.zero;
        //    parentFloor.transform.localScale = Vector3.one;
        //
        //    var allMesh = FindObjectsOfType<MeshRenderer>();
        //    for(int i = 0; i < allMesh.Length; i++) {
        //        if(allMesh[i].materials[0].name == "Floor (Instance)") {
        //
        //            var mesh = allMesh[i].gameObject.GetComponent<MeshFilter>();
        //            Vector3 midl = Vector3.zero;
        //            for(int j = 0; j < mesh.mesh.vertices.Length; j++) {
        //                midl += mesh.mesh.vertices[j];
        //            }
        //            midl = midl * (1f / mesh.mesh.vertices.Length);
        //
        //            var newGo = Instantiate(allMesh[i].gameObject);
        //            newGo.GetComponent<MeshRenderer>().material = newFloorMaterial;
        //            newGo.transform.position = allMesh[i].gameObject.transform.position;
        //            var go = new GameObject();
        //            go.transform.position = new Vector3(midl.x, 0, -midl.y);
        //            go.transform.localScale = Vector3.one;
        //            go.name = allMesh[i].gameObject.name;
        //            go.transform.parent = parentFloor.transform;
        //            allMesh[i].gameObject.transform.parent = go.transform;
        //            allMesh[i].material = oldFloorMaterial;
        //            allParentOldGoFloor.Add(go);
        //
        //            if(allMesh[i].gameObject.isStatic) {
        //                Debug.Log(allMesh[i].gameObject.name);
        //            }
        //
        //            allOldMeshFloor.Add(allMesh[i]);
        //            allGoNewFloor.Add(newGo);
        //            newGo.SetActive(false);
        //            //allMesh[i].transform.parent = transform;
        //        }
        //    }
        //}


    }
}
