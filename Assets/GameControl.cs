using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public bool planetDestroyed;
    public bool bossDestroyed;

    public CanvasGroup WinCanvas;

	void Update () {
		if (bossDestroyed)
        {
            WinCanvas.alpha += 1 * Time.deltaTime;
        }
	}
}
