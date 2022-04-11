using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        
        hide();
    }
    private void Start()
    {

        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActivebuildingTypeChanged;

    }
    private void BuildingManager_OnActivebuildingTypeChanged(object sender,BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
       if(e.activeBuildingType == null)
        {
            hide();
        }
        else
        {
            show(e.activeBuildingType.sprite);
        }
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }
    private void show(Sprite ghosSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghosSprite;
    }
    private void hide()
    {
        spriteGameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged -= BuildingManager_OnActivebuildingTypeChanged;
    }
}
