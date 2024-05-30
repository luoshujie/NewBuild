using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurBuildData
{
    public int build_Id;
    public int build_Type;
    public GameObject model;
    public Vector3 pos;
    public float angle;

    public void Destroy()
    {
        if (model)
        {
            GameObject.Destroy(model);
            model = null;
        }
    }
}

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public CurBuildData curBuildData;

    public List<BuildBase> BuildList;

    public float cell_x = 0.2f;
    public float cell_y = 0.2f;

    public ColliderTrigger ColliderTrigger;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BuildList = new List<BuildBase>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            CancelPreviewBuild();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CancelPreviewBuild();
            curBuildData = new CurBuildData();
            curBuildData.build_Type = Random.Range(1, 13);
        }

        if (curBuildData != null)
        {
            if (curBuildData.model == null)
            {
                BuildConfig config = GlobalConfig.GetBuildConfigByType(curBuildData.build_Type);
                GameObject prefab = Resources.Load<GameObject>(config.res);
                curBuildData.model = GameObject.Instantiate(prefab);
                BoxCollider collider = curBuildData.model.GetComponent<BoxCollider>();
                collider.isTrigger = true;
                ColliderTrigger = curBuildData.model.AddComponent<ColliderTrigger>();
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    curBuildData.pos = hit.point;
                    float x = curBuildData.pos.x % cell_x;
                    float y = curBuildData.pos.z % cell_y;
                    if (x > cell_x / 2)
                    {
                        x = x - cell_x;
                    }

                    if (y > cell_y / 2)
                    {
                        y = y - cell_y ;
                    }

                    curBuildData.pos.x -= x;
                    curBuildData.pos.z -= y;
                    curBuildData.pos.y = 0;
                    curBuildData.model.transform.position = curBuildData.pos;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (curBuildData != null && curBuildData.model != null)
            {
                if (ColliderTrigger != null && !ColliderTrigger.IsTriggerNow())
                {
                    AddBuild(curBuildData.build_Type, curBuildData.pos);
                    CancelPreviewBuild();
                }
                else
                {
                    if (ColliderTrigger == null)
                    {
                        Debug.LogWarning("trigger is null");
                    }
                    else
                    {
                        Debug.LogWarning(ColliderTrigger.IsTriggerNow());
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << LayerMask.NameToLayer($"Build")))
                {
                    int build_Id = int.Parse(hit.collider.gameObject.name);
                    if (build_Id != null)
                    {
                        BuildBase build = GetBuildById(build_Id);
                        if (build != null)
                        {
                            build.ClickModel();
                        }
                    }
                }
            }
        }
    }

    public void CancelPreviewBuild()
    {
        curBuildData?.Destroy();
        curBuildData = null;
    }

    public void AddBuild(int build_Type, Vector3 pos)
    {
        BuildBase newBuild = new BuildBase(GetOnlyBuildId(), build_Type, pos, 0);
        BuildList.Add(newBuild);
    }

    public void RemoveBuild(int build_Id)
    {
        for (int i = 0; i < BuildList.Count; i++)
        {
            if (BuildList[i].build_Id == build_Id)
            {
                BuildBase build = BuildList[i];
                BuildList.RemoveAt(i);
                build.Destroy();
                break;
            }
        }
    }

    public BuildBase GetBuildById(int build_Id)
    {
        BuildBase build = null;
        for (int i = 0; i < BuildList.Count; i++)
        {
            if (BuildList[i].build_Id == build_Id)
            {
                build = BuildList[i];
                break;
            }
        }

        return build;
    }

    public int onlyBuildId = 0;

    public int GetOnlyBuildId()
    {
        onlyBuildId += 1;
        return onlyBuildId;
    }
}