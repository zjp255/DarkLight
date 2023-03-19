using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo instance;
    public TextAsset skillsInfoText;

    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        InitSkillInfoDict();//初始化字典
    }

    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;
        skillInfoDict.TryGetValue(id, out info);
        return info;
    }

    void InitSkillInfoDict()
    {
        string text = skillsInfoText.text;
        string[] skillInfoArray = text.Split('\n');
        foreach (string skillinfoStr in skillInfoArray)
        {
            string[] pa = skillinfoStr.Split(',');
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(pa[0]);
            info.name = pa[1];
            info.icon_name = pa[2];
            info.des = pa[3];
            string str_applytypp = pa[4];
            switch (str_applytypp)
            {
                case "Passive":
                    info.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applyType = ApplyType.MultiTarget;
                    break;
            }
            string str_applypro = pa[5];
            switch (str_applypro)
            {
                case "Attack":
                    info.applyProperty = ApplyProperty.ATK;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case"HP":               
                    info.applyProperty = ApplyProperty.Hp;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.Mp;
                    break;
            }
            info.applyValue = int.Parse(pa[6]);
            info.applyTime = int.Parse(pa[7]);
            info.mp = int.Parse(pa[8]);
            info.coldTime = int.Parse(pa[9]);
            switch (pa[10])
            {
                case "Swordman":
                    info.applicationType = ApplicationType.Swordman;
                    break;
                case "Magician":
                    info.applicationType = ApplicationType.Magician;
                    break;
            }
            info.level = int.Parse(pa[11]);
            switch (pa[12])
            {
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;               
            }
            info.distance = float.Parse(pa[13]);
            info.efx_name = pa[14];
            info.aniname = pa[15];
            info.anitime = float.Parse(pa[16]);
            skillInfoDict.Add(info.id, info);
        }
    }
}

//作用类型
public enum ApplyType
{ 
    Passive,//增益（加mp，hp）
    Buff,//增强
    SingleTarget,//单个目标
    MultiTarget//多个目标
}

//作用属性
public enum ApplyProperty
{ 
ATK,
Def,
Speed,
AttackSpeed,
Hp,
Mp
}

public enum ReleaseType
{ 
Self,
Enemy,
Position,
}

public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicationType applicationType;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    public string efx_name;
    public string aniname;
    public float anitime = 0;
}