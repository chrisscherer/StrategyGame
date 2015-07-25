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

	public List<Player> Players { get; set; }
	public int Turn_number { get; set; }
	public bool Game_over { get; set; }
	public string Player_name { get; set; }
	public List<General> Available_generals { get; set; }
	public csv_reader My_csv_reader { get; set; }

	// Use this for initialization
	void Start () {
		this.players = new List<Player>();
		this.available_generals = new List<General>();
		this.my_csv_reader = new csv_reader();
		this.my_csv_reader.read_in_generals();
		this.my_csv_reader.read_in_board_sections();
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
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
		player_name = GUI.TextField(new Rect(10, 10, 200, 20), player_name, 25);
		if(GUI.Button(new Rect(Screen.width / 9.2f,Screen.height / 1.5f, Screen.width / 2.65f, Screen.height / 4), "Add Player")){
			this.add_player(player_name);
			player_name = "";
		}
		if(GUI.Button(new Rect(Screen.width / 8f,Screen.height / 3f, Screen.width / 5f, Screen.height / 10), "Add Player")){
			this.list_players();
		}
		if(GUI.Button(new Rect(Screen.width / 12f,Screen.height / 5f, Screen.width / 5f, Screen.height / 10), "Draw Generals")){
			this.draw_generals("start");
		}
		if(GUI.Button(new Rect(Screen.width / 15f,Screen.height / 8f, Screen.width / 5f, Screen.height / 10), "Draw Generals")){
			Debug.Log (this.my_csv_reader.game_generals.Count);
		}
		if(this.players.Count > 0 && this.players[0].My_generals.Count > 0){
			for(int k = 0; k < this.players[0].My_generals.Count; k++){
				GUI.Box(new Rect(Screen.width / 2 + (k * 150), Screen.height - 100, Screen.width / 6, Screen.height / 6), this.players[0].My_generals[k].Name);
			}
		}
	}
}
