using UnityEngine;
using System.Collections;

public class camera_controls : MonoBehaviour {

	// Use this for initialization
	public float camera_move_speed = 1f;
	public float camera_rotate_speed = 100f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal_rotation = Input.GetAxis ("Mouse X") * camera_rotate_speed;
		float vertical_rotation = Input.GetAxis ("Mouse Y") * -camera_rotate_speed;
		float horizontal_move = Input.GetAxis("Horizontal") * camera_move_speed;
		float vertical_move = Input.GetAxis("Vertical") * camera_move_speed;

		horizontal_rotation *= Time.deltaTime;
		vertical_rotation *= Time.deltaTime;
		horizontal_move *= Time.deltaTime;
		vertical_move *= Time.deltaTime;

//		this.transform.Rotate(vertical_rotation,horizontal_rotation, 0);
		this.transform.Translate(horizontal_move, 0, vertical_move);
	}
}