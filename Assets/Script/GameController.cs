using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private IGameState GameState;
    public IStoryState StoryState = new RootStoryState();

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
    /**
     * 状態遷移
     * 状態がnullになった場合、ゲームを終了し、リザルト画面へ
     */
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
     * 次の順番のプレイヤー/ボスへ
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
    /**
     * 現在のプレイヤー/ボス
     */
    public PlayableEntity GetCurrentPlayableEntity()
    {
        if(PlayerOrderIndex < Player.Length)
        {
            return Player[PlayerOrderIndex];
        }
        return Boss;
    }
    //現在のゲーム状態
    public IGameState GetGameState()
    {
        return this.GameState;
    }
    //現在のプレイヤーを変える
    public void SetCurrentPlayer(Player Player)
    {
        this.CurrentPlayer = Player;
    }
    //現在のプレイヤー
    public Player GetCurrentPlayer()
    {
        return CurrentPlayer;
    }
    //UIController
    public UIController GetUIController()
    {
        return this.UIController;
    }
    //CameraController
    public CameraController GetCameraController()
    {
        return this.CameraController;
    }

    //ストーリーやその他パラメータをリセット
    public void Reset()
    {
        this.StoryState = new RootStoryState();
        BattleState.EnemyLevel = 1;
    }
}
