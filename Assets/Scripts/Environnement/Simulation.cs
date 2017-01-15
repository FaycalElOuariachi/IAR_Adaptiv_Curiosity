using UnityEngine;

namespace IAR_AdaptiveCuriosity {
	
	public class Simulation {

		public Robot robot;

		public Jouet jouet;

		public Simulation() {
			robot = new Robot ();
			jouet = new Jouet ();
		}

		public void step () {
			robot.avancer ();
			jouet.frequenceSon = robot.frequenceSon;
			jouet.bouger ();
		}

	}

}