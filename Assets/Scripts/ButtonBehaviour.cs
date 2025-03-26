using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void WaitAndTransition(string sceneName) 
    {
        StartCoroutine(WaitForSoundAndTransition(sceneName));
    }

    public IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }

    public void goToMenu()
    {
        WaitAndTransition("Menu");
    }

    public void goToFight()
    {
        Debug.Log("fight");
        WaitAndTransition("Fight");
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WaitAndExit() 
    {
        StartCoroutine(WaitForSoundAndExit());
    }

    public IEnumerator WaitForSoundAndExit()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }

    public void QuitGame()
    {
        WaitAndExit() ;
    }
}
