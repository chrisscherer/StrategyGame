using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class csv_reader : MonoBehaviour {

	public List<General> game_generals = new List<General>();
	public List<Tile> game_tiles = new List<Tile>();
	public List<BoardSection> game_board_sections = new List<BoardSection>();
	private int j = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void read_in_generals(){
		StreamReader reader = new StreamReader("/Users/christopherscherer/TimeLords/Assets/Standard Assets/Scripts/general_list.csv");
		
		try
		{
			do
			{
				string[] general = reader.ReadLine().Split (',');
				//Debug.Log ("read line: " + j);
				General new_g = new General();
				for (int i = 0;i<general.Length;i++){
					if(i == 0){
						//Debug.Log ("General Name is: " + general[i]);
						new_g.Name = general[i];
					}
					else if(i == 1){
						//Debug.Log ("General can move " + general[i] + " soldiers.");
						new_g.Num_move = general[i];
					}
					else if(i == 2){
						//Debug.Log ("General can move " + general[i] + " tiles.");
						new_g.Move_range = general[i];
					}
					else if(i == 3){
						//Debug.Log ("General can roll " + general[i] + " dice to attack.");
						new_g.Attack_dice = general[i];
					}
					else if(i == 4){
						//Debug.Log ("General can add " + general[i] + " to attack rolls.");
						new_g.Attack_bonus = general[i];
					}
					else if(i == 5){
						//Debug.Log ("General can attack at range: " + general[i]);
						new_g.Attack_range = general[i];
					}
					else if(i == 6){
						//Debug.Log ("General can roll " + general[i] + " dice to defend.");
						new_g.Defense_dice = general[i];
					}
					else if(i == 7){
						//Debug.Log ("General can add " + general[i] + " to defense rolls.");
						new_g.Defense_bonus = general[i];
					}
					else if(i == 8){
						//Debug.Log ("General generates " + general[i] + " power gems per turn.");
						new_g.Power_gem_rate = general[i];
					}
					else if(i == 9){
						//Debug.Log ("General's first ability is:  " + general[i]);
						new_g.Ability_one = general[i];
					}
					else if(i == 10){
//						Debug.Log ("General's second ability is:  " + general[i]);
						new_g.Ability_two = general[i];
					}
				}
				game_generals.Add (new_g);
				Debug.Log ("In reader: " + game_generals[j].Name);
				j++;
			}
			while(reader.Peek () != -1);
		}

		catch
		{
			Debug.Log ("File is Empty");
		}
		finally
		{
			Debug.Log ("closing stream: " + game_generals.Count);
			reader.Close ();
		}
	}

	public void read_in_board_sections(){
		StreamReader reader = new StreamReader("/Users/christopherscherer/TimeLords/Assets/Standard Assets/Scripts/board_tiles.csv");
		int k = 0;
		BoardSection board_section = new BoardSection();
		try
		{
			do
			{
				if(k == 5){
					game_board_sections.Add (board_section);
					board_section = new BoardSection();
					k = 0;
				}
				else{
					string[] section_row = reader.ReadLine().Split (',');

					for (int i = 0;i<section_row.Length;i++){
						Tile new_tile = new Tile();
						new_tile.Terrain_type = section_row[i];
						board_section.Tiles[k,i] = new_tile;
					}
					k++;
				}
			}
			while(reader.Peek () != -1);
		}
		
		catch
		{
			Debug.Log ("File is Empty");
		}
		finally
		{
			game_board_sections[1].render_board_section(Vector3.zero);
			reader.Close ();
		}
	}
}
