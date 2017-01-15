using UnityEngine;
using System.Collections;

public class Jouet : MonoBehaviour {
	/**
	 * Jouet à représenter
	 */
	public IAR_AdaptiveCuriosity.Jouet jouet;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (waitForStart());

		transform.position = jouet.position;
		jouet.rayon = transform.FindChild("Balle").transform.localScale.x / 2f;
		Rect rec = transform.Find ("/Environnement/Salle/Sol").GetComponent<RectTransform> ().rect;
		jouet.dimensionsSalle = new Vector3(rec.width / 20f, 0f, rec.height / 20f);
	}

	private IEnumerator waitForStart() {
		yield return new WaitForEndOfFrame();
	}

	// Update is called once per frame
	void Update ()
	{
		transform.position = jouet.position;
	}

	public void bouger() {
		jouet.bouger ();
	}

	public void setFrequence (float f) {
		jouet.frequenceSon = f;
	}
}
