using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DiceMoveState : IGameState
{

    private List<Square> PastSquares;
    private int DiceCount;
    
    public DiceMoveState(int diceCount)
    {
        this.PastSquares = new List<Square>();
        this.DiceCount = diceCount;
    }
    
    /**
     * 移動状態
     */
    public IGameState Next(GameController Controller)
    {
        Player P = Controller.GetCurrentPlayer();
        Boss Boss = Controller.Boss;
        Square next = P.GetNextSquare();

        if (next == null)
        {
            return this;
        }
        else
        {
            if (PastSquares.Count > 0 && this.PastSquares[this.PastSquares.Count - 1] == next)
            {
                this.PastSquares.RemoveAt(this.PastSquares.Count - 1);
                this.DiceCount++;
            }
            else
            {
                this.PastSquares.Add(P.GetSquare());
                this.DiceCount--;
            }
            P.MoveToNextSquare(next);
            UIController.Log("あと" + this.DiceCount + "マス進めます。");
            
            float distance = (Boss.transform.position - P.transform.position).sqrMagnitude;
            if (distance < 0.01)
            {
                Controller.GetComponent<UIController>().SetDicePanelVisible(false);
                return new SomeTextState(Boss.GetStatus().Name + "と遭遇した！", new BattleState(Boss.GetEnemyStatus()));
            }

            if (DiceCount == 0)
            {
                Controller.GetComponent<UIController>().SetDicePanelVisible(false);
                return new SquareEventState();
            }
            else
            {
                return this;
            }
        }
    }
}
