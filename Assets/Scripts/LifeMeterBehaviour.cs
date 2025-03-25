using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeMeterBehaviour : MonoBehaviour
{
    public Sprite LifeSprite;
    public Sprite EmptyLifeSprite;
    public Image[] spriteRenderers;
    private int lifeCount;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<Image>();
        Image panelSprite = GetComponent<Image>();
        
        // Remove Parent sprite
        if (panelSprite != null)
        {
            for(int i = 0; i < spriteRenderers.Length; i++)
            {
                if(spriteRenderers[i] == panelSprite)
                {
                    List<Image> tempList  = new List<Image>(spriteRenderers);
                    tempList.RemoveAt(i);
                    spriteRenderers = tempList.ToArray();
                    break;
                }
            }
        }

        ResetHearts();
        Debug.Log(spriteRenderers.Length);
    }

    public void RemoveHeart()
    {
        int heartIndex = lifeCount - 1;
        if(heartIndex < spriteRenderers.Length && heartIndex >= 0)
        {
            spriteRenderers[heartIndex].sprite = EmptyLifeSprite;
            lifeCount--;
        }
    }    

    public void ResetHearts()
    {
        lifeCount = spriteRenderers.Length;
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = LifeSprite;
        }
    }
}
