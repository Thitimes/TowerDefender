using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resourcesUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Transform resourceTemplate;
    
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTranformDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTranformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);
        int index = 1;
        foreach(ResourceTypeSO resourceType in resourceTypeList.List)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index,-40);


            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTypeTranformDictionary[resourceType] = resourceTransform;
            
            index++;
        }
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        updateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        updateResourceAmount();
    }

    private void updateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.List)
        {

            Transform resourceTransform = resourceTypeTranformDictionary[resourceType];
           
            
            int resourceAmount = ResourceManager.Instance.resourceAmount(resourceType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());


        }

    }
}
