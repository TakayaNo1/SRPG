using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public GameController Controller;

    public GameObject ChatPanel;
    public Text ChatText;
    public GameObject MainChatPanel;
    public Text MainChatText;

    private static List<string> LogList = new List<string>();
    private readonly static int LogListLength = 12;
    private static bool ChatUpdated = false;

    public GameObject PlayerStatusPanel;
    public Text PlayerStatusText;

    public Button[] SelectButton = new Button[6];
    public Button[] SubButton = new Button[7];

    public Image[] DicePanel = new Image[9];

    public GameObject StoryPanel;
    public GameObject BattlePanel;
    private StoryUIController StoryUIController;
    private BattleUIController BattleUIController;

    void Start()
    {
        SetMainChatPanelVisible(false);
        this.StoryUIController = new StoryUIController(this.StoryPanel);
        this.BattleUIController = new BattleUIController(this.BattlePanel);
    }

    void Update()
    {
        ChatUpdate();
        PlayerStatusUpdate();
    }

    //StoryPanel
    public void SetStoryPanelVisible(bool value)
    {
        this.StoryPanel.SetActive(value);
    }
    public StoryUIController GetStoryUIController()
    {
        return this.StoryUIController;
    }

    //BattlePanel
    public void SetBattlePanelVisible(bool value)
    {
        this.BattlePanel.SetActive(value);
    }
    public BattleUIController GetBattleUIController()
    {
        return this.BattleUIController;
    }

    //DicePanel
    public void SetDicePanelVisible(int index, bool value)
    {
        this.DicePanel[index].gameObject.SetActive(value);
    }
    public void SetDicePanelVisible(bool value)
    {
        for (int i = 0; i < this.DicePanel.Length; i++)
        {
            this.SetDicePanelVisible(i, value);
        }
    }

    //MainButton
    public void HideAllButton()
    {
        HideSelectButton();
        HideSubButton();
    }
    public void HideSelectButton()
    {
        for (int i = 0; i < this.SelectButton.Length; i++)
        {
            this.SelectButton[i].gameObject.SetActive(false);
        }
    }
    
    //SubButton
    public void HideSubButton()
    {
        for (int i = 0; i < this.SubButton.Length; i++)
        {
            this.SubButton[i].gameObject.SetActive(false);
        }
    }

    //PlayerStatus
    private void PlayerStatusUpdate()
    {
        this.PlayerStatusText.text = this.Controller.GetCurrentPlayer().GetPlayerStatus().ToString();
    }


    //Chat
    private void ChatUpdate()
    {
        if (ChatUpdated)
        {
            if (LogList.Count == 0) return;

            SetMainChatPanelVisible(LogList[LogList.Count - 1].Length != 0);
            this.MainChatText.text = LogList[LogList.Count - 1];
            //Debug.Log(LogList[LogList.Count - 1]);

            if (LogList.Count <= LogListLength)
            {
                this.ChatText.text = LogList[0];
                for (int i = 1; i < LogList.Count; i++) 
                {
                    this.ChatText.text += "\n" + LogList[i];
                }
            }
            else
            {
                this.ChatText.text = LogList[LogList.Count - LogListLength];
                for (int i = LogList.Count - LogListLength + 1; i < LogList.Count; i++)
                {
                    this.ChatText.text += "\n" + LogList[i];
                }
            }

            ChatUpdated = false;
        }
    }
    private void SetMainChatPanelVisible(bool value)
    {
        this.MainChatPanel.SetActive(value);
    }
    public static void Log(string Log)
    {
        LogList.Add(Log);
        ChatUpdated = true;
    }
}
