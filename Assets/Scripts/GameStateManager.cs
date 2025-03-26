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
        GameOver
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
            case GameState.GameOver:
                UpdateGameOver();
                break;   
        }
    }

    private void UpdateFightStart()
    {
        // This gets called way more times then it should
        // but sometimes the update order makes it so the player's state
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
                roundMeter2.AddRound();
                Debug.Log(roundMeter2.roundsWon);
                if(roundMeter2.roundsWon >= 3)
                {
                    StartCoroutine(WaitThenTransition(GameState.GameOver, 1));
                }
                else
                {
                    StartCoroutine(WaitThenTransition(GameState.FightEnd, 1));
                }
            }
        }

        if(lifeMeter2.lifeCount <= 0)
        {
            if(!isWaiting)
            {
                roundMeter1.AddRound();
                Debug.Log(roundMeter1.roundsWon);
                if(roundMeter1.roundsWon >= 3)
                {
                    StartCoroutine(WaitThenTransition(GameState.GameOver, 1));
                }
                else
                {
                    StartCoroutine(WaitThenTransition(GameState.FightEnd, 1));
                }
            }
        }
    }

    private void UpdateFightEnd()
    {
        preGameTimer.ResetTime();
        timer.ResetTime();

        state = GameState.FightStart;
    }

    private void UpdateGameOver()
    {
        Debug.Log("Game Over");
    }

    private IEnumerator WaitThenTransition(GameState transitionState, int waitTime)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        state = transitionState;
        isWaiting = false;
    }
}

