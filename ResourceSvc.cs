using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ResourceSvc : MonoBehaviour
{
    public static ResourceSvc Instance;
    public XmlDocument dataDocument;
    public XmlNodeList dataNodeList;
    public void InitSvc()
    {
        Instance = this;
        InitSaveData();
    }

    private void InitSaveData()
    {
        string path = Application.dataPath + "/Resources/Data/SaveData.xml";
        StreamReader xmlFile = new StreamReader(path);
        dataDocument = new XmlDocument();
        dataDocument.LoadXml(xmlFile.ReadToEnd());
        dataNodeList = dataDocument.SelectSingleNode("data").ChildNodes;
    }

    public struct SaveData { 
        public string state;
        public int deathcount;
        public string time;
        public string savePosition;
    
    }


    public SaveData GetSaveData(int dataChooseNum)
    {
        XmlElement element = (XmlElement)dataNodeList[dataChooseNum];
        SaveData saveData = new SaveData();
        saveData.state = element.GetAttribute("state");
        saveData.deathcount = int.Parse(element.GetAttribute("death"));
        saveData.time = element.GetAttribute("time");
        saveData.savePosition = element.GetAttribute("save_position");
        return saveData;
    }
}
