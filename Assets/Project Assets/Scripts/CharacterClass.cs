using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {
	public Animator playerAnimator;
	public Rigidbody2D playerRigibody;
	public float characterSpeed;
	public Vector3 movement;
	public int souls;
	public int lives;
	public bool a;
	public int b;

	public AudioClip heartBeat;
	public AudioClip bringThem;
	public AudioClip laugh;
	public AudioClip capture;
	public AudioClip quitSouls;
	
	void Start () {
		characterSpeed = 10.0f;
		playerRigibody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
		
		b = 0;
		a = false;
		souls = 0;
		lives = 3;
	}

	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		
		if(transform.position.x <= -7.5f && horizontal <= -1)
			horizontal = 0;
		if(transform.position.x >= 7.6f && horizontal >= 1)
			horizontal = 0;
		if(transform.position.y <= -2.3f && vertical <= -1)
			vertical = 0;
		if(transform.position.y >= 5 && vertical >= 1)
			vertical = 0;

		
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
		
		Animating(horizontal, vertical);
		
		Move(horizontal, vertical);
		
		if(Input.GetKey(KeyCode.E))
		{
			QuitSouls();
		}
	}
	
	void Move(float h, float v)
	{
		movement.Set(h, v, 0.0f);
		movement = movement.normalized * characterSpeed * Time.deltaTime;
		
		playerRigibody.MovePosition(transform.position + movement);
	}
	
	public void Hurt()
	{
		GetComponent<AudioSource>().clip = heartBeat;
		GetComponent<AudioSource>().Play();
		
		b = 0;
		a = false;
		InvokeRepeating("Blink", 0, 0.15f);
		
		lives -= 1;
		
		switch(lives)
		{
			case 2:
				GameObject.FindGameObjectWithTag("Heart3").GetComponent<Animator>().SetBool("LoseHeart", true);
				break;
				
			case 1:
				GameObject.FindGameObjectWithTag("Heart2").GetComponent<Animator>().SetBool("LoseHeart", true);
				break;

			case 0:
				GameObject.FindGameObjectWithTag("Heart1").GetComponent<Animator>().SetBool("LoseHeart", true);
				break;						
		}
		
		if(lives <= 0)
			Dead();
	}
	
	void Blink()
	{
		if(b < 5)
		{
			if(a)
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
				a = false;
			}
			else
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
				a = true;
			}
			
			b++;
		}
		
		else
			CancelInvoke("Blink");
	}
	
	void Animating(float h, float v)
	{
		playerAnimator.SetFloat("Horizontal", h);
		playerAnimator.SetFloat("Vertical", v);
		
		if(h < 0)
			transform.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
		else if(h > 0)
			transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
		
		for(int i = 0; i < souls; i++)
		{
			string tag = "Soul" + i.ToString();							
			GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Vertical", v);
			GameObject.FindGameObjectWithTag(tag).transform.GetChild(0).GetComponent<Animator>().SetFloat("Horizontal", h);
		}
	}	
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Soul")
		{
			if(Input.GetKey(KeyCode.Space) && souls <= 9)
			{
				other.GetComponent<SoulClass>().SetTarget();
				other.transform.GetChild(0).GetComponent<Animator>().SetBool("Capture", true);
				GetComponent<AudioSource>().volume = 0.1f;
				GetComponent<AudioSource>().clip = capture;
				GetComponent<AudioSource>().Play();
				GetComponent<AudioSource>().volume = 1.0f;
			}
		}
		
		if(other.tag == "Ritual")
		{
			if(souls == other.GetComponent<RitualClass>().total)
			{
				int type1 = other.GetComponent<RitualClass>().condition[0];
				int type2 = other.GetComponent<RitualClass>().condition[1];
				int type3 = other.GetComponent<RitualClass>().condition[2];
				
				for(int i = 0; i < souls; i++)
				{
					string tag = "Soul" + i.ToString();
					
					switch(GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().type)
					{
						case 1:
							type1 -= 1;
							break;
							
						case 2:
							type2 -= 1;
							break;
							
						case 3:
							type3 -= 1;
							break;
					}
				}
					
				if(type1 == 0 && type2 == 0 && type3 == 0)
					other.GetComponent<RitualClass>().SuccessfullRitual();
			}
		}		
	}

	void QuitSouls()
	{	
		for(int i = 0; i < souls; i++)
		{
			string tag = "Soul" + i.ToString();
			
			GameObject.FindGameObjectWithTag(tag).GetComponent<SoulClass>().StopFollowing(Random.Range(1, 5), 0.1f);
			GameObject.FindGameObjectWithTag(tag).tag = "Untagged";

			if(i == (souls - 1))
			{
				GetComponent<AudioSource>().clip = quitSouls;
				GetComponent<AudioSource>().Play();
			}
		}

		souls = 0;
	}
	
	void Dead()
	{
		Application.LoadLevel("GameOver");
	}
}
