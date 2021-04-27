using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class administrator_script : MonoBehaviour
{
    postConfirmParchase postConfirmParchaseRef;
    public TMP_Text homeAppliance;
    public TMP_Text menwomenWardrobe;
    public TMP_Text officeEquipment;
    public TMP_Text mobilePhone;
    public TMP_Text autoMobile;
    public TMP_Text teleCommunication;
    public TMP_Text purchaseMessage;
    string first = "homeappliance";
    string[] itemsNameArraay = {"homeappliance", "menwomenwardrobe", "officeequipment",
    "mobilephone", "automobile", "telecommunication"};
    public string index_url;  
    private void Start()
    {
        StartCoroutine(readMessage());
        foreach (string index in itemsNameArraay)
        {
            
            index_url = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/" + index + "/index.json";
            StartCoroutine(loadItemsTotal_message(index));
        }
     
    }

    public IEnumerator loadItemsTotal_message(string itemsCase)
    {
       
        UnityWebRequest loadItemsIndex = UnityWebRequest.Get(index_url);
        yield return loadItemsIndex.SendWebRequest();
      
       if(loadItemsIndex.result == UnityWebRequest.Result.Success)
        {
            switch (itemsCase) {

                case "homeappliance":
                    homeAppliance.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                case "menwomenwardrobe":
                    menwomenWardrobe.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                case "officeequipment":
                    officeEquipment.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                case "mobilephone":
                    mobilePhone.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                case "automobile":
                    autoMobile.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                case "telecommunication":
                    teleCommunication.SetText(loadItemsIndex.downloadHandler.text);
                    break;
                
            }

        }

       
          
    }
    public void itemScene()
    {
        SceneManager.LoadScene("UserInterface", LoadSceneMode.Single);
    }
    public void itemUpadateScene()
    {
        SceneManager.LoadScene("UserShopData", LoadSceneMode.Single);
    }

    public IEnumerator readMessage()
    {

        UnityWebRequest getMessage = UnityWebRequest.Get(
            "https://test-5e868-default-rtdb.firebaseio.com/Message.json");
        yield return getMessage.SendWebRequest();
        if(getMessage.result == UnityWebRequest.Result.Success)
        {
            postConfirmParchaseRef = JsonUtility.FromJson<postConfirmParchase>(getMessage.downloadHandler.text);
        }
        purchaseMessage.SetText(postConfirmParchaseRef.product_BuyerName + " from " + postConfirmParchaseRef.product_BuyerAddress
          + " mobile number: " + postConfirmParchaseRef.product_BuyerPhone + " purchase " + postConfirmParchaseRef.product_Name
          + " " + postConfirmParchaseRef.product_Description + " Color: " + postConfirmParchaseRef.product_Color
          + " Price: " + postConfirmParchaseRef.product_Price + " Brand: " + postConfirmParchaseRef.product_Brand
          + " Quntity: " + postConfirmParchaseRef.product_Quantity);
       
    }
}
