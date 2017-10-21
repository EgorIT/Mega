using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderProcessor : MonoBehaviour
{
	public bool right=false;
	public RectTransform content;
	public CupController cupCollider;
	
	public BoxCollider bx2d;
	
	
	// Use this for initialization
	void Start ()
	{
		//bx2d = gameObject.GetComponent<BoxCollider2D>();
	}

	/*private void OnTriggerEnter(Collider other)
	{
		if (other.name == bx2d.name)
		{
			if (right)
			{
				content.anchoredPosition += new Vector2(-30,0);
			}
			else
			{
				content.anchoredPosition += new Vector2(30,0);
			}

		}
	}
*/

	private void OnTriggerStay(Collider other)
	{
		if (cupCollider.bufferedCollider)
		{
			if (other.name == cupCollider.bufferedCollider.name)
			{
				Debug.Log(other.name);
				if (right)
				{
					content.anchoredPosition += new Vector2(-5, 0);
				}
				else
				{
					content.anchoredPosition += new Vector2(5, 0);
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		cupCollider.bufferedCollider = null;
	}

	/*private void OnTriggerStay(Collider other)
	{
		throw new System.NotImplementedException();
	}*/
	/*public IEnumerator Kick()
	{
	}*/

	// Update is called once per frame
	void Update () {
		
	}
}
