using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public PauseMenuBehaviour pauseMenu;

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

    public IEnumerator WaitForSoundAndExit()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }

    public IEnumerator WaitForSoundAndUnPause()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        pauseMenu.Close();
    }

    public void goToMenu()
    {
        WaitAndTransition("Main Menu");
    }

    public void goToFight()
    {
        WaitAndTransition("Fight");
    }

    public void QuitGame()
    {
        WaitAndExit() ;
    }    

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WaitAndExit() 
    {
        StartCoroutine(WaitForSoundAndExit());
    }

    public void ClosePauseMenu()
    {
        if(pauseMenu != null)
        {
            StartCoroutine(WaitForSoundAndUnPause());
        }
    }
}
