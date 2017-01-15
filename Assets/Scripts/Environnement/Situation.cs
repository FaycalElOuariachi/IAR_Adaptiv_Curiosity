using UnityEngine;

/*
 * Situation de l'environnement perçue par le robot. 
 * Caractérisée par sa distance au jouet et l'angle qui les sépare, en coordonnées polaires
 */

public class Situation {

	public float distanceJouet;

	public float thetaJouet;

	public float thetaMur;

	public Vector3 plusProcheMur;

	public IAR_AdaptiveCuriosity.Robot robot;

	public IAR_AdaptiveCuriosity.Jouet jouet;

	public float distanceMur; // Distance au mur le plus proche du robot


	public Situation(IAR_AdaptiveCuriosity.Robot r, IAR_AdaptiveCuriosity.Jouet j) {
		robot = r;
		jouet = j;
		computeDistanceJouet ();
		computeThetaJouet ();
		computeDistanceMur ();
		computeThetaMur ();

	}

	public float computeDistanceJouet() {
		distanceJouet = Vector3.Distance (robot.position, jouet.position);
		return distanceJouet;
	}

	public float computeThetaJouet() {
		thetaJouet = Vector3.Angle ( robot.angleRotation * new Vector3(1,0,0), jouet.position  - robot.position);
		return thetaJouet;
	}


	public Vector3 getRobotPos() {
		return robot.position;
	}

	public Vector3 getJouetPos() {
		return jouet.position;
	}

	public float computeDistanceMur() {
		Vector3 dimensions = robot.dimensionsSalle;
		Vector3 murGauche = new Vector3 (-dimensions.x, 0, robot.position.z);
		Vector3 murDroit = new Vector3 (dimensions.x, 0, robot.position.z);
		Vector3 murDevant = new Vector3 (robot.position.x, 0, dimensions.z);
		Vector3 murDerriere = new Vector3 (robot.position.x, 0, -dimensions.z);
		Vector3[] murs = { murGauche, murDroit, murDevant, murDerriere};

		float distance = Vector3.Distance (robot.position, murs[0]);
		plusProcheMur = murs [0];
		for (int i = 1; i < murs.Length; i++) {
			if (Vector3.Distance (robot.position, murs[i]) < distance) {
				distance = Vector3.Distance (robot.position, murs [i]);
				plusProcheMur = murs [i];
			}
		}

		distanceMur = distance;

		return distance;
	}

	public float computeThetaMur() {
		thetaMur = Vector3.Angle (robot.angleRotation * new Vector3(1,0,0), plusProcheMur - robot.position);
		return thetaMur;
	}

	public static float compareSituations(Situation a, Situation b) {
		return Mathf.Abs (a.distanceJouet - b.distanceJouet) +
			Mathf.Abs(a.thetaJouet - b.thetaJouet) +
			Mathf.Abs(a.distanceMur - b.distanceMur) +
			Mathf.Abs(a.thetaMur - b.thetaMur);
	}

	public void toDebug() {
		Debug.Log ("distance Jouet : " + distanceJouet);
		Debug.Log ("distance Mur : " + distanceMur);
		Debug.Log ("angle Jouet : " + thetaJouet );
		Debug.Log ("angle Mur : " + thetaMur );
	}


}
