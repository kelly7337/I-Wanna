using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance;
    public StartWindow startWindow;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ClearWindow();
        InitGame();
    }

    private void ClearWindow()
    {
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        startWindow.SetWindowState(true);
    }

    private void InitGame()
    {
        ResourceSvc resourceSvc = GetComponent<ResourceSvc>();
        resourceSvc.InitSvc();
    }
}
