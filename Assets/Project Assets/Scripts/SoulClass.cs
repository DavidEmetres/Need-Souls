using UnityEngine;
using System.Collections;

public class SoulClass : MonoBehaviour {

	public Transform target;
	public CharacterClass targetScript;
	public float speed;
	public int position;
	public int side;
	public float minSpeed;
	public float maxSpeed;
	public bool isFollowing;
	public int type;

	public AudioClip hit;
	
	void Start () {
		isFollowing = false;
		minSpeed = GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().minSpeed;
		maxSpeed = GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().maxSpeed;
		speed = Random.Range(minSpeed, maxSpeed);

		type = GameObject.Find("SoulGenerator").GetComponent<SoulsGenerator>().type;
	
		/*type = Random.Range(1, 4);
		
		switch(type)
		{
			case 1:
				GetComponent<SpriteRenderer>().color = Color.red;
				break;
			
			case 2:
				GetComponent<SpriteRenderer>().color = Color.blue;
				break;			
			
			case 3:
				GetComponent<SpriteRenderer>().color = Color.green;
				break;			
			
			/*case 4:
				GetComponent<SpriteRenderer>().color = Color.yellow;
				break;			
		}*/
		
		targetScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterClass>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		
		if(transform.position.y >= 6.3f)
			side = 1;
		
		else if(transform.position.x >= 9.3f)
			side = 2;
		
		else if(transform.position.y <= -4.3f)
			side = 3;
		
		else if(transform.position.x <= -9.3f)
			side = 4;
	}

	void Update(){
		//GetComponent<SpriteRenderer>().enabled = false;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
		transform.GetChild(0).transform.rotation = Quaternion.identity;
		
		/*if(transform.position.y >= GameObject.FindGameObjectWithTag("Player").transform.position.y)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1 - position;
		}
		
		else
			transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3 + position;*/
		
		if(isFollowing)
		{
			speed = targetScript.characterSpeed;
			
			transform.LookAt(target.position);
			transform.Rotate(new Vector3(0,-90,0),Space.Self);

			if (Vector3.Distance(transform.position, target.position) > (1.0f))
			{
				transform.Translate(new Vector3(speed* Time.deltaTime,0,0));
			}			
		}
		
		else
		{	
			switch(side)
			{
				case 1:
					transform.Translate (Vector3.down * speed, Space.World);
					break;
					
				case 2:
					transform.Translate (Vector3.left * speed, Space.World);
					break;
					
				case 3:
					transform.Translate (Vector3.up * speed, Space.World);
					break;
					
				case 4:
					transform.Translate (Vector3.right * speed, Space.World);
					break;
			}
		}
		
		if(transform.position.y >= 7.0f || transform.position.x >= 10.0f || transform.position.y <= -5.0f || transform.position.x <= -10.0f)
			Destroy(this.gameObject);
	}
	
	public void SetTarget()
	{
		position = targetScript.souls;
		targetScript.souls += 1;
		
		switch(position)
		{
			case 0:
				this.gameObject.tag = "Soul0";
				target = GameObject.FindGameObjectWithTag("Player").transform;
				break;
				
			case 1:
				this.gameObject.tag = "Soul1";
				target = GameObject.FindGameObjectWithTag("Soul0").transform;
				break;

			case 2:
				this.gameObject.tag = "Soul2";
				target = GameObject.FindGameObjectWithTag("Soul1").transform;
				break;

			case 3:
				this.gameObject.tag = "Soul3";
				target = GameObject.FindGameObjectWithTag("Soul2").transform;
				break;

			case 4:
				this.gameObject.tag = "Soul4";
				target = GameObject.FindGameObjectWithTag("Soul3").transform;
				break;		

			case 5:
				this.gameObject.tag = "Soul5";
				target = GameObject.FindGameObjectWithTag("Soul4").transform;
				break;

			case 6:
				this.gameObject.tag = "Soul6";
				target = GameObject.FindGameObjectWithTag("Soul5").transform;
				break;				
			
			case 7:
				this.gameObject.tag = "Soul7";
				target = GameObject.FindGameObjectWithTag("Soul6").transform;
				break;			
			
			case 8:
				this.gameObject.tag = "Soul8";
				target = GameObject.FindGameObjectWithTag("Soul7").transform;
				break;			
			
			case 9:
				this.gameObject.tag = "Soul9";
				target = GameObject.FindGameObjectWithTag("Soul8").transform;
				break;			
		}

		isFollowing = true;
	}
	
	public void StopFollowing(int side, float speed)
	{
		isFollowing = false;
		this.side = side;
		this.speed = speed;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(this.tag == "Soul")
		{	
			if(other.tag == "Soul9")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				other.tag = "Untagged";
				targetScript.souls = 9;
			}

			else if(other.tag == "Soul8")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				other.tag = "Untagged";			
				switch(targetScript.souls)
				{
					case 10:	
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";	
						break;						
				}
				targetScript.souls = 8;
			}

			else if(other.tag == "Soul7")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				other.tag = "Untagged";				
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";						
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						break;															
				}
				targetScript.souls = 7;
			}

			else if(other.tag == "Soul6")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				other.tag = "Untagged";			
				switch(targetScript.souls)
				{
					case 10:	
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";						
						break;						
					
					case 9:	
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						break;										
				}
				targetScript.souls = 6;
			}			
			
			else if(other.tag == "Soul5")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				other.tag = "Untagged";				
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";		
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";					
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";					
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";				
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";					
						break;					
				}
				targetScript.souls = 5;
			}
			
			else if(other.tag == "Soul4")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.tag = "Untagged";
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";						
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";							
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";	
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						break;					
					
					case 6:
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";	
						break;
				}
				targetScript.souls = 4;
			}
			
			else if(other.tag == "Soul3")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.tag = "Untagged";
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);			
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
					
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";												
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";											
						break;					
					
					case 6:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";
						break;
						
					case 5:
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";;									
						break;
				}
				targetScript.souls = 3;
			}

			else if(other.tag == "Soul2")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.tag = "Untagged";
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);			
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";											
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";										
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";										
						break;					
					
					case 6:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";				
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";				
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						break;
						
					case 5:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						break;
						
					case 4:
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						break;
				}
				targetScript.souls = 2;
			}

			else if(other.tag == "Soul1")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.tag = "Untagged";
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";											
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";											
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";				
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";										
						break;					
					
					case 6:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						break;
						
					case 5:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						break;
						
					case 4:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						break;
						
					case 3:
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						break;
				}
				targetScript.souls = 1;
			}

			else if(other.tag == "Soul0")
			{
				GetComponent<AudioSource>().clip = hit;
				GetComponent<AudioSource>().Play();				
				other.tag = "Untagged";
				other.GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);				
				switch(targetScript.souls)
				{
					case 10:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul9").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul9").tag = "Untagged";
						break;						
					
					case 9:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";				
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul8").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul8").tag = "Untagged";												
						break;						
					
					case 8:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul7").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul7").tag = "Untagged";												
						break;						
					
					case 7:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul6").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul6").tag = "Untagged";											
						break;					
					
					case 6:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";							
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul5").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul5").tag = "Untagged";						
						break;
						
					case 5:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul4").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul4").tag = "Untagged";					
						break;
						
					case 4:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";					
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						GameObject.FindGameObjectWithTag("Soul3").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);						
						GameObject.FindGameObjectWithTag("Soul3").tag = "Untagged";						
						break;
						
					case 3:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						GameObject.FindGameObjectWithTag("Soul2").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);							
						GameObject.FindGameObjectWithTag("Soul2").tag = "Untagged";								
						break;
						
					case 2:
						GameObject.FindGameObjectWithTag("Soul1").GetComponent<SoulClass>().StopFollowing(this.side, 0.1f);					
						GameObject.FindGameObjectWithTag("Soul1").tag = "Untagged";						
						break;
				}
				targetScript.souls = 0;
			}	
		}
	}	
}
