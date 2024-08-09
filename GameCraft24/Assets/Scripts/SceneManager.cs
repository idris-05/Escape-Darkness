using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{ 
    [SerializeField] Animator transitionAnim;
    public void StartScene(int indice)
    {
        if (indice == 2)
        {
            Time.timeScale = 1;
        }
        StartCoroutine(LoadScene(indice));
        Time.timeScale = 1;
    }

    IEnumerator LoadScene(int indice)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(indice);
        transitionAnim.SetTrigger("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I am here");
    }

}
