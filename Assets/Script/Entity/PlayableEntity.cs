using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableEntity : MonoBehaviour
{
    public GameController GameController;
    public int X, Z;
    protected Square CurrentSqare;

    protected Transform Trans;

    public abstract EntityStatus GetStatus();
    public Parameta GetParameta(EntityParamsType type)
    {
        return GetStatus().Params[(int)type];
    }

    public void MoveToNextSquare(Square Next)
    {
        Vector3 dir = this.CurrentSqare.GetDirection(Next);
        Vector3 lookAtPos = this.Trans.position - new Vector3(dir.x, 0, dir.z);

        this.Trans.LookAt(lookAtPos);
        this.Trans.position += dir;

        this.CurrentSqare = Next;
    }
    public void MoveTo(Square Square)
    {
        Vector3 pos = Square.transform.position;
        pos.y = 0;
        this.Trans.position = pos;
    }
    public Square GetSquare()
    {
        return this.CurrentSqare;
    }
}
