using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
		InvokeRepeating("Blink", 0, 1.0f);
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		else if(Input.anyKeyDown)
			Application.LoadLevel("Scene");
	}
	
	void Blink()
	{
		if(GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled)
			GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = false;
		else
			GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = true;
	}
}
