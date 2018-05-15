using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts.Interface {
    public class ParkingArrowsController : MonoBehaviour {
        public static ParkingArrowsController inst;

        public Transform stockArrowTransform;
        public Coroutine coroJump;

        public List<Transform> listArrowTransforms;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            HideArrow();
        }

        public void HideArrow () {
            StopCoroJump();
            SetActivArrow(false);
        }

        [ContextMenu("ShowArrow")]
        public void ShowArrow () {
            StopCoroJump();
            coroJump = StartCoroutine(IEnumGoJump());
        }

        private void StopCoroJump () {
            if(coroJump != null) {
                StopCoroutine(coroJump);
            }
            coroJump = null;
            StopAllCoroutines();
        }

        public void SetActivArrow (bool var) {
            stockArrowTransform.gameObject.SetActive(var);
        }

        private IEnumerator IEnumGoJump () {
            SetActivArrow(true);
            //StartCoroutine(IEnumRotateY(stockArrowTransform, 55));
            while(true) {
                yield return StartCoroutine(IEnumMove(stockArrowTransform, Vector3.up * 5, 1));
                yield return StartCoroutine(IEnumMove(stockArrowTransform, Vector3.zero, 1));
            }
        }

        //public IEnumerator IEnumRotateY (Transform tran, float speed) {
        //    while(true) {
        //        tran.Rotate(Vector3.up * speed * Time.deltaTime);
        //        yield return null;
        //    }
        //}

        public IEnumerator IEnumMove (Transform tran, Vector3 finalPos, float time) {
            Vector3 startPos = tran.localPosition;
            float currentTime = 0;

            while(currentTime < time) {
                var t = currentTime / time;
                tran.localPosition = Vector3.Lerp(startPos, finalPos, t * t);

                currentTime += Time.deltaTime;
                yield return null;
            }


        }
    }
}