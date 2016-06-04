using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour {
	public Transform ritual;
	public Transform player;
	public Transform soul;
	public float nextActionTime;
	public bool firstTime;
	static public int level;
	public int maxSouls;
	public int minSouls;
	public float ritualDuration;
	public bool isPaused;
	public float p;
	
	public GUIStyle style = null;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		level = 1;
		
		nextActionTime = 0.0f;
		firstTime = true;
		
		GetComponent<AudioSource>().Play();
		Time.timeScale = 0.0f;
		isPaused = true;
	}
	
	void Update () {
		GameObject.Find("Level").transform.GetChild(0).GetComponent<Animator>().SetInteger("num", level/10);
		GameObject.Find("Level").transform.GetChild(1).GetComponent<Animator>().SetInteger("num", level - (level/10 * 10));
		
		if(isPaused)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Time.timeScale = 1.0f;
				GameObject.Find("bocadillointro").GetComponent<SpriteRenderer>().enabled = false;
				isPaused = false;
			}
		}
		
		else if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("Main Menu");
		
		if(Time.time > nextActionTime && firstTime)
		{
			GenerateRitual();
			firstTime = false;
		}
		
		switch(level)
		{
			case 1:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 3.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.01f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.03f;
				maxSouls = 1;
				ritualDuration = 30.0f;
				break;
				
			case 2:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 3.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.02f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.05f;
				maxSouls = 3;
				ritualDuration = 20.0f;
				break;

			case 3:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 2.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.025f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.05f;
				maxSouls = 4;
				ritualDuration = 25.0f;
				break;	

			case 4:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 2.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.025f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.05f;
				maxSouls = 5;
				ritualDuration = 25.0f;
				break;

			case 5:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 2.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.03f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.05f;
				maxSouls = 6;
				ritualDuration = 25.0f;
				break;	

			case 6:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 2f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.04f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.055f;
				maxSouls = 6;
				ritualDuration = 25.0f;
				break;	

			case 7:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 2f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.045f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.055f;
				maxSouls = 6;
				ritualDuration = 25.0f;
				break;

			case 8:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.045f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.06f;
				maxSouls = 7;
				ritualDuration = 25.0f;
				break;

			case 9:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.05f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.065f;
				maxSouls = 7;
				ritualDuration = 25.0f;
				break;

			case 10:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.05f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.07f;
				maxSouls = 7;
				ritualDuration = 25.0f;
				break;	

			case 11:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.055f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.07f;
				maxSouls = 8;
				ritualDuration = 25.0f;
				break;	

			case 12:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.055f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.07f;
				maxSouls = 8;
				ritualDuration = 25.0f;
				break;	

			case 13:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.055f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.075f;
				maxSouls = 8;
				ritualDuration = 25.0f;
				break;		

			case 14:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.5f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.06f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.075f;
				maxSouls = 9;
				ritualDuration = 25.0f;
				break;

			case 15:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.06f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.08f;
				maxSouls = 9;
				ritualDuration = 20.0f;
				break;	

			case 16:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.06f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.08f;
				maxSouls = 9;
				ritualDuration = 20.0f;
				break;	

			case 17:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.07f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.08f;
				maxSouls = 9;
				ritualDuration = 20.0f;
				break;	

			case 18:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.075f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.085f;
				maxSouls = 10;
				ritualDuration = 20.0f;
				break;	

			case 19:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.08f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.09f;
				maxSouls = 10;
				ritualDuration = 20.0f;
				break;	

			default:
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minTime = 0.0f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxTime = 1f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed = 0.085f;
				GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed = 0.095f;
				maxSouls = 10;
				ritualDuration = 20.0f;
				break;
		}
		
		minSouls = (int)(maxSouls/2);
		
		if(minSouls == 0) minSouls = 1;
	}
	
	public void GenerateRitual()
	{
		Instantiate(ritual, new Vector3(10, 10, 0), Quaternion.identity);
	}
	
	void OnGUI()
	{
		/*string txt = "Level " + level.ToString();
		Vector2 size = style.CalcSize (new GUIContent (txt));*/
		//GUI.Label(new Rect(842, 30, 60, 60), "Level " + level, style);
		//GUI.Label(new Rect(Screen.width - size.x - 40, 30, 60, 60), "Level " + level, style);
	}
}
