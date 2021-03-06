using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BossDiceMoveState : IGameState
{

    private List<Square> PastSquares;
    private int DiceCount;

    private Boss Boss;
    private Player Player;
    private bool isCoroutineActive = false;
    
    public BossDiceMoveState(int diceCount)
    {
        this.PastSquares = new List<Square>();
        this.DiceCount = diceCount;
    }
    
    public IGameState Next(GameController Controller)
    {
        if (Boss == null)
        {
            this.Boss = Controller.Boss;
        }
        //最も近いプレイヤー
        this.Player = NearByPlayer(Controller);

        //ボス移動ルーチン
        if (!isCoroutineActive)
        {
            this.Boss.StartCoroutine(this.BossMove());
        }

        //遭遇したか判定
        float distance = (Boss.transform.position - Player.transform.position).sqrMagnitude;
        if (distance < 0.01)
        {
            Controller.SetCurrentPlayer(Player);
            return new SomeTextState(Player.Name + "と遭遇した！", new BattleState(Boss.GetEnemyStatus()));
        }

        //移動
        if (this.DiceCount>0)
        {
            UIController.Log("あと" + this.DiceCount + "マス進めます。");
            return this;
        }
        else
        {
            return new SomeTextState("", new EndState());
        }
    }

    /**
     * ボス移動ルーチン
     * Nextがnullになるバグ有り
     * nullの場合ルーチンが永続的にActiveになる
     */
    private IEnumerator BossMove()
    {
        isCoroutineActive = true;
        Square Next = NextSquare();
        if (Next == null)
        {
            Debug.Log("West:"+ Boss.GetSquare().WestSquare.Type);
            Debug.Log("East:" + Boss.GetSquare().EastSquare.Type);
            Debug.Log("South:" + Boss.GetSquare().SouthSquare.Type);
            Debug.Log("North:" + Boss.GetSquare().NorthSquare.Type);
        }
        Boss.MoveToNextSquare(Next);
        this.DiceCount--;

        yield return new WaitForSeconds(0.7f);

        isCoroutineActive = false;
    }
    /**
     * Playerへ接近するためのマスを選択
     */
    private Square NextSquare()
    {
        Vector3 vec = Boss.transform.position - Player.transform.position;
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.z))
        {
            if (vec.x>0)
            {
                return Boss.GetSquare().WestSquare;
            }
            else
            {
                return Boss.GetSquare().EastSquare;
            }
        }
        else
        {
            if (vec.z > 0)
            {
                return Boss.GetSquare().SouthSquare;
            }
            else
            {
                return Boss.GetSquare().NorthSquare;
            }
        }
    }
    private Player NearByPlayer(GameController controller)
    {
        Player player=null;
        float distance = float.MaxValue;
        foreach(Player p in controller.Player)
        {
            float d = (Boss.transform.position - p.transform.position).magnitude;
            if (d < distance)
            {
                distance = d;
                player = p;
            }
        }
        return player;
    }
}
