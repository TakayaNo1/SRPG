using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public SquareType Type = SquareType.DefaultSquare;
    public Square NorthSquare;
    public Square EastSquare;
    public Square WestSquare;
    public Square SouthSquare;
    private Transform Trans;

    void Start()
    {
        this.Trans = GetComponent<Transform>();
        GetComponent<MeshRenderer>().materials[0].mainTexture = Resources.Load<Texture>("Texture/Square/"+this.Type.ToString());
    }

    void Update()
    {

    }

    public Vector3 GetDirection(Square Square)
    {
        return Square.Trans.position - this.Trans.position;
    }
}

public enum SquareType
{
    DefaultSquare,
    MoneySquare,
    ItemSquare,
    EnemySquare,
    AccidentSquare,
    ShopSquare,
    ExpSquare,
    TeleportSquare,
    HealSquare
}
