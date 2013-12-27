using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 2f;
	
	void Update()
	{
		if (networkView.isMine)
			InputMovement();
	}
	
	void InputMovement()
	{
		transform.Translate(0f, 
		                    Input.GetAxis ("Vertical") * Time.deltaTime * speed,
		                    0f);
	}
}
