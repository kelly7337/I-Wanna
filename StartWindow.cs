using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : WindowRoot
{
    public RecordWindow recordWindow;
    protected override void InitWindow()
    {
        base.InitWindow();
    }

    // Update is called once per frame
    void Update()
    {
        EnterRecordWindow();
    }

    private void EnterRecordWindow()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetWindowState(false);
            recordWindow.SetWindowState(true);
        }
    }
}
