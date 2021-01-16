using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(blala());
        SceneManager.LoadScene(2);
    }
    public void Quıt()
    {
        Application.Quit();
    }

    IEnumerator  blala()
    {
        yield return new WaitForSeconds(2f);
    }
}
