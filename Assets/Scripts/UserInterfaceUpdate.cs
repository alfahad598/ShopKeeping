using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UserInterfaceUpdate : MonoBehaviour
{

    public GameObject itemPrefabs;
    public GameObject itemParent;
    public delegate void jsonListener(string brand, string color, string description, string name, 
        string price, string image_url);
    public static event jsonListener jsonFound;

   
   
    int count;
    int endCount;
    string homeAppianece = "homeappliance";
    string menWomenWaredrobe = "menwomenwardrobe";
    string officeEquipment = "officeequipment";
    string autoMobile = "automobile";
    string mobilePhone = "mobilephone";
    string teleCommunications = "telecommunication";

    public void HomeAppliance()
    {

        count = 0;
        StartCoroutine(getItemsInfo(homeAppianece));
        
    }
    public void MenWomenWaredrobe()
    {
        count = 0;
        StartCoroutine(getItemsInfo(menWomenWaredrobe));

    }
    public void OfficeEquipment()
    {
        count = 0;
        StartCoroutine(getItemsInfo(officeEquipment));

    }
    public void Automobiles()
    {
        count = 0;
        StartCoroutine(getItemsInfo(autoMobile));

    }
    public void Mobilephone()
    {
        count = 0;
        StartCoroutine(getItemsInfo(mobilePhone));

    }
    public void Telecommunication()
    {
        count = 0;
        StartCoroutine(getItemsInfo(teleCommunications));

    }

    public IEnumerator getItemsInfo(string urlFromClick)
    {
        string itemsCountUrl = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/" + urlFromClick + ".json";
        itemsIndex itemsindexRef;
        UnityWebRequest itemsGetReq = UnityWebRequest.Get(itemsCountUrl);
        yield return itemsGetReq.SendWebRequest();
        
        if (itemsGetReq.result == UnityWebRequest.Result.Success)
        {
            itemsindexRef = JsonUtility.FromJson<itemsIndex>(itemsGetReq.downloadHandler.text);
            endCount = Convert.ToInt32( itemsindexRef.index);
        }

            string urlOfJson = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/all/"+ urlFromClick +
            "/" + count + ".json";
        
        UnityWebRequest webRequest = UnityWebRequest.Get(urlOfJson) ;
      
        yield return webRequest.SendWebRequest();
        if(webRequest.result == UnityWebRequest.Result.Success)
        {
          
            string JsonData = webRequest.downloadHandler.text;
            
            jsonObj jsonObj;
            jsonObj = JsonUtility.FromJson<jsonObj>(JsonData);
            
            jsonFound.Invoke(jsonObj.brand, jsonObj.color, jsonObj.description, jsonObj.name, 
                jsonObj.price, jsonObj.image_url);
            Instantiate(itemPrefabs, itemParent.transform);
           
             if (count < endCount-1)
              {
                count++;
                StartCoroutine(getItemsInfo(urlFromClick));
                
              }
              
        }

  
    }

    public void UserDataUpdate()
    {
        SceneManager.LoadScene("Administrator", LoadSceneMode.Single);
    }


}
