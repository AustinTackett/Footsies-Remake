using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoundMeterBehaviour : MonoBehaviour
{
    public bool leftToRight;
    [HideInInspector] public int roundsWon;
    private Image[] spriteRenderers;

    void Awake()
    {
        roundsWon = 0;
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

        ResetRounds();
    }

    public void AddRound()
    {
        if(roundsWon < spriteRenderers.Length)
        {
            int roundIndex = roundsWon;
            if(!leftToRight)
            {
                roundIndex = spriteRenderers.Length - roundIndex - 1;
            }

            spriteRenderers[roundIndex].gameObject.SetActive(true);
            roundsWon++;
            Debug.Log("Rounds" + roundsWon);
        }
    }    

    public void ResetRounds()
    {
        roundsWon = 0;
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].gameObject.SetActive(false);
        }
    }
}
