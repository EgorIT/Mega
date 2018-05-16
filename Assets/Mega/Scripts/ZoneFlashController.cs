using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class ZoneFlashController : MonoBehaviour {
        public static ZoneFlashController inst;

        public List<MeshRenderer> listMeshRenderers = new List<MeshRenderer>();

        public List<Color> listStartColorMesh = new List<Color>();
        public List<Color> listZeroColor = new List<Color>() { new Color(1, 1, 1, 0), new Color(1, 1, 1, 0), new Color(1, 1, 1, 0), new Color(1, 1, 1, 0), };

        public MeshRenderer meshRenderer20;
        public MeshRenderer meshRenderer21;
        public MeshRenderer meshRenderer22;
        public MeshRenderer meshRenderer23;

        public Coroutine coroFlash;
        public Coroutine coroLow;

        //public MeshRenderer currentMeshRenderer;
        //public Color colorForReturn;

        public int dontFlashIndex = -1;

        

        public void Awake () {
            inst = this;
        }

        public void Start() {
            listMeshRenderers.Add(meshRenderer20);
            listMeshRenderers.Add(meshRenderer21);
            listMeshRenderers.Add(meshRenderer22);
            listMeshRenderers.Add(meshRenderer23);

            listStartColorMesh.Add(meshRenderer20.material.color);
            listStartColorMesh.Add(meshRenderer21.material.color);
            listStartColorMesh.Add(meshRenderer22.material.color);
            listStartColorMesh.Add(meshRenderer23.material.color);
        }

        public void StartFlashAll() {
            coroFlash = StartCoroutine(IEnumFlashSwap());
        }

        public void StartFlashmeshRenderer (int index) {
            dontFlashIndex = index;

            //StopFlash();

            //currentMeshRenderer = meshRendererToFlash;
            //colorForReturn = meshRendererToFlash.material.color;
            //Debug.Log("1 color for return " + colorForReturn);

            // coroFlash = StartCoroutine(IEnumFlashSwap(meshRendererToFlash, new Color(colorForReturn.r, colorForReturn.g, colorForReturn.g, 0)));
        }

        public IEnumerator IEnumFlashSwap() {
            while (true) {
                yield return coroLow = StartCoroutine(IEnumFlash(listZeroColor));
                yield return coroLow = StartCoroutine(IEnumFlash(listStartColorMesh));
            }
        }

        [ContextMenu("StopFlash")]
        public void StopFlash () {
            if(coroFlash != null) {
                StopCoroutine(coroFlash);
                coroFlash = null;
            }

            if (coroLow != null) {
                StopCoroutine(coroLow);
                coroLow = null;
            }
            dontFlashIndex = -1;
            //if(currentMeshRenderer != null) {
            //    currentMeshRenderer.material.color = colorForReturn;
            //    //Debug.Log("2 color for return " + colorForReturn);
            //    currentMeshRenderer = null;
            //}
        }

        private IEnumerator IEnumFlash (List<Color> listFinalColor) {
            var listStartColor = new List<Color>();
            listStartColor.Add(meshRenderer20.material.color);
            listStartColor.Add(meshRenderer21.material.color);
            listStartColor.Add(meshRenderer22.material.color);
            listStartColor.Add(meshRenderer23.material.color);
            //Color startColor = meshRendererToFlash.material.color;
            float currentTime = 0;
            float time = 1;
            while(currentTime < time) {
                var t = currentTime / time;
                for(int i = 0; i < listStartColor.Count; i++) {
                    if (i == dontFlashIndex) {
                        listMeshRenderers[i].material.color = listStartColorMesh[i];
                        continue;
                    }
                    var r = Mathf.Lerp(listStartColor[i].r, listFinalColor[i].r, t);
                    var g = Mathf.Lerp(listStartColor[i].g, listFinalColor[i].g, t);
                    var b = Mathf.Lerp(listStartColor[i].b, listFinalColor[i].b, t);
                    var a = Mathf.Lerp(listStartColor[i].a, listFinalColor[i].a, t);
                    listMeshRenderers[i].material.color = new Color(r, g, b, a);
                }
                currentTime += Time.deltaTime;
                yield return null;
            }
        }

        //private IEnumerator IEnumFlash (MeshRenderer meshRendererToFlash, Color finalColor) {
        //    Color startColor = meshRendererToFlash.material.color;
        //    float currentTime = 0;
        //    float time = 1;
        //    while(currentTime < time) {
        //        var t = currentTime / time;
        //        var r = Mathf.Lerp(startColor.r, finalColor.r, t);
        //        var g = Mathf.Lerp(startColor.g, finalColor.g, t);
        //        var b = Mathf.Lerp(startColor.b, finalColor.b, t);
        //        var a = Mathf.Lerp(startColor.a, finalColor.a, t);
        //        meshRendererToFlash.material.color = new Color(r, g, b, a);
        //        currentTime += Time.deltaTime;
        //        yield return null;
        //    }
        //}

    }
}