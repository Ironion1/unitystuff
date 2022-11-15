using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	private Rigidbody rb_player; 
	private Vector3 move;
	[SerializeField] float movement_speed;

	void Start(){
		rb_player = gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
			
			Vector3 ver = transform.forward * Input.GetAxis ("Vertical");
			Vector3 hor = transform.right * Input.GetAxis ("Horizontal");

			move = new Vector3(0,rb_player.velocity.y,0) + (hor+ver).normalized * Time.deltaTime * movement_speed;

			rb_player.velocity = move;
		} else {
			rb_player.velocity = new Vector3(0,rb_player.velocity.y,0);
			rb_player.angularVelocity = Vector3.zero;
		}
	}
}