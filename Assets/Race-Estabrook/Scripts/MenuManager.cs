using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void quit()
    {

        Application.Quit();
    }
    public void start()
    {
        SceneManager.LoadScene(0);

    }
    public void menu()
    {
        SceneManager.LoadScene(1);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
