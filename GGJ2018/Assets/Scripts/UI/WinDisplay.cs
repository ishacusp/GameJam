using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinDisplay : MonoBehaviour {
	public List<RectTransform> WipeSeeds;

	public float seedDelay = 0.1f;

	void OnEnable() {
		GoalControl.SceneInstance.AllPlanetsHarvested += Win;
	}

	void OnDisable() {
		GoalControl.SceneInstance.AllPlanetsHarvested -= Win;
	}

	void Win ()
	{
		WipeSeeds.ForEach (s => s.gameObject.SetActive (true));

		List<RectTransform> seeds = new List<RectTransform> (WipeSeeds);
		seeds.Sort ((s1, s2) => Random.Range (0, 2) == 1 ? 1 : -1);

		var wipeSequence = DOTween.Sequence ();

		float delay = 0f;
		foreach (RectTransform seed in seeds) {
			delay += seedDelay;
			wipeSequence.Insert(delay, seed.transform.DOLocalMoveX (1000f, 2f).SetEase (Ease.Linear));
		}

		wipeSequence.AppendCallback (() => SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1));
	}
}
