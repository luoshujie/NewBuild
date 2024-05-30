using System;
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
        ShowModel();
    }

    public virtual void ShowModel()
    {
        if (this.model == null || String.Compare(this.model.gameObject.name, "Cube", StringComparison.Ordinal) != 1)
        {
            GameObject prefab = Resources.Load<GameObject>("Cube");
            this.model = GameObject.Instantiate(prefab);
            this.model.gameObject.name = build_Id.ToString();
//            this.model.transform.SetParent();
        }

        this.model.transform.position = pos;
        this.model.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void ClickModel()
    {
        Debug.LogWarning(this.build_Id);
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