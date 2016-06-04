using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	public GUIStyle style = null;
	public string txt;
	public bool activated;
	
	void Start () {
		activated = false;
		InvokeRepeating("Blink", 0, 1.0f);
		Invoke("Activate", 2);
	}
	
	void Update () {
		if(activated)
		{
			if(Input.GetKey(KeyCode.Escape))
				Application.LoadLevel("Main Menu");
			
			else if(Input.anyKeyDown)
				Application.LoadLevel("Scene");
		}
		
		GameObject.Find("Number1").GetComponent<Animator>().SetInteger("num", GameFlow.level/10);
		GameObject.Find("Number2").GetComponent<Animator>().SetInteger("num", GameFlow.level - (GameFlow.level/10 * 10));
	}
	
	void Activate()
	{
		activated = true;
	}
	
	void Blink()
	{
		if(GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled)
			GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = false;
		else
			GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = true;
	}	
}
