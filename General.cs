using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	private string name;
	private string num_move;
	private string move_range;
	private string attack_dice;
	private string attack_bonus;
	private string attack_range;
	private string defense_dice;
	private string defense_bonus;
	private string power_gem_rate;
	private string ability_one;
	private string ability_two;

	public string Name{
		get{
			return name;
		}
		set{
			name = value;
		}
	}
	public string Num_move{
		get{
			return num_move;
		}
		set{
			num_move = value;
		}
	}
	public string Move_range{
		get{
			return move_range;
		}
		set{
			move_range = value;
		}
	}
	public string Attack_dice{
		get{
			return attack_dice;
		}
		set{
			attack_dice = value;
		}
	}
	public string Attack_bonus{
		get{
			return attack_bonus;
		}
		set{
			attack_bonus = value;
		}
	}
	public string Attack_range{
		get{
			return attack_range;
		}
		set{
			attack_range = value;
		}
	}
	public string Defense_dice{
		get{
			return defense_dice;
		}
		set{
			defense_dice = value;
		}
	}
	public string Defense_bonus{
		get{
			return defense_bonus;
		}
		set{
			defense_bonus = value;
		}
	}
	public string Power_gem_rate{
		get{
			return power_gem_rate;
		}
		set{
			power_gem_rate = value;
		}
	}
	public string Ability_one{
		get{
			return ability_one;
		}
		set{
			ability_one = value;
		}
	}
	public string Ability_two{
		get{
			return ability_two;
		}
		set{
			ability_two = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
