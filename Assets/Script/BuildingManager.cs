using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingManager : MonoBehaviour
{
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

  
    public static BuildingManager Instance { get; private set; }
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCam;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
        mainCam = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = null;
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           if(activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition()))
            {
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
          
            
        }
    }
   
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs{activeBuildingType = activeBuildingType });
    }

   public BuildingTypeSO getActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType,Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

       Collider2D[] collider2DArr =  Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArr.Length == 0;

        if (!isAreaClear) return false;

       collider2DArr = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach(Collider2D collider2D in collider2DArr)
        {
            //collider on top
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                //has a BuildingTypeHolder
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    // there already a building of this type within the construction radius
                    return false;
                }
            }
        }

        float maxConstuctionRadius = 25;
        collider2DArr = Physics2D.OverlapCircleAll(position, maxConstuctionRadius);
        foreach (Collider2D collider2D in collider2DArr)
        {
            //collider on top
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //it building
                return true;
              
            }
        }
            return false;
    }
}
