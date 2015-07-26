﻿using UnityEngine;
using System.Collections;

public class generate_terrain : MonoBehaviour {

	public string resource_name;
	public string[] material_names;

	// Use this for initialization
	void Start () {
		generate_flat_land(3, new Vector3(0,0,0));
		generate_flat_land(3, new Vector3(-7,0,0));
		generate_flat_land(3, new Vector3(7,0,0));
		generate_flat_land(3, new Vector3(0,0,-7));
		generate_flat_land(3, new Vector3(0,0,7));
		generate_flat_land(3, new Vector3(7,0,-7));
		generate_flat_land(3, new Vector3(-7,0,-7));
		generate_flat_land(3, new Vector3(-7,0,7));
		generate_flat_land(3, new Vector3(7,0,7));
	}
	
	// Update is called once per frame
	void Update () {

	}

	void generate_flat_land (int radius, Vector3 mp){
		for(int x=-radius;x<=radius;x++){
			for(int z=-radius;z<=radius;z++){
				GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
				cube.GetComponent<Renderer>().material = Resources.Load<Material>(get_material_name());
				cube.transform.position = new Vector3( mp.x + x, mp.y, mp.z + z);
				cube.tag = "terrain";
			}
		}
	}

	void generate_hilly_land (int radius){
		for(int x=0;x<=radius;x++){
			for(int y=0;y<=radius;y++){
				Vector3 mp = new Vector3(x, 0, y);
				GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
				cube.transform.position = mp;
//				cube.tag = "terrain";
				if(x >= 5 && x <= 45 && y >= 5 && y <= 45){
					check_hill(mp);
				}
			}
		}
	}
	
	void generate_random_height_land (){
		//for every x position
		for (int x=0;x <= 50;x++){
			//for every y position
			for(int y = 0; y<=50; y++){
				//get height one
				int h = Random.Range(1,4);
				//get height two
				int h2 = Random.Range(1,4);
				
				for(int j = 0;j <= h; j++){
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(x, j, y);
					cube.tag = "terrain";
				}
				for(int k = 0;k <= h2; k++){
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(-x, k, y);
					cube.tag = "terrain";
				}
				for(int k = 0;k <= h2; k++){
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(x, k, -y);
					cube.tag = "terrain";
				}
				for(int k = 0;k <= h2; k++){
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(-x, k, -y);
					cube.tag = "terrain";
				}
			}
		}
	}

	void make_square_ring(int radius, Vector3 midpoint){
		int midX = (int) midpoint.x;
		int midY = (int) midpoint.y;
		int midZ = (int) midpoint.z;

		for(int r = -radius;r <= radius;r++){
			for(int ra = -radius; ra <= radius; ra++){
				int rand = Random.Range(1, 20);
				if(rand > 2 && rand < 15){
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(midX + r, midZ, midY + ra);
					cube.tag = "terrain";
				}
				else if(rand > 15){
					int randX = Random.Range(1,3); 
					int randY = Random.Range(1,3);
					GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
					cube.transform.position = new Vector3(midX + r + randX, midZ, midY + ra + randY);
					cube.tag = "terrain";
				}
			}
		}
	}

	void clean_overlap (GameObject go){

		Bounds bounds = go.GetComponent<Renderer>().bounds;

		Collider[] hitColliders = Physics.OverlapSphere(go.transform.position, bounds.extents.magnitude / 2);
		Debug.Log (bounds.extents.magnitude);
		foreach(Collider col in hitColliders){
			GameObject other = col.gameObject;
			if(other == go){
				continue;
			}
			if(bounds.Intersects(other.GetComponent<Renderer>().bounds)) {
				Destroy(other);
				break;
			}
		}
	}

	Vector3 first_pass (Vector3 mp){
		int midX = (int) mp.x;
		int midY = (int) mp.y;
		int midZ = (int) mp.z;

		GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
		cube.transform.position = new Vector3(midX, midY  + Random.Range(1,4), midZ);
		cube.tag = "terrain";

		return cube.transform.position;
	}

	void fill (Vector3 start_point){
		for(int h = (int)start_point.y - 1; h > 0; h--){
			GameObject cube = (GameObject)Instantiate(Resources.Load(resource_name));
			cube.transform.position = new Vector3(start_point.x, h, start_point.z);
			cube.tag = "terrain";
		}
	}
	
	void check_hill (Vector3 midpoint){
		int check = Random.Range(1,150);
		if( check <= 3){
			int radius = Random.Range(1,7);
			make_hill(midpoint, radius, radius);
		}
	}

	void make_hill(Vector3 midpoint, int radius, int height) {
		int midY = (int) midpoint.y + 1;
		int midX = (int) midpoint.x;
		int midZ = (int) midpoint.z;
		for(int h=0;h <= height;h++){
			make_square_ring(radius - h, new Vector3(midX, midZ, midY + h));
		}
	}

	string get_material_name(){
		string mat_name = material_names[0];
		int rand_int = Random.Range(0,100);
		if(rand_int <= 80){
			mat_name = material_names[0];
		}
		else if( rand_int > 80 && rand_int <= 90 ){
			mat_name = material_names[1];
		}
		else{
			//mat_name = material_names[2];
		}
		return mat_name;
	}

}
