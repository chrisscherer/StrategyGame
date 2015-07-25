using UnityEngine;
using System.Collections;

public class BoardSection : MonoBehaviour {
	private Tile[,] tiles = new Tile[5,5];

	public Tile[,] Tiles{
		get{
			return tiles;
		}
		set{
			tiles = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Report_Tiles (){
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				Debug.Log (this.tiles[i,j].Terrain_type);
			}
		}
	}

	public void render_board_section(Vector3 mp){
		for(int i=-2;i<3;i++){
			for(int j=-2;j<3;j++){
				GameObject cube = (GameObject)Instantiate(Resources.Load("Cube"));
				cube.renderer.material = Resources.Load<Material>(this.tiles[i + 2,j + 2].Terrain_type);
				cube.transform.position = new Vector3( mp.x + i, mp.y, mp.z + j);
				cube.tag = "terrain";
			}
		}
	}
}
