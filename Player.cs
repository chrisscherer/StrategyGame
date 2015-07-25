using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	private List<General> my_generals = new List<General>();
	private General current_general;
	private int power_gem_count;
	private string name;
	private int id;

	public string Name{
		get{
			return name;
		}
		set{
			name = value;
		}
	}

	public General Current_general{
		get{
			return current_general;
		}
		set{
			current_general = value;
		}
	}

	public List<General> My_generals{
		get{
			return my_generals;
		}
		set{
			my_generals = value;
		}
	}

	public int Id{
		get{
			return id;
		}
		set{
			id = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
