using UnityEngine;
using System.Collections;

public class SoulsGenerator : MonoBehaviour {

	public Transform blueSoul;
	public Transform redSoul;
	public Transform greenSoul;

	public float position;
	public int side;
	public float minTime;
	public float maxTime;
	public float period;
	public float nextActionTime;
	public float minSpeed;
	public float maxSpeed;
	public int type;
	public int anteriorType;

	// Use this for initialization
	void Start () {
		nextActionTime = 1.0f;
		minTime = 1.0f;
		maxTime = 3.0f;
		minSpeed = 0.01f;
		maxSpeed = 0.05f;
		anteriorType = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextActionTime)
		{
			period = Random.Range (minTime, maxTime);
			nextActionTime = Time.time + period;
			GenerateSoul();
		}
	}

	void GenerateSoul()
	{
		side = Random.Range (1, 5);

		switch(side)
		{
			case 1:
				position = Random.Range(-8.0f, 8.15f);
				type = Random.Range(1, 4);
				
				while(type == anteriorType)
					type = Random.Range(1, 4);
				
				switch(type)
				{
					case 1:
						Instantiate(redSoul, new Vector3(position, 6.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;
						
					case 2:
						Instantiate(blueSoul, new Vector3(position, 6.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
					
					case 3:
						Instantiate(greenSoul, new Vector3(position, 6.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
				}
				break;
				
			case 2:
				position = Random.Range(-2.7f, 4f);
				type = Random.Range(1, 4);
				
				while(type == anteriorType)
					type = Random.Range(1, 4);
				
				switch(type)
				{
					case 1:
						Instantiate(redSoul, new Vector3(9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;
						
					case 2:
						Instantiate(blueSoul, new Vector3(9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
					
					case 3:
						Instantiate(greenSoul, new Vector3(9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
				}				
				break;	

			case 3:
				position = Random.Range(-8.0f, 8.15f);
				type = Random.Range(1, 4);
				
				while(type == anteriorType)
					type = Random.Range(1, 4);				
				
				switch(type)
				{
					case 1:
						Instantiate(redSoul, new Vector3(position, -4.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;
						
					case 2:
						Instantiate(blueSoul, new Vector3(position, -4.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
					
					case 3:
						Instantiate(greenSoul, new Vector3(position, -4.4f, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
				}				
				break;	

			case 4:
				position = Random.Range(-2.7f, 4f);
				type = Random.Range(1, 4);
				
				while(type == anteriorType)
					type = Random.Range(1, 4);				
				
				switch(type)
				{
					case 1:
						Instantiate(redSoul, new Vector3(-9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;
						
					case 2:
						Instantiate(blueSoul, new Vector3(-9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
					
					case 3:
						Instantiate(greenSoul, new Vector3(-9.4f, position, 0f), Quaternion.identity);
						anteriorType = type;
						break;					
				}				
				break;				
		}
	}
}
