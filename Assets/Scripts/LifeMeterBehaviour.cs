using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeMeterBehaviour : MonoBehaviour
{
    public Sprite LifeSprite;
    public Sprite EmptyLifeSprite;
    public Image[] spriteRenderers;
    public bool leftToRight;
    [HideInInspector] public int lifeCount;

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<Image>();
        Image panelSprite = GetComponent<Image>();

        // Remove panel sprite (get components in children also get component in self)
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
    }

    public void RemoveHeart()
    {
        int heartIndex = lifeCount - 1;

        // Is the array accessed left to right or right to left
        if(!leftToRight)
        {
            heartIndex = spriteRenderers.Length - heartIndex - 1;
        }


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
