using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class ViewBlock : MonoBehaviour {
        public Transform finalCameraTransform;
        public PolygonCollider2D zoneCollider;
        //public List<Vector3> listVec3ColliderPos;
        public GameObject pointMidlle;

        public Material tempMaterial;
        //public Transform boxCollider;
        public bool TEST;
        public MeshCollider meshCollider;

        public void Start() {
           // ResetStartParams();
           // ShowPerimeter();//tempMega
        }

        public void ResetStartParams() {
            float x = 0f;
            float y = 0f;
            if (zoneCollider) {
                Vector2 posPointMidlle = Vector2.zero;
                for (int i = 0; i < zoneCollider.points.Length; i++) {
                    posPointMidlle += new Vector2(zoneCollider.points[i].x, zoneCollider.points[i].y);
                }
                x = posPointMidlle.x / zoneCollider.points.Length;
                y = posPointMidlle.y / zoneCollider.points.Length;
            }
            pointMidlle.transform.localPosition = new Vector3(x, GlobalParams.deltaYpointMidlleInViewBlock, y);
            pointMidlle.SetActive(false);

            finalCameraTransform.localPosition = Vector3.zero;
            //finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera;

            //finalCameraTransform.transform.Translate(- Vector3.forward * GlobalParams.distancePointForCameraMidlleFragment);
            /*if (boxCollider) {
                boxCollider.transform.localPosition = new Vector3(x, GlobalParams.deltaYpointMidlleInViewBlock, 0);
            }*/
            

        }

        public void SetClick(Vector3 v3) {
            //Debug.Log("Select" + gameObject.name);
            finalCameraTransform.localPosition = Vector3.zero;

            var finalVector3 = pointMidlle.transform.position - v3;
           // finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera;
            //Debug.Log(finalVector3);
            //if (finalVector3.x < 0 && finalVector3.z < 0) {
            //    finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0,180,0);
            //}
            //if(finalVector3.x > 0 && finalVector3.z < 0) {
            //    finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 90, 0);
            //}
            //if(finalVector3.x > 0 && finalVector3.z > 0) {
            //    finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 0, 0);
            //}
            //if(finalVector3.x < 0 && finalVector3.z > 0) {
            //    finalCameraTransform.localEulerAngles = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 270, 0);
            //}

           // MegaCameraController.inst.SetNewPosCamera(v3, 
           //     finalCameraTransform.eulerAngles, GlobalParams.distancePointForCameraMidlleFragment, GlobalParams.sizeOrtocameraMidlleFragment);
            //MainLogic.inst.GoMiddleView();
        }

        public void ShowPerimeter() {
            if (false) {
                meshCollider = GetComponentsInChildren<MeshCollider>()[0];
                for (int i = 0; i < meshCollider.sharedMesh.vertices.Length; i++) {
                    Debug.Log(meshCollider.sharedMesh.vertices[i].x + " " + meshCollider.sharedMesh.vertices[i].y);
                }

                /*if (!zoneCollider) {
                    return;
                }*/
                float r = 1f;
                var line = new GameObject();
                line.transform.parent = meshCollider.gameObject.transform.parent;
                line.transform.localPosition = meshCollider.gameObject.transform.localPosition;
                line.transform.localScale = meshCollider.gameObject.transform.localScale;
                line.transform.localEulerAngles = new Vector3(-180,0,0);// meshCollider.gameObject.transform.localEulerAngles;
                line.name = "line";
                line.layer = 9; //tempGameObject
                var lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.material = tempMaterial;
                var col = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
                lineRenderer.startColor = col;
                lineRenderer.endColor = col;
                lineRenderer.useWorldSpace = false;
                List<Vector3> listVector3 = new List<Vector3>();
                for (int i = 0; i < meshCollider.sharedMesh.vertices.Length; i++) {
                    listVector3.Add(new Vector3(meshCollider.sharedMesh.vertices[i].x * r,
                        GlobalParams.deltaPerimetrInViewBlock, meshCollider.sharedMesh.vertices[i].y * r));
                }
                listVector3.Add(new Vector3(meshCollider.sharedMesh.vertices[0].x * r,
                    GlobalParams.deltaPerimetrInViewBlock, meshCollider.sharedMesh.vertices[0].y * r));

                /* for(int i = 0; i < zoneCollider.points.Length; i++) {
                     listVector3.Add(new Vector3(zoneCollider.points[i].x, GlobalParams.deltaPerimetrInViewBlock, zoneCollider.points[i].y));
                 }
                 lis*tVector3.Add(new Vector3(zoneCollider.points[0].x, GlobalParams.deltaPerimetrInViewBlock, zoneCollider.points[0].y));
                 */

                lineRenderer.endWidth = 0.1f;
                lineRenderer.startWidth = 0.1f;
                lineRenderer.SetVertexCount(listVector3.Count);
                lineRenderer.SetPositions(listVector3.ToArray());
            }
        }

    }
}