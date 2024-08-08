using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{ 
    [SerializeField] Animator transitionAnim;
    public void StartLevel ()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
        transitionAnim.SetTrigger("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I am here");
    }

}
