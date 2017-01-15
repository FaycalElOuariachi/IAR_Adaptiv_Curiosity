using System.Collections.Generic;
using UnityEngine;

namespace IAR_AdaptiveCuriosity
{

	public class Expert
	{

		/**
		 * Liste des erreurs de prédiction
		 */
		public List<float> listE;

		/**
		 * Liste des moyennes des [Simulation.DELAY] dernières erreurs de prédiction
		 */
		public List<float> listEm;

		/**
		 * Liste des "learning progress"
		 */
		public List<float> listLP;

		public List<Couple<Situation, Action>> situationsActions;

		public List<Action> actions;

		private Situation lastSituation;

		public Expert ()
		{
			listE = new List<float> ();
			listEm = new List<float> ();
			listLP = new List<float> ();

			situationsActions = new List<Couple<Situation, Action>> ();
		}

		public Action choixAction(Situation situation) {

			if (Random.value <= 0.1)
				return Action.newAleaAction ();

			List<Action> listeActions = Action.newAleaActions (100);

			Situation s = null;

			float learningProgress = - Mathf.Infinity;
			float tmp;

			Action a = null;

			for ( int i = 0 ; i < listeActions.Count ; i++ ) {
				s = prediction (listeActions[i], situation);
				listE.Add( calculErreur (s,situation));
				calculEmLP ();
				tmp = listLP [listLP.Count - 1];
				listE.RemoveAt (listE.Count-1);
				listEm.RemoveAt (listEm.Count - 1);
				listLP.RemoveAt (listLP.Count - 1);

				if (tmp > learningProgress) {
					learningProgress = tmp;
					a = listeActions [i];
					lastSituation = s;
				}
			}

			Debug.Log ("Timon");
			situation.toDebug ();
			s.toDebug();

			return a;
		}

		/**
		 * Dans le papier, cette fonction représente MP
		 * Prend en argument une action a efectuer
		 * et la configuration de l'environnement
		 * et renvoie une prédiction sur la configuration qui devra être observée
		 */
		private Situation prediction(Action action, Situation situation) {

			if (situationsActions.Count == 0)
				return situation;

			if (situationsActions.Count == 1)
				return situationsActions [0].first;

			float minDifference = 4 * Action.compareActions( situationsActions[0].second, action ) + 3 * Situation.compareSituations ( situationsActions[0].first, situation );
			int indiceMin = 0;
			float tmp;

			for ( int i = 1 ; i < situationsActions.Count - 1 ; i++ ) {
				tmp = 4 * Action.compareActions( situationsActions[i].second, action ) + 3 * Situation.compareSituations ( situationsActions[i].first, situation );

				if (tmp < minDifference) {
					minDifference = tmp;
					indiceMin = i;
				}
			}

			return situationsActions [indiceMin + 1].first;
		}

		/**
		 * Dans le papier, cette fonction représente E
		 * @param s La situation prédite
		 * @param sa La situation réellement obsérvée
		 * @return L'erreur de prédiction
		 */
		public float calculErreur(Situation s, Situation sa) {
			return Situation.compareSituations(s, sa);
		}

		/**
		 * Calcul des valeurs Em et LP
		 */
		public void calculEmLP() {
			int t = listE.Count - 1;

			// Calcul de Em
			float Em = 0;
			for (int i = 0; i <= Mathf.Min( listE.Count - 1, Simulation.DELAY) ; i++) {
				Em += listE [t - i];
			}
			Em /= Mathf.Min( listE.Count - 1, Simulation.DELAY) + 1;
			listEm.Add (Em);

			// Calcul de LP
			listLP.Add (-(Em - listEm [t - Mathf.Min( listE.Count - 1, Simulation.DELAY)]));
		}

		/**
		 * Indique si dans la situation courrante, c'est à
		 * cet expert de travailler
		 */
		public bool critereActivation () {
			return true;
		}

		/**
		 * Divise l'expert en deux
		 */
		public void fission() {

		}
	}
}

