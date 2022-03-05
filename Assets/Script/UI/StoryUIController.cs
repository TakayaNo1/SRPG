using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoryUIController
{
    private GameObject LeftPanel, RightPanel;
    private AudioSource AudioSource;
    public Image MainImage, LeftImage, RightImage;

    public StoryUIController(GameObject StoryPanel)
    {
        this.MainImage = StoryPanel.GetComponent<Image>();
        this.AudioSource = StoryPanel.GetComponent<AudioSource>();

        this.LeftPanel = StoryPanel.transform.GetChild(0).gameObject;
        this.LeftImage = this.LeftPanel.transform.GetChild(0).gameObject.GetComponent<Image>();

        this.RightPanel = StoryPanel.transform.GetChild(1).gameObject;
        this.RightImage = this.RightPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        //this.RightImage.sprite = Resources.Load<Sprite>("Texture/Player");
    }

    public void SetLeftImageVisible(bool Visible)
    {
        this.LeftPanel.SetActive(Visible);
    }
    public void SetRightImageVisible(bool Visible)
    {
        this.RightPanel.SetActive(Visible);
    }
    public void SetLeftImageAlpha(float alpha)
    {
        Color c=this.LeftImage.color;
        this.LeftImage.color = new Color(c.r,c.g,c.b, alpha);
    }
    public void SetRightImageAlpha(float alpha)
    {
        Color c = this.RightImage.color;
        this.RightImage.color = new Color(c.r, c.g, c.b, alpha);
    }
    public void SetLeftImage(string path)
    {
        this.LeftImage.sprite = Resources.Load<Sprite>(path);
    }
    public void SetRightImage(string path)
    {
        this.RightImage.sprite = Resources.Load<Sprite>(path);
    }
}
