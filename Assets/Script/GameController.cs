using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int PlayerSize = 1;
    public Player[] Player=new Player[PlayerSize];
    public int PlayerOrderIndex = 0;
    private Player CurrentPlayer;

    private UIController UIController;

    void Start()
    {
        this.CurrentPlayer = Player[this.PlayerOrderIndex];
        this.UIController = GetComponent<UIController>();
    }

    void Update()
    {
        
    }

    public Player GetCurrentPlayer()
    {
        return CurrentPlayer;
    }
    public UIController GetUIController()
    {
        return this.UIController;
    }
}
