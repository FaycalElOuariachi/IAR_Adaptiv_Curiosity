using UnityEngine;
using System.Collections.Generic;

namespace IAR_AdaptiveCuriosity
{
	public class Action
	{

		public static int nbValeursDiscretes = 100;

		/**
		 * Vitesse de la roue droite du robot
		 */
		public float roueDroite;

		/**
		 * Vitesse de la roue gauche du robot
		 */
		public float roueGauche;

		/**
		 * Fréquence du son qu'émet le robot
		 */
		public float frequenceSon;

		/**
		 * Taille d'un intervalle discrétisé
		 */
		public float tailleIntervalle {
			get {
				return (Simulation.maxVitesse - Simulation.minVitesse) / nbValeursDiscretes;
			}
		}

		/**
		 * Constructeur
		 * @param roueDroite La vitesse de la roue droite du robot
		 * @param roueGauche La vitesse de la roue gauche du robot
		 * @param frequence La fréquence du son émit par le robot
		 */
		public Action (float roueDroite, float roueGauche, float frequence)
		{
			this.roueDroite = roueDroite;
			this.roueGauche = roueGauche;
			this.frequenceSon = frequence;
		}

		/**
		 * Discrétisation des valeurs
		 * @param La valeur à discrétiser
		 * @return La valeur après discrétisation
		 */
		public float discretiseValeur(float vitesse) {
			// Calcul du reste de la division Euclidienne
			float reste = (vitesse - Simulation.minVitesse) % ((Simulation.maxVitesse - Simulation.minVitesse) / nbValeursDiscretes);

			// Arrondie
			if (reste > nbValeursDiscretes / 2)
				return vitesse - reste + 1;
			else
				return vitesse - reste;
		}

		/**
		 * Génère une action aléatoirement
		 * @return Une action générée aléatoirement
		 */
		public static Action newAleaAction() {
			float vitesseDroit = Random.value * (Simulation.maxVitesse - Simulation.minVitesse) + Simulation.minVitesse;
			float vitesseGauche = Random.value * (Simulation.maxVitesse - Simulation.minVitesse) + Simulation.minVitesse;
			float frequence = Random.value;

			return new Action (vitesseDroit, vitesseGauche, frequence);
		}

		/**
		 * Génère une liste d'actions aléatoirement
		 * @param nb Taille de la liste
		 * @return La liste d'action générée aléatoirement
		 */
		public static List<Action> newAleaActions (int nb) {
			List<Action> liste = new List<Action> ();
			for ( int i = 0 ; i < nb ; i++ ) {
				liste.Add (newAleaAction ());
			}
			return liste;
		}

		public static float compareActions (Action a, Action b) {
			return Mathf.Abs (a.roueDroite - b.roueDroite) +
				Mathf.Abs( a.roueGauche - b.roueGauche ) +
				Mathf.Abs( a.frequenceSon - b.frequenceSon );
		}

		public void toDebug() {
			Debug.Log ("Droite : " + roueDroite);
			Debug.Log ("Gauche : " + roueGauche);
			Debug.Log ("frequence : " + frequenceSon );
		}
	}
}

