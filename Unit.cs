using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	private int move_speed;
	public int health;
	public int cur_health;
	public int weapon_damage;
	public int attack_speed;
	public float next_attack_time;
	public int attack_range;
	private int cost;
	private int spawn_rate;
	private int point_in_sequence;
	private Vector3 point_to_move_to;
	public string name;
	public List<Vector3> patrol_points;
	public bool patrolling;
	public bool active;
	public int Health{
		get{
			return health;
		}
		set{
			health = value;
		}
	}
	public int Cur_Health {
		get{
			return cur_health;
		}
		set{
			cur_health = value;
		}
	}

	public string unit_type;

	public int Weapon_damage { get; set; }
	public int Attack_speed { get; set; }
	public int Attack_range { get; set; }

	public GameObject target;

	public bool selected;

	public Renderer my_renderer;
	
	enum state { active, idle, attacking, dead, spawning, walking, running, charging, engaging }

	private state character_state;

	// Use this for initialization
	void Start () {
		my_renderer = this.GetComponent<Renderer>();
//		state.active;
		selected = false;
		point_in_sequence = 0;
		move_speed = 30;
		point_to_move_to = transform.position;
		character_state = state.idle;
	}
	
	// Update is called once per frame
	void Update () {
		if(im_dead())
			GameObject.Destroy (gameObject);
		move();

		if(selected){
			if(Input.GetMouseButtonDown(1)){
				select_rightclick();
			}
		}

		attack();
	}

	public void move(){
		gameObject.transform.position = Vector3.MoveTowards(transform.position, point_to_move_to, (move_speed * Time.deltaTime));
		gameObject.transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position) + my_renderer.bounds.size.y / 2, transform.position.z);
	}

	public void patrol(){
		bool reached_point = false;
		Vector3 point_to_patrol_to = patrol_points [point_in_sequence];
		Vector3 pos = gameObject.transform.position;

		if (patrolling && reached_point == false && patrol_points.Count > 0)
		{
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

	public void select_unit(){
		selected = true;
		my_renderer.material.color = Color.blue;
	}

	public void deselect_unit(){
		selected = false;
		my_renderer.material.color = Color.white;
	}

	public void attack(){
		if(character_state == state.engaging && distance_to_target() > attack_range){
			point_to_move_to = (target.transform.position - gameObject.transform.position);
			Debug.Log ("Out of range: " + distance_to_target());
		}
		else if(character_state == state.engaging && distance_to_target() <= attack_range){
			point_to_move_to = gameObject.transform.position;
			character_state = state.attacking;
			Debug.Log ("Switching to attack mode");
		}
		else if (character_state == state.attacking && distance_to_target() <= attack_range){
			Debug.Log ("Attacking");
			if(target != null)
				make_attack();
		}
	}

	public float distance_to_target(){
		if(target != null)
			return Mathf.Abs (Vector3.Distance(gameObject.transform.position, target.transform.position));
		return 0;
	}

	public void make_attack(){
		if(Time.time >= next_attack_time){
			target.GetComponent<Unit>().cur_health -= weapon_damage;
			next_attack_time = Time.time + attack_speed;
		}
	}

	public GameObject select_rightclick(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit)){
			if(hit.transform.gameObject.GetComponent<Unit>())
			{
				target = hit.transform.gameObject;
				character_state = state.engaging;
			}
			if(hit.transform.gameObject.tag == "terrain"){
				Vector3 destination = ray.GetPoint (hit.distance);
				point_to_move_to = new Vector3(destination.x, destination.y + 1f, destination.z);
				target = null;
				character_state = state.walking;
			}
			return hit.transform.gameObject;
		}
		return null;
	}

	public bool im_dead(){
		if(cur_health <= 0){
			return true;
		}
		return false;
	}
}
