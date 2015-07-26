using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	private int move_speed;
	private int health;
	private int attack_speed;
	private int attack_range;
	private int cost;
	private int spawn_rate;
	private int point_in_sequence;
	public string name;
	public List<Vector3> patrol_points;
	public bool patrolling;
	public bool active;

	// Use this for initialization
	void Start () {
		patrol_points.Add (new Vector3 (10,0,Random.Range (-10,10)));
   		patrol_points.Add (new Vector3 (-10,0,Random.Range (-10,10)));
		point_in_sequence = 0;
		move_speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && active){
			spawn (new Vector3(0, Random.Range (-10, 10), 0));
		}
		if (!active) {
			patrol ();
		}
	}

	private void spawn(Vector3 spawn_point){
		var go  = Instantiate(Resources.Load (name), spawn_point, Quaternion.identity) as GameObject;
	}

	private void patrol(){
		bool reached_point = false;
		Vector3 point_to_patrol_to = patrol_points [point_in_sequence];
		Vector3 pos = gameObject.transform.position;
		if (patrolling & reached_point == false)
		{
			Debug.Log (Vector3.Distance (pos, point_to_patrol_to));
			gameObject.transform.position = Vector3.MoveTowards (transform.position, point_to_patrol_to, (move_speed * Time.deltaTime));
			if(Mathf.Abs (Vector3.Distance (pos, point_to_patrol_to)) < .5f)
			{
				reached_point = true;
				if(((point_in_sequence + 1) < patrol_points.Count) && reached_point == true)
				{
					point_in_sequence++;
					point_to_patrol_to = patrol_points[point_in_sequence];
					reached_point = false;
				}
				else{
					point_in_sequence = 0;
					point_to_patrol_to = patrol_points[point_in_sequence];
					reached_point = false;
				}
			}
		}
	}
}
