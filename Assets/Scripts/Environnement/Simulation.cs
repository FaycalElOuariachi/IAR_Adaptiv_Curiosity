using UnityEngine;
using System.Collections.Generic;

namespace IAR_AdaptiveCuriosity {
	
	public class Simulation {

		/**
		 * Le robot du laboratoire
		 */
		public Robot robot;

		/**
		 * Le jouet du laboratoire
		 */
		public Jouet jouet;

		/**
		 * Valeur maximale que peut prendre la vitesse
		 * des roues du robot
		 */
		public static float maxVitesse = 5f;

		/**
		 * Valeur minimale que peut prendre la vitesse
		 * des roues du robot
		 */
		public static float minVitesse = -5f;

		// ========= Variables concernant l'apprentissage ========= //

		/**
		 * Taille des mémoires (des listes)
		 * concernant l'apprentissage
		 */
		public static int DELAY = 150;

		/**
		 * Liste des experts
		 */
		public List<Expert> experts;

		private Expert actuelExpert = null;


		public Simulation() {
			robot = new Robot ();
			jouet = new Jouet ();

			robot.jouet = jouet;

			experts = new List<Expert> ();
			experts.Add (new Expert ());
		}

		public void step () {
			Situation lastSituation = robot.getSituation();

			// Choix de l'action que le robot devra effectuer
			Action actionChoisie = stepExpert ();
			setActionToRobot (actionChoisie);

			// Exécution de l'action
			robot.avancer ();
			jouet.frequenceSon = robot.frequenceSon;
			jouet.positionRobot = robot.position;
			jouet.bouger ();

			// Mise à jour des erreurs, etc
			actuelExpert.listE.Add (actuelExpert.calculErreur ( lastSituation , robot.getSituation() ));
			actuelExpert.calculEmLP ();
		}

		/**
		 * Choisi l'expert qui prendra la main
		 */
		private Action stepExpert () {
			foreach ( Expert e in experts ) {
				if (e.critereActivation ()) {
					actuelExpert = e;

					break;
				}
			}

			if (actuelExpert == null)
				return null;

			Situation s = robot.getSituation ();

			Action a = actuelExpert.choixAction (s);

			actuelExpert.situationsActions.Add (new Couple<Situation, Action>(s, a));

			return a;
		}

		private void setActionToRobot (Action action) {
			robot.roueDroite = action.roueDroite;
			robot.roueGauche = action.roueGauche;
			robot.frequenceSon = action.frequenceSon;
		}

	}

}