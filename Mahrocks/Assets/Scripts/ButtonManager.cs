using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	public void starGame(string newGameScene){
		SceneManager.LoadScene (newGameScene);
	}
}
