using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DiceMoveState : IPlayerState{

    private List<Square> PastSquares;
    private int DiceCount;
    
    public DiceMoveState(int diceCount)
    {
        this.PastSquares = new List<Square>();
        this.DiceCount = diceCount;
    }
    
    public IPlayerState Next(GameController Controller)
    {
        Player P = Controller.GetCurrentPlayer();
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
                this.PastSquares.Add(P.CurrentSqare);
                this.DiceCount--;
            }
            P.MoveToNextSquare(next);
            UIController.Log("あと" + this.DiceCount + "マス進めます。");
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
