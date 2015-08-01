using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_state : MonoBehaviour {
	
	private List<Player> players;
	private int turn_number;
	private bool game_over;
	private string player_name = "";
	private List<General> available_generals;
	private csv_reader my_csv_reader;

	private float tapSpeed;
	private float key1Time;
	private float key2Time;
	
	private float lastTapTime;
	private float lastRegenTime;
	private string lastInput;
	
	private string key1Name;
	private string key2Name;

	public List<Player> Players { get; set; }
	public int Turn_number { get; set; }
	public bool Game_over { get; set; }
	public string Player_name { get; set; }
	public List<General> Available_generals { get; set; }
	public csv_reader My_csv_reader { get; set; }

	public Camera camera;

	// Use this for initialization
	void Start () {
		this.camera = Camera.main;
		this.players = new List<Player>();
		this.available_generals = new List<General>();
		this.my_csv_reader = new csv_reader();
//		this.my_csv_reader.read_in_generals();
//		this.my_csv_reader.read_in_board_sections();
		Cursor.visible = true;
		tapSpeed = .25f;
		key1Name = null;
		key2Name = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.G)){
			
		}
		
		double_tap();
	}

	void add_player(string player_name) {
		if(player_name != ""){
			this.players.Add (new Player());
			this.players[this.players.Count - 1].Name = player_name;
			this.players[this.players.Count - 1].Id = this.players.Count - 1;
		}
	}
	
	void remove_player(string player_name) {

	}

	void list_players(){
		for(int i=0;i<= this.players.Count - 1; i++){
			Debug.Log(players[i].Name + " id: " + players[i].Id);
		}
	}

	void draw_generals(string args){
		foreach (Player player in this.players){
			for(int i=0;i<3;i++){
				int hold = Random.Range(0, this.my_csv_reader.game_generals.Count);
				player.My_generals.Add (this.my_csv_reader.game_generals[Random.Range(0, hold)]);
				this.my_csv_reader.game_generals.RemoveAt(hold);
			}
		}
	}

	void OnGUI(){
		GUI.depth = 1;
		if(GUI.Button(new Rect(10,10, 100, 20), "Spawn Archer")){
			spawn_unit(new Vector3(Random.Range (-10,10), Random.Range (-10, 10), 0), "archer");
		}
		if(GUI.Button(new Rect(10,40, 100, 20), "Spawn Swordsman")){
			spawn_unit(new Vector3(Random.Range (-10,10), Random.Range (-10, 10), 0), "swordsman");
		}
	}

	public void spawn_unit(Vector3 spawn_point, string unit_name){

		if (unit_name == "archer"){
			var clone = Instantiate(Resources.Load (unit_name), spawn_point, Quaternion.identity) as GameObject;
			clone.tag = unit_name;
		}
		else if(unit_name == "swordsman"){
			var clone = Instantiate(Resources.Load (unit_name), spawn_point, Quaternion.identity) as GameObject;
			clone.tag = unit_name;
		}
	}

	private void double_tap(){
		if (Input.GetMouseButtonDown(0))
		{
			select_click();
			if ((Time.time - lastTapTime) < tapSpeed && lastInput == "left_click")
			{
				//do stuff
				var obj = select_click();
				if(obj.GetComponent<Unit>()){
					var units = GameObject.FindGameObjectsWithTag(obj.tag);
					for (int i = 0; i < units.Length;i++){
						units[i].GetComponent<Unit>().select_unit();
					}
				}

			}
			lastTapTime = Time.time;
			lastInput = "left_click";
		}

	}

	private GameObject select_click(){
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit)){
			if(hit.transform.gameObject.GetComponent<Unit>())
			{
				if(hit.transform.gameObject.GetComponent<Unit>().selected){

				}
				else{
					hit.transform.gameObject.GetComponent<Unit>().select_unit();
				}
			}
			else{
				var units = GameObject.FindObjectsOfType<Unit>();

				for(int i = 0; i < units.Length; i++){
					units[i].deselect_unit();
				}
			}
			return hit.transform.gameObject;
		}
		return null;
	}
}
