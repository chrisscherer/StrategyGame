using UnityEngine;
using System.Collections;

public class UnitGUI : MonoBehaviour {
	private Unit owner;
	public float width;
	public float height;
	public float float_height;

	public Unit[] units;

	// Use this for initialization
	void Start () {
		owner = gameObject.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
		units = GameObject.FindObjectsOfType<Unit>();
	}

	void OnGUI(){
		for(int i = 0; i < units.Length; i++){
			var healthbar = GUI.Button (new Rect(Screen.width - 200, 10 + (40 * i),150,20), units[i].Cur_Health + " / " + units[i].Health);
		}
	}
}
