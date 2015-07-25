using UnityEngine;
using System.Collections;

public class Tile {
	private bool occupied;
	private bool defense_boost;
	private bool attack_boost;
	private string material_type;
	private Unit occupier;
	private string terrain_type;

	public bool Occupied{
		get{
			return occupied;
		}
		set{
			occupied = value;
		}
	}

	public bool Defense_boost{
		get{
			return defense_boost;
		}
		set{
			defense_boost = value;
		}
	}

	public bool Attack_boost{
		get{
			return attack_boost;
		}
		set{
			attack_boost = value;
		}
	}

	public string Material_type{
		get{
			return material_type;
		}
		set{
			material_type = value;
		}
	}

	public Unit Occupier{
		get{
			return occupier;
		}
		set{
			occupier = value;
		}
	}

	public string Terrain_type{
		get{
			return terrain_type;
		}
		set{
			terrain_type = value;
		}
	}
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
