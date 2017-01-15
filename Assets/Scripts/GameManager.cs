using UnityEngine;
using System.Collections;

using IAR_AdaptiveCuriosity;

public class GameManager : MonoBehaviour {

	public Simulation simulation;

	public RobotIllusion robot;

	public JouetIllusion jouet;

	private bool flag = true;
	private int counter = 0;


	// Use this for initialization
	void Start () {
		simulation = new Simulation ();
		robot.robot = simulation.robot;
		jouet.jouet = simulation.jouet;
	}
	
	// Update is called once per frame
	void Update () {
		while (flag) {
			counter++;
			if (counter < 10000) {
				simulation.step ();
			}
			else {
				flag = false;
			}
		}
		Debug.Log (counter);
		simulation.step ();
	}
}
