using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TeleportState : IGameState
{
    private Player Player;
    private int stateCount = 0;
    private Vector3 scale;
    public IGameState Next(GameController Controller)
    {
        if (Player == null)
        {
            Player = Controller.GetCurrentPlayer();
        }

        if (stateCount == 0)
        {
            stateCount++;
            Controller.StartCoroutine(PlayerVanish());
        }
        else if (stateCount == 2)
        {
            stateCount++;
            int x = UnityEngine.Random.Range(0, 30);
            int z = UnityEngine.Random.Range(0, 30);
            Player.MoveToNextSquare(MapGenerator.GetSquare(x, z));
            Player.transform.LookAt(Player.transform.position + Vector3.forward);

            Controller.StartCoroutine(PlayerAppear());
        }
        else if (stateCount == 4)
        {
            return new SomeTextState(Player.Name+"はテレポートした", new EndState());
        }

        return this;
    }

    private IEnumerator PlayerVanish()
    {
        this.scale=Player.transform.localScale;
        float size = 1.0f;
        
        while (size > 0.0f) {
            size -= 0.02f;
            Player.transform.localScale = this.scale*size;

            yield return new WaitForSeconds(0.03f);
        }
        stateCount++;
    }
    private IEnumerator PlayerAppear()
    {
        float size = 0.0f;

        while (size < 1.0f)
        {
            size += 0.02f;
            Player.transform.localScale = this.scale*size;
            
            yield return new WaitForSeconds(0.03f);
        }
        stateCount++;
    }
}
