using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleUIController
{
    private GameObject PlayerPanel, EnemyPanel;
    private AudioSource AudioSource;
    private Image BattleImage, PlayerImage;
    private Image[] EnemyImage;

    public BattleUIController(GameObject BattlePanel)
    {
        this.BattleImage = BattlePanel.GetComponent<Image>();
        this.AudioSource = BattlePanel.GetComponent<AudioSource>();

        this.PlayerPanel = BattlePanel.transform.GetChild(0).gameObject;
        this.PlayerImage = this.PlayerPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        this.PlayerImage.sprite = Resources.Load<Sprite>("Texture/Yuusya");

        this.EnemyPanel = BattlePanel.transform.GetChild(1).gameObject;
        this.EnemyImage=new Image[this.EnemyPanel.transform.childCount];
        for (int i = 0; i < this.EnemyImage.Length; i++)
        {
            this.EnemyImage[i] = this.EnemyPanel.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }

    public void DamagedPlayer()
    {
        this.BattleImage.GetComponent<Image>().StartCoroutine(DamagePlayer());
    }
    public void DamagedEntity(int Index)
    {
        this.BattleImage.StartCoroutine(DamageEntity(Index));
    }
    public void SetEnemyImage(List<EnemyStatus> Enemies)
    {
        Color showColor = new Color(1f, 1f, 1f, 1f);
        Color hideColor = new Color(1f, 1f, 1f, 0f);
        int i=0;
        for(; i < Enemies.Count; i++)
        {
            //Debug.Log(Enemies[i].GetType().Name);
            EnemyImage[i].sprite = Resources.Load<Sprite>("Texture/"+Enemies[i].Name);
            EnemyImage[i].color = showColor;
        }
        for (; i < 3; i++)
        {
            EnemyImage[i].color = hideColor;
        }
    }

    private IEnumerator DamagePlayer()
    {
        this.AudioSource.PlayOneShot(Resources.Load<AudioClip>("Sound/sword-gesture" + Random.Range(1, 4)));
        for (int i = 0; i < 3; i++)
        {
            this.PlayerPanel.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            this.PlayerPanel.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator DamageEntity(int Index)
    {
        this.AudioSource.PlayOneShot(Resources.Load<AudioClip>("Sound/sword-gesture" + Random.Range(1, 4)));
        for (int i = 0; i < 3; i++)
        {
            this.EnemyImage[Index].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            this.EnemyImage[Index].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
