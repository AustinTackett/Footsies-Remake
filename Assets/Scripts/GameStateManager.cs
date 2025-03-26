using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] FighterBehaviour fighter1;
    [SerializeField] FighterBehaviour fighter2;
    [SerializeField] LifeMeterBehaviour lifeMeter1;
    [SerializeField] LifeMeterBehaviour lifeMeter2;
    [SerializeField] TimerBehaviour preGameTimer;
    [SerializeField] TimerBehaviour timer;
    [SerializeField] RoundMeterBehaviour roundMeter1;
    [SerializeField] RoundMeterBehaviour roundMeter2;

    private GameState state;
    private bool isWaiting;

    private enum GameState 
    {
        FightStart,
        Fight,
        FightEnd,
    }

    void Awake()
    {
        isWaiting = false;
        state = GameState.FightStart;
    }

    private void Update()
    {
        switch(state) 
        {
            case GameState.FightStart:
                UpdateFightStart();
                break;
            case GameState.Fight:
                UpdateFight();
                break;   
            case GameState.FightEnd:
                UpdateFightEnd();
                break;        
        }
    }

    private void UpdateFightStart()
    {
        // This gets called way more times then it should
        // but sometimes the update order makes it so the player state
        // can change between the round end and round start
        // so this is a temporary fix
        preGameTimer.gameObject.SetActive(true);
        timer.enabled = false;
        fighter1.enabled = false;
        fighter2.enabled = false;
        fighter1.ResetPosition();
        fighter2.ResetPosition();
        fighter1.ResetAnimation();
        fighter2.ResetAnimation();
        lifeMeter1.ResetHearts();
        lifeMeter2.ResetHearts();

        if(preGameTimer.displayTime <= 0)
        {
            preGameTimer.gameObject.SetActive(false);
            timer.enabled = true;
            fighter1.enabled = true;
            fighter2.enabled = true;
            state = GameState.Fight;
        }
    }

    private void UpdateFight()
    {
        if(lifeMeter1.lifeCount <= 0)
        {
            if(!isWaiting)
            {
                Debug.Log("Player 2 won");
                StartCoroutine(WaitThenTransition(GameState.FightEnd));
                roundMeter2.AddRound();
            }
        }

        if(lifeMeter2.lifeCount <= 0)
        {
            if(!isWaiting)
            {
                Debug.Log("Player 1 won");
                StartCoroutine(WaitThenTransition(GameState.FightEnd));
            }
            roundMeter1.AddRound();
        }
    }

    private void UpdateFightEnd()
    {
        preGameTimer.ResetTime();
        timer.ResetTime();

        state = GameState.FightStart;
    }

    private IEnumerator WaitThenTransition(GameState transitionState)
    {
        isWaiting = true;
        yield return new WaitForSeconds(1);
        state = transitionState;
        isWaiting = false;
    }
}

