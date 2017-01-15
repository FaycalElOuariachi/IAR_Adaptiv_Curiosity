using UnityEngine;
using System.Collections;

using IAR_AdaptiveCuriosity;

public class Robot : MonoBehaviour
{
	/**
	 * Robot à représenter
	 */
	public IAR_AdaptiveCuriosity.Robot robot;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (waitForStart());

		transform.position = robot.position;
		transform.rotation = robot.angleRotation;
		robot.rayon = transform.FindChild("Corps").transform.localScale.x / 2f;
		Rect rec = transform.Find ("/Environnement/Salle/Sol").GetComponent<RectTransform> ().rect;
		robot.dimensionsSalle = new Vector3(rec.width / 20f, 0f, rec.height / 20f);
	}

	private IEnumerator waitForStart() {
		yield return new WaitForEndOfFrame();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = robot.position;
		transform.rotation = robot.angleRotation;
	}

	public void avancer() {
		robot.avancer ();
	}

	public float getFrequence() {
		return robot.frequenceSon;
	}

	public void setFrequence(float f) {
		robot.frequenceSon = f;
	}

	public void setDroite(float d) {
		robot.roueDroite = d;
	}

	public void setGauche(float g) {
		robot.roueGauche = g;
	}
}

