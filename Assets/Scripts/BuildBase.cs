using System;
using Configs;
using UnityEngine;

[Serializable]
/// <summary>
/// 建筑基础类型
/// </summary>
public class BuildBase
{
    public int build_Id;
    public int build_Type;
    public int build_Time;

    public GameObject model;
    public Vector3 pos;
    public float angle;

    public BuildConfig config;

    public BuildBase(int build_id, int build_Type, Vector3 pos, float angle)
    {
        Init(build_id, build_Type, pos, angle);
    }

    public virtual void Init(int build_id, int build_Type, Vector3 pos, float angle)
    {
        this.build_Id = build_id;
        this.build_Type = build_Type;
        build_Time = TimeUtils.GetCurrentTimestamp();
        this.pos = pos;
        this.angle = angle;
        config = GlobalConfig.GetBuildConfigByType(build_Type);
        ShowModel();
    }

    public virtual void ShowModel()
    {
        if (this.model == null || String.Compare(this.model.gameObject.name, config.name, StringComparison.Ordinal) != 1)
        {
            GameObject prefab = Resources.Load<GameObject>(config.res);
            this.model = GameObject.Instantiate(prefab);
            this.model.gameObject.name = config.name;
//            this.model.transform.SetParent();
        }

        this.model.transform.position = pos;
        this.model.transform.eulerAngles = new Vector3(-90, angle, -90);
    }

    public void ClickModel()
    {
        Debug.LogWarning(this.config.name);
    }
    
    public void Destroy()
    {
        if (model)
        {
            GameObject.Destroy(model);
            model = null;
        }
    }
}