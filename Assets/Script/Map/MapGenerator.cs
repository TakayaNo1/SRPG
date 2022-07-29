using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private static GameObject prefab;
    private static Square[][] Maps;
    void Start()
    {
        prefab = (GameObject)Resources.Load("Prefabs/DefaultSquare");
        
        Maps = GenerateDefaultMap(GetComponent<Transform>());
    }

    public static Square GetSquare(int x,int z)
    {
        return Maps[x][z];
    }


    /**
     * 30*30のランダムなマップ生成
     */
    public static Square[][] GenerateDefaultMap(Transform Trans)
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

        return squares;
    }
    /**
     * Map A
     */
    public static Square[][] GenerateMapA(Transform Trans)
    {
        int N = 30;
        Square[][] squares = new Square[N][];
        Array types = Enum.GetValues(typeof(SquareType));
        int x = 10, y = 10, dirX = 1, dirY = 0;

        for (int i = 0; i < N; i++)
        {
            squares[i] = new Square[N];
        }
        for (int i = 1; i < N-1; i++)
        { 
            squares[0][i] = GenerateSquare(-10, i-10).GetComponent<Square>();
            squares[0][i].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
            squares[0][i].transform.parent = Trans;

            squares[N-1][i] = GenerateSquare(N-11, i - 10).GetComponent<Square>();
            squares[N-1][i].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
            squares[N-1][i].transform.parent = Trans;

            squares[i][0] = GenerateSquare(i-10, -10).GetComponent<Square>();
            squares[i][0].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
            squares[i][0].transform.parent = Trans;

            squares[i][N-1] = GenerateSquare(i - 10, N-11).GetComponent<Square>();
            squares[i][N-1].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
            squares[i][N-1].transform.parent = Trans;
        }

        
        for (int i = 0; i < 400; i++)
        {
            if (squares[x][y] == null)
            {
                squares[x][y] = GenerateSquare(x - 10, y - 10).GetComponent<Square>();
                squares[x][y].Type = (SquareType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
                squares[x][y].transform.parent = Trans;
            }
            if(x == 0 || x == N - 1 || y == 0 || y == N-1)
            {
                x = UnityEngine.Random.Range(0, N);
                y = UnityEngine.Random.Range(0, N);
                int dir = UnityEngine.Random.Range(0, 4);
                if (dir == 0) { dirX = 1; dirY = 0; }
                if (dir == 1) { dirX = -1; dirY = 0; }
                if (dir == 2) { dirX = 0; dirY = 1; }
                if (dir == 3) { dirX = 0; dirY = -1; }
            }
            else
            {
                x += dirX;
                y += dirY;
            }
        }
        
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if(squares[i][j] != null)SetNextSquare(squares, i, j);
            }
        }

        return squares;
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
