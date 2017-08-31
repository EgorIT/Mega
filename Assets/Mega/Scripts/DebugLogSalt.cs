using UnityEngine;
using UnityEngine.UI;
    public class DebugLogSalt : MonoBehaviour {
        public static DebugLogSalt inst;
        public Text debugLogText;
        public bool boolShowLog;
        public static int countLog = 0;

        public void Awake () {
            inst = this;
        }

        public static void Log (string text) {
            if(inst == null) {
                return;
            }
            inst.Add(text);
        }

        public void Add (string text) {
            if(!boolShowLog || debugLogText == null || !debugLogText.gameObject.activeInHierarchy) {
                return;
            }

            var old = debugLogText.text;
            if(old.Length > 500) {
                old = string.Empty;
            }
            debugLogText.text = countLog + " ## " + text + "\n " + old;
            countLog++;
        }
    }

