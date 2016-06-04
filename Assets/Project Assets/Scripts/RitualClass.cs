using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RitualClass : MonoBehaviour {
	public Transform ritual;
	public Transform ritualActivated;
	
	public int[] condition;
	public int max;
	public int min;
	public int total;
	public float duration;
	public float initialTime;
	public bool fail;
	public bool success;
	public bool disappear;
	public bool temporizador;
	
	public AudioClip bringThem;
	public AudioClip circleAppear;
	public AudioClip laugh;
	
	public GUIStyle style = null;

	void Start () {
		condition = new int[3];
		total = 0;
		max = GameObject.Find("Main Camera").GetComponent<GameFlow>().maxSouls;
		min = GameObject.Find("Main Camera").GetComponent<GameFlow>().minSouls;
		duration = GameObject.Find("Main Camera").GetComponent<GameFlow>().ritualDuration;
		disappear = true;
		initialTime = 100;
		success = false;
		GameObject.Find("bocadillo2").GetComponent<SpriteRenderer>().enabled = false;
		
		if(max == 1)
		{
			condition[Random.Range(0, 3)] = 1;
			total += 1;
		}
		
		else
		{
			for(int i = 0; i < 3; i++)
			{
				condition[i] = Random.Range(0, 4);
				total += condition[i];
			}
			
			while(total < min)
			{
				int c = Random.Range(0,3);
				if(condition[c] < 4)
				{
					condition[c] += 1;
					total += 1;
				}
			}
			
			while(total > max)
			{
				int c = Random.Range(0,3);
				if(condition[c] > 0)
				{
					condition[c] -= 1;
					total -= 1;
				}
			}
		}
		
		Invoke("RecolocateRitual", 3.0f);
	}

	void ChangeConditions()
	{
		condition = new int[3];
		total = 0;
		max = GameObject.Find("Main Camera").GetComponent<GameFlow>().maxSouls;
		min = GameObject.Find("Main Camera").GetComponent<GameFlow>().minSouls;
		duration = GameObject.Find("Main Camera").GetComponent<GameFlow>().ritualDuration;
		disappear = true;
		initialTime = 100;
		success = false;
		
		if(max == 1)
		{
			condition[Random.Range(0, 3)] = 1;
			total += 1;
		}
		
		else
		{
			for(int i = 0; i < 3; i++)
			{
				condition[i] = Random.Range(0, 4);
				total += condition[i];
			}
			
			while(total < min)
			{
				int c = Random.Range(0,3);
				if(condition[c] < 4)
				{
					condition[c] += 1;
					total += 1;
				}
			}
			
			while(total > max)
			{
				int c = Random.Range(0,3);
				if(condition[c] > 0)
				{
					condition[c] -= 1;
					total -= 1;
				}
			}
		}

		Invoke("RecolocateRitual", 3.0f);		
	}
	
	void Update () {
		this.transform.rotation = Quaternion.identity;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.GetChild(0).transform.position.y * 100f) * -1;
		
		GameObject.Find("bocadillo2").transform.GetChild(0).GetComponent<Animator>().SetInteger("num", condition[0]);
		GameObject.Find("bocadillo2").transform.GetChild(1).GetComponent<Animator>().SetInteger("num", condition[2]);
		GameObject.Find("bocadillo2").transform.GetChild(2).GetComponent<Animator>().SetInteger("num", condition[1]);
		
		if(success)
		{
			Instantiate(ritualActivated, new Vector3(transform.position.x, transform.position.y + 1.1f, 0f), Quaternion.identity);
			GameObject.Find("bocadillo2").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("bocadillo2").transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("bocadillo2").transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("bocadillo2").transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

			transform.position = new Vector3(10, 10, 0);
			ChangeConditions();
		}
		
		else if(Time.time > initialTime + duration)
		{
			if(!disappear)
			{			
				disappear = true;
				//GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>().lives -= 1;
				GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>().Hurt();
				//GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
				GetComponent<AudioSource>().volume = 0.2f;
				GetComponent<AudioSource>().clip = circleAppear;
				GetComponent<AudioSource>().Play();
				transform.position = new Vector3(10, 10, 0);
				Invoke("RecolocateRitual", 5.0f);
			}
		}
		
		if(((initialTime + duration) - Time.time) <= 5)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
			transform.GetChild(0).GetComponent<Animator>().SetBool("CountDown", true);
		}
		else
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			transform.GetChild(0).GetComponent<Animator>().SetBool("CountDown", false);		
		}
	}
	
	void RecolocateRitual()
	{
		float positiony = Random.Range(-2.3f, 3.5f);
		float positionx = Random.Range(-7.0f, 7.3f);
		transform.position = new Vector3(positionx, positiony, 0);
		initialTime = Time.time;		
		disappear = false;
		
		//transform.GetChild(0).GetComponent<Animator>().SetBool("CountDown", false);		
		transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		
		if(!GameObject.Find("bocadillo2").GetComponent<SpriteRenderer>().enabled)
		{
			GameObject.Find("bocadillo2").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("bocadillo2").transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("bocadillo2").transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("bocadillo2").transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
			//GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>().bringThem;
			//GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
			GameObject.Find("Cabra").GetComponent<AudioSource>().Play();
			
			GetComponent<AudioSource>().volume = 0.2f;
			GetComponent<AudioSource>().clip = circleAppear;
			GetComponent<AudioSource>().Play();
		}
		
		else
		{
			GetComponent<AudioSource>().volume = 0.2f;
			GetComponent<AudioSource>().clip = circleAppear;
			GetComponent<AudioSource>().Play();
		}
	}
	
	public void SuccessfullRitual()
	{
		success = true;
		
		switch(total)
		{
			case 10:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (36 * i) + 36);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;			
			
			case 9:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (40.25f * i) + 40.25f);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;			
			
			case 8:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (45 * i) + 45);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;			
			
			case 7:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (51.5f * i) + 51.5f);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;				
			
			case 6:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (60 * i) + 60);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);	
					
					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);				
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;
				
			case 5:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (72 * i) + 72);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;		

			case 4:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (90 * i) + 90);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);	
					
					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);				
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;	

			case 3:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (120 * i) + 120);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;	

			case 2:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					GameObject.FindGameObjectWithTag(tag).transform.RotateAround(transform.position, Vector3.forward, (180 * i) + 180);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);	
					
					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);				
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;		

			case 1:
				for(int i = 0; i < total; i++)
				{
					string tag = "Soul" + i.ToString();							
					GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
					transform.LookAt(transform.position);
					transform.Rotate(new Vector3(0,-90,0),Space.Self);

					if(GameObject.FindGameObjectWithTag(tag).transform.position.y >= transform.position.y)
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", -1.0f);
					else
						GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", 1.0f);
					
					GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.0f);
					Destroy(GameObject.FindGameObjectWithTag(tag), 1);
					GameObject.FindGameObjectWithTag(tag).tag = "Untagged";
				}
				break;				
		}
		
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>().souls = 0;
		GameFlow.level += 1;
		
		/*GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>().laugh;
		GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();*/
		GameObject.Find("Interfaz").GetComponent<AudioSource>().Play();
		
		GetComponent<AudioSource>().volume = 0.2f;
		GetComponent<AudioSource>().clip = circleAppear;
		GetComponent<AudioSource>().Play();
	}
	
	void OnGUI()
	{
		if(GameObject.Find("bocadillo2").GetComponent<SpriteRenderer>().enabled)
		{
			/*GUI.Label(new Rect(330, 30, 60, 60), "" + condition[0], style);
			GUI.Label(new Rect(410, 30, 60, 60), "" + condition[2], style);
			GUI.Label(new Rect(475, 30, 60, 60), "" + condition[1], style);*/
			/*GUI.Label(new Rect(330, 30, 60, 60), "" + condition[0], style);
			GUI.Label(new Rect(410, 30, 60, 60), "" + condition[2], style);
			GUI.Label(new Rect(475, 30, 60, 60), "" + condition[1], style);*/
		}
	}	
}
