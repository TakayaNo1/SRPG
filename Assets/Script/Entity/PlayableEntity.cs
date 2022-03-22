using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayableEntity : MonoBehaviour
{
    public GameController GameController;
    public int X, Z;

    protected Square CurrentSqare;
    protected Transform Trans;

    private GameObject TextObject;
    private static readonly Vector3 Height = new Vector3(0, 0.5f, 0);

    virtual protected void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/TextObject");
        TextObject = Instantiate(prefab, transform.position + Height, Quaternion.identity);
        TextObject.transform.Rotate(90, 0, 0);
    }
    virtual protected void Update()
    {
        TextObject.transform.position = transform.position + Height;
    }

    public abstract EntityStatus GetStatus();
    public Parameta GetParameta(EntityParamsType type)
    {
        return GetStatus().Params[(int)type];
    }
    protected void SetNameText(string Name)
    {
        this.TextObject.GetComponent<TextMesh>().text=Name;
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

        this.CurrentSqare = Square;
    }
    public Square GetSquare()
    {
        return this.CurrentSqare;
    }
}
