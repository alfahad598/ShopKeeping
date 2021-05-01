using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;
using UnityEngine.SceneManagement;


public class readCartInfo : MonoBehaviour
{
    public delegate void passCartInfo(string BuyInfoPhoto, string BuyInfoBrand, string BuyInfoColor,
        string BuyInfoDescription, string BuyInfoName, string BuyInfoPrice);
    public static event passCartInfo passCartProductDetailsEvent;
    int jsonIndex = 0;
    public int JsonIndexEnd;
    string url = "https://test-5e868-default-rtdb.firebaseio.com/Cart/0.json";
    ReadObj cartObjRef;

    public GameObject parentOfCart;
    public GameObject cartPrefabs;
    public GameObject cartWindow;
    public GameObject confirmWindow;
    
    public TMP_Text buyer_Name;
    public TMP_Text buyer_phone;
    public TMP_Text buyer_address;
    public TMP_Text buyer_ConfirmText;
    public TMP_InputField buyer_name_input;
    public TMP_InputField buyer_address_input;
    public TMP_InputField buyer_phone_input;
    private void Start()
    {
        downloadCartInformation();
    }
    public void downloadCartInformation()
    {

        StartCoroutine(jsonFileOfCart());

    }

    public IEnumerator jsonFileOfCart()
    {
        UnityWebRequest requestJson = UnityWebRequest.Get(url);
        yield return requestJson.SendWebRequest();

        if(requestJson.result == UnityWebRequest.Result.Success)
        {
         cartObjRef = JsonUtility.FromJson<ReadObj>(requestJson.downloadHandler.text);
            passCartProductDetailsEvent.Invoke(cartObjRef.photo_link, cartObjRef.c_brand, cartObjRef.c_color
                , cartObjRef.c_description, cartObjRef.c_name, cartObjRef.c_price);
            jsonIndex++;
            url = "https://test-5e868-default-rtdb.firebaseio.com/Cart/"+jsonIndex+".json";
            Instantiate(cartPrefabs, parentOfCart.transform, true);
            
        }
        UnityWebRequest cartIndexRead = UnityWebRequest.Get("https://test-5e868-default-rtdb.firebaseio.com/Cart/index.json");
        yield return cartIndexRead.SendWebRequest();
        if(cartIndexRead.result == UnityWebRequest.Result.Success)
        {
            JsonIndexEnd = Convert.ToInt32( cartIndexRead.downloadHandler.text);
        }
        if(jsonIndex < JsonIndexEnd)
        {
            StartCoroutine(jsonFileOfCart());
        }
    }

    public void confirmButtonForCart()
    {
        buyer_Name.SetText(buyer_name_input.text);
        buyer_address.SetText(buyer_address_input.text);
        buyer_phone.SetText(buyer_phone_input.text);
        buyer_ConfirmText.SetText("Purchase " + JsonIndexEnd + " items successfully .");

        cartWindow.SetActive(false);
        confirmWindow.SetActive(true);
    }
    public void backToItems()
    {
        SceneManager.LoadScene("UserInterface", LoadSceneMode.Single);
    }
}
