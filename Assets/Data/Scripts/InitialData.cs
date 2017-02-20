using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

[System.Serializable]
public class InitialData
{
    public float RotationSpeedInit;
    public float RotationSpeedMax;
    public float RotationAcceleMove;
    public float RotationAcceleStop;
    public float HumanHp;
    public float HumanHeal;
    public float HumanESpeed;
    public float HumanSpeed;
    public float SunAtk;
    public float SunRangeStart;
    public float SunRangeEnd;
}

[System.Serializable]
public class Datalist
{
    public InitialData[] init;
}



[System.Serializable]
public class TimeInfo
{
    public int type;
    public float limitTime;
    public float humanHp;
    public float humanRegen;
    public float humanCreateTime;
    public float humanSpeed;
    public float humanESpeed;
    public float obstacleCreateTime;
    public float helperCreateTime;
}
[System.Serializable]
public class TimeTable
{
    public TimeInfo[] timeInfo;
}