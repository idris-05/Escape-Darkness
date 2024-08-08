using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{ 
    [SerializeField] Animator transitionAnim;
    public void StartScene (int indice)
    {
        StartCoroutine(LoadScene(indice));
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
