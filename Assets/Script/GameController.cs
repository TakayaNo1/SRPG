using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private IGameState GameState;

    public static int PlayerSize = 1;
    public Player[] Player=new Player[PlayerSize];
    public Boss Boss;
    public int PlayerOrderIndex = 0;
    private Player CurrentPlayer;

    private UIController UIController;
    private CameraController CameraController;

    void Start()
    {
        this.GameState = new StartState();

        this.CurrentPlayer = Player[this.PlayerOrderIndex].GetComponent<Player>();
        this.UIController = GetComponent<UIController>();
        this.CameraController = GetComponent<CameraController>();
    }

    void Update()
    {
        if (this.GameState != null)
        {
            this.GameState = this.GameState.Next(this); //state next
        }
        else
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    /**
     * @return Next Playable Entity (Player or Boss)
     */
    public PlayableEntity ShiftPlayer()
    {
        PlayerOrderIndex++;
        if (PlayerOrderIndex < Player.Length)
        {
            this.CurrentPlayer = Player[PlayerOrderIndex];
        }
        else if (PlayerOrderIndex == Player.Length && Boss != null)
        {
            CameraController.SetTarget(Boss.gameObject);
            return Boss;
        }
        else
        {
            PlayerOrderIndex = 0;
            this.CurrentPlayer = Player[PlayerOrderIndex];
        }
        CameraController.SetTarget(this.CurrentPlayer.gameObject);
        return this.CurrentPlayer;
    }
    public PlayableEntity GetCurrentPlayableEntity()
    {
        if(PlayerOrderIndex < Player.Length)
        {
            return Player[PlayerOrderIndex];
        }
        return Boss;
    }

    public IGameState GetGameState()
    {
        return this.GameState;
    }
    public void SetCurrentPlayer(Player Player)
    {
        this.CurrentPlayer = Player;
    }
    public Player GetCurrentPlayer()
    {
        return CurrentPlayer;
    }
    public UIController GetUIController()
    {
        return this.UIController;
    }
    public CameraController GetCameraController()
    {
        return this.CameraController;
    }

    public void Reset()
    {
        StartState.StoryState = new RootStoryState();
        BattleState.EnemyLevel = 1;
    }
}
