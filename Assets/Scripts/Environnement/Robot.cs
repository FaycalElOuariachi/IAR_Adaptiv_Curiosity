using UnityEngine;

namespace IAR_AdaptiveCuriosity {

	/**
	 * Le robot est doté de trois degrés de libertés :
	 * • Il peut émettre un son de fréquence choisie sur la plage [ 0, 1 ]
	 * • Il possodèe deux roues, qu'il peut faire tourner indépendamment
	 */
	public class Robot {

		// ========= Attributs principaux du robots ========= //

		/**
		 * Position du robot dans l'espace
		 */
		public Vector3 position;

		/**
		 * Rayon du robot
		 */
		public float rayon;

		/**
		 * Taille de la salle dans laquelle le robot se déplace
		 */
		public Vector3 dimensionsSalle;

		/**
		 * Rotation du robot
		 */
		public Quaternion angleRotation;

		/*
		 * Vitesses des deux roues du robot
		 * (gauche, droite)
		 */
		public Vector2 vitesseRoues;

		/*
		 * Fréquence du son qu'émet le robot
		 * Doit être entre 0 et 1
		 * Si le robot n'émet aucun son, le mettre à -1
		 */
		public float frequenceSon;

		/**
		 * Rotation à effectuer par le robot
		 * (et non la rotation du robot)
		 * sous forme de Vector3
		 */
		private Vector3 angleRotationVector;

		// ========= Accesseurs des variables du robots ========= //

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

		/**
		 * Permet de récupérer la vitesse de la roue droite du robot
		 */
		public float roueDroite {
			get { return vitesseRoues.y; }
			set { vitesseRoues.y = value; }
		}

		/**
		 * Permet de récupérer la vitesse de la roue gauche du robot
		 */
		public float roueGauche {
			get { return vitesseRoues.x; }
			set { vitesseRoues.x = value; }
		}

		// ========= Constructeur de la class ========= //

		/**
		 * Constructeur
		 * Initialise la position, les vitesses des roues,
		 * la rotation à effectuer des roues à zéro,
		 * la taille de la salle à l'infini,
		 * le rayon du robot à 0
		 * et la fréquence du son à 0.5
		 */
		public Robot() {
			position = Vector3.zero;
			rayon = 0f;
			dimensionsSalle = Vector3.zero;
			dimensionsSalle.x = dimensionsSalle.y = dimensionsSalle.z = Mathf.Infinity;
			vitesseRoues = Vector2.zero;
			angleRotation = Quaternion.identity;
			angleRotationVector = Vector3.zero;
			frequenceSon = 0.5f;
			//roueDroite = 1f; roueGauche = 1f;
		}

		/**
		 * Fait avancer le robot, en fonction des vitesses de ses roues
		 */
		public void avancer() {
			// Distance à parcourir (coordonnée polaire)
			float r = 0f;

			// Rotation à effectuer (coordonée polaire)
			float theta = 0f;

			/**
			 * Calcul de r
			 * • Doit être égale à 1 si les deux roues ont une vitesse de 1
			 * • Doit être égale à 0 si les deux roues ont des vitesses opossées (par exemple 1 et -1)
			 */
			r = (roueDroite + roueGauche) / 2f;

			/**
			 * Calcul de theta
			 * • Doit être égale à 0 si les deux roues ont la même vitesse
			 * • Doit faire un tour complet en 3 secondes (~ 180 frames sur Unity) si les deux roues
			 *   ont pour vitesses 1 et -1
			 */
			theta = 2f * (roueGauche - roueDroite) / 3f;

			angleRotationVector.y = theta;
			Quaternion angle = Quaternion.Euler(angleRotationVector);
			angleRotation *= angle;

			float x_prime = r * Mathf.Cos (-Mathf.PI * angleRotation.eulerAngles.y / 180f) / 60f;
			float z_prime = r * Mathf.Sin (-Mathf.PI * angleRotation.eulerAngles.y / 180f) / 60f;

			x = Mathf.Max(Mathf.Min (x + x_prime, dimensionsSalle.x - rayon), -dimensionsSalle.x + rayon);
			z = Mathf.Max(Mathf.Min (z + z_prime, dimensionsSalle.z - rayon), -dimensionsSalle.z + rayon);
		}

	}

}