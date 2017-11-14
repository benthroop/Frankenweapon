using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

	public void PlayGame()
	{

		SceneManager.LoadScene ("Race-Gingello");
	}
	public void Menu()
	{

		SceneManager.LoadScene ("Race-GingelloMenu");
	}
}
