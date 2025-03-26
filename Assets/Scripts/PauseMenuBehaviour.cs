using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    private bool isOpen;

    void Awake()
    {
        isOpen = gameObject.activeInHierarchy;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if(isOpen)
        {   
            isOpen = false;
            Close();
        }
        else
        {   
            isOpen = true;
            Open();
        }
    }
}
