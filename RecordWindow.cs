using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RecordWindow : WindowRoot
{
    public PlayerControl playerController;

    public GameObject[] dataArr;
    public GameWindow gameWindow;

    [HideInInspector]
    public int dataChooseNum;
    protected override void InitWindow()
    {
        base.InitWindow();
        ShowData();
    }

    private void ShowData()
    {
        for(int i = 0; i < dataArr.Length; i++)
        {
            ResourceSvc.SaveData saveData = resourceSvc.GetSaveData(i);
            string state = saveData.state;
            string deathCount = "Death: " + saveData.deathcount.ToString();
            string time = "Time: " + saveData.time;
            dataArr[i].transform.GetChild(1).GetComponent<Text>().text = state;
            dataArr[i].transform.GetChild(2).GetComponent<Text>().text = deathCount;
            dataArr[i].transform.GetChild(3).GetComponent<Text>().text = time;
        }
    }

    public void Update()
    {
        ChangeChoose();
        EnterGame();
    }
    private void ChangeChoose()
    {
        RectTransform dataChoose = transform.Find("DataChoose").GetComponent<RectTransform>();
        float posx = dataChoose.localPosition.x;
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            posx = posx >= 300f ? -300f : posx + 300f;
            dataChooseNum = dataChooseNum >= 2 ? 0 : dataChooseNum + 1;
            dataChoose.localPosition = new Vector2(posx, dataChoose.localPosition.y);
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            posx = posx <= -300f ? 300f : posx - 300f;
            dataChooseNum = dataChooseNum <= 0 ? 2 : dataChooseNum - 1;
            dataChoose.localPosition = new Vector2(posx, dataChoose.localPosition.y);
            
        }
    }

    public void EnterGame()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerController.InitPlayer();
            SetWindowState(false);
            gameWindow.SetWindowState(true);
        }
    }
}
