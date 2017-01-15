using UnityEngine;
using System.Collections;

using IAR_AdaptiveCuriosity;

public class GameManager : MonoBehaviour {

	public Simulation simulation;

	public Robot robot;

	public Jouet jouet;

	// Use this for initialization
	void Start () {
		simulation = new Simulation ();
		robot.robot = simulation.robot;
		jouet.jouet = simulation.jouet;
	}
	
	// Update is called once per frame
	void Update () {
		simulation.step ();
	}
}
