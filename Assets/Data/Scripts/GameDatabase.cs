using System.Collections.Generic;
using System.IO;
using UnityEngine;

class GameDatabase
{
    private static GameDatabase s_instance = null;
    private Dictionary<string, float> m_initData = new Dictionary<string, float>();
    private Dictionary<int, TimeInfo> m_timeData = new Dictionary<int, TimeInfo>();

    public static GameDatabase Instance()
    {
        if (s_instance == null)
        {
            s_instance = new GameDatabase();
            GameDatabase.Instance().InitData();
        }
        return s_instance;
    }
    public float GetInitData(string key)
    {
        return m_initData[key];
    }
    public TimeInfo GetTimeData(int key)
    {
        return m_timeData[key];
    }
    public void InitData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("init_info");
        var loadData = JsonUtility.FromJson<Datalist>(textAsset.text);
        m_initData.Add("RotationSpeedInit", loadData.init[0].RotationSpeedInit);
        m_initData.Add("RotationSpeedMax", loadData.init[0].RotationSpeedMax);
        m_initData.Add("RotationAcceleMove", loadData.init[0].RotationAcceleMove);
        m_initData.Add("RotationAcceleStop", loadData.init[0].RotationAcceleStop);
        m_initData.Add("HumanHp", loadData.init[0].HumanHp);
        m_initData.Add("HumanHeal", loadData.init[0].HumanHeal);
        m_initData.Add("HumanESpeed", loadData.init[0].HumanESpeed);
        m_initData.Add("HumanSpeed", loadData.init[0].HumanSpeed);
        m_initData.Add("SunAtk", loadData.init[0].SunAtk);
        m_initData.Add("SunRangeStart", loadData.init[0].SunRangeStart);
        m_initData.Add("SunRangeEnd", loadData.init[0].SunRangeEnd);

        textAsset = Resources.Load<TextAsset>("time_info");
        var loadData2 = JsonUtility.FromJson<TimeTable>(textAsset.text);
        
        for(int i = 0; i < loadData2.timeInfo.Length; i++)
        {
            m_timeData.Add(loadData2.timeInfo[i].type, loadData2.timeInfo[i]);
        }
    }

}