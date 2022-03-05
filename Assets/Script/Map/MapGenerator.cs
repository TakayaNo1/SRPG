using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private static GameObject prefab;
    private static Square StartSquare;
    void Start()
    {
        prefab = (GameObject)Resources.Load("Prefabs/DefaultSquare");
        
        StartSquare = GenerateDefaultMap(GetComponent<Transform>());
    }

    public static Square GetStartSquare()
    {
        return StartSquare;
    }


    /**
     * 30*30のランダムなマップ生成
     */
    public static Square GenerateDefaultMap(Transform Trans)
    {
        int N = 30;
        Square[][] squares = new Square[N][];
        Array types = Enum.GetValues(typeof(SquareType));

        for (int i = 0; i < N; i++)
        {
            squares[i] = new Square[N];
            for (int j = 0; j < N; j++)
            {
                squares[i][j] = GenerateSquare(i - 10, j - 10).GetComponent<Square>();
                squares[i][j].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
                squares[i][j].transform.parent = Trans;
            }
        }
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                SetNextSquare(squares, i, j);
            }
        }

        return squares[10][10];
    }

    /**
     * (x,z)座標にマスを生成
     */
    private static GameObject GenerateSquare(float x, float z)
    {
        Vector3 pos = new Vector3(x, -0.4f, z);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        obj.transform.Rotate(0,0,180);
        return obj;
    }
    /**
     *(i,j)マスを四方向のマスに関連付け
     */
    private static void SetNextSquare(Square[][] squares, int i, int j){
        int n = squares.Length;
        int m = squares[0].Length;

        if (i < n - 1)
        {
            squares[i][j].EastSquare = squares[i + 1][j];
        }
        if (i > 0)
        {
            squares[i][j].WestSquare = squares[i - 1][j];
        }
        if (j < m - 1)
        {
            squares[i][j].NorthSquare = squares[i][j + 1];
        }
        if (j > 0)
        {
            squares[i][j].SouthSquare = squares[i][j - 1];
        }
    }
}
