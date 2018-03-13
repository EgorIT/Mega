using System.Collections;
using UnityEngine;

namespace Assets.Mega.Scripts.Interface {
    public class KidsArrowController : MonoBehaviour {
        public static KidsArrowController inst;

        public Transform kidsArrowTransform;
        public Coroutine coroJump;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            HideArrow();
        }

        public void HideArrow() {
            StopCoroJump();
            SetActivKidsArrow(false);
        }

        public void ShowArrow () {
            StopCoroJump();
            coroJump = StartCoroutine(IEnumGoJump());
        }

        public void StopCoroJump() {
            if (coroJump != null) {
                StopCoroutine(coroJump);
            }
            coroJump = null;
            StopAllCoroutines();
        }

        public void SetActivKidsArrow(bool var) {
            kidsArrowTransform.gameObject.SetActive(var);
        }

        public IEnumerator IEnumGoJump() {
            SetActivKidsArrow(true);
            StartCoroutine(IEnumRotateY(kidsArrowTransform, 55));
            while (true) {
                yield return StartCoroutine(IEnumMove(kidsArrowTransform,Vector3.up * 5, 1));
                yield return StartCoroutine(IEnumMove(kidsArrowTransform, Vector3.zero,  1));
            }
        }

        public IEnumerator IEnumRotateY (Transform tran, float speed) {
            while(true) {
                tran.Rotate(Vector3.up * speed * Time.deltaTime);
                yield return null;
            }
        }

        public IEnumerator IEnumMove(Transform tran, Vector3 finalPos, float time) {
            Vector3 startPos = tran.localPosition;
            float currentTime = 0;
            
            while (currentTime < time) {
                var t = currentTime / time;
                tran.localPosition = Vector3.Lerp(startPos, finalPos, t*t);

                currentTime += Time.deltaTime;
                yield return null;
            }


        }
    }
}