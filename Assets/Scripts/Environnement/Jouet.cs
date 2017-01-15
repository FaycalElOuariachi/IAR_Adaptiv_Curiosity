using UnityEngine;

namespace IAR_AdaptiveCuriosity {

	public class Jouet {

		// ========= Attributs principaux du jouet ========= //

		/**
		 * Position du jouet dans l'espace
		 */
		public Vector3 position;

		/**
		 * Position du robot dans l'espace
		 */
		public Vector3 positionRobot;

		/**
		 * Rayon du jouet
		 */
		public float rayon;

		/**
		 * Taille de la salle dans laquelle le jouet se déplace
		 */
		public Vector3 dimensionsSalle;

		/*
		 * Fréquence du son qu'émet le robot
		 * Doit être entre 0 et 1
		 * Si le robot n'émet aucun son, le mettre à -1
		 */
		public float frequenceSon;

		/**
		 * Intervalles des fréquences
		 * (Y ajouter les bornes 0 et 1 à chaque bout)
		 */
		private float[] intervallesFrequences = { 1f/3f , 2f/3f };

		/**
		 * direction du jouet
		 */
		private Quaternion direction;

		// ========= Accesseurs des variables du jouet ========= //

		/**
		 * Permet de récupérer la coordonnée x de la position du robot
		 */
		public float x {
			get { return position.x; }
			set { position.x = value; }
		}

		/**
		 * Permet de récupérer la coordonnée y de la position du robot
		 */
		public float y {
			get { return position.y; }
			set { position.y = value; }
		}

		/**
		 * Permet de récupérer la coordonnée z de la position du robot
		 */
		public float z {
			get { return position.z; }
			set { position.z = value; }
		}

		// ========= Constructeur de la class ========= //

		/**
		 * Constructeur
		 * Initialise la position, les vitesses des roues,
		 * la rotation à effectuer des roues à zéro,
		 * la taille de la salle à l'infini,
		 * le rayon du robot à 0
		 * et la fréquence du son à -1 ( c'est-à-dire qu'il n'émet rien)
		 */
		public Jouet() {
			position = new Vector3(-3f, 0, -4);
			positionRobot = Vector3.zero;
			rayon = 0f;
			dimensionsSalle = Vector3.zero;
			dimensionsSalle.x = dimensionsSalle.y = dimensionsSalle.z = Mathf.Infinity;
			frequenceSon = -1f;
			direction = Quaternion.identity;
		}

		/**
		 * Fait bouger le jouet, en fonction du osn émit par le robot
		 */
		public void bouger() {
			switch (intervalleSon ()) {
			case -1:
				break;
			case 0:
				randomMove ();
				break;
			case 1:
				break;
			case 2:
				jumpInto ();
				break;
			}
		}

		/**
		 * Renvoie l'indice de l'intervalle dans lequel se trouve la fréquence
		 * émit par le robot
		 */
		private int intervalleSon() {
			if (frequenceSon == -1) {
				return -1;
			}
			int indice = 0;
			foreach ( float x in intervallesFrequences ) {
				if (frequenceSon > x)
					indice++;
				else
					break;
			}
			return indice;
		}

		private void randomMove() {

			float theta = (2f*Random.value-1f) * 90f;

			Quaternion angle = Quaternion.Euler(new Vector3(0f,theta,0f));
			direction *= angle;

			float x_prime = Mathf.Cos (-Mathf.PI * direction.eulerAngles.y / 180f) / 5f;
			float z_prime = Mathf.Sin (-Mathf.PI * direction.eulerAngles.y / 180f) / 5f;

			x = Mathf.Max(Mathf.Min (x + x_prime, dimensionsSalle.x - rayon), -dimensionsSalle.x + rayon);
			z = Mathf.Max(Mathf.Min (z + z_prime, dimensionsSalle.z - rayon), -dimensionsSalle.z + rayon);
		}

		private void jumpInto() {
			
		}

	}

}