using UnityEngine;
using System.Collections;

public class RitualActivated : MonoBehaviour {

	void Start () {
		GameObject.Find("pergamino2").GetComponent<SpriteRenderer>().color = new Color(1f, 0.094f, 0.249f, 1f);
		Invoke("Exit", 1);
	}
	
	void Update () {
	
	}
	
	void Exit()
	{
		GameObject.Find("pergamino2").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		Destroy(this.gameObject);
	}
}
