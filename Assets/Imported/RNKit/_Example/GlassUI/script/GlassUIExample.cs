using UnityEngine;
using System.Collections;

public class GlassUIExample : MonoBehaviour
{
    public int box_count = 25;
    public float box_size = 5f;
    public Vector3 offset = new Vector3(0f, 5f);
    void Start()
    {
        var c = transform.parent.Find("testScene").GetChild(0);

        for(var i = 0; i < box_count; ++i)
        {
            var ci = GameObject.Instantiate(c);
            ci.transform.parent = c.parent;

            ci.localPosition = Random.insideUnitSphere * box_size + offset;
            ci.localRotation = Random.rotationUniform;
        }
    }

    void Update()
    {
        var euler = transform.localEulerAngles;
        euler.y += 10f * Time.deltaTime;
        transform.localEulerAngles = euler;
    }
}