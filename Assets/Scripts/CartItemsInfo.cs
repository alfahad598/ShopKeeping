using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class CartItemsInfo : MonoBehaviour
{
    CartObj jsonMap;
    int CartJsonIndex = 0;
   /* public delegate void passCartInfo(string BuyInfoPhoto, string BuyInfoBrand, string BuyInfoColor,
         string BuyInfoDescription, string BuyInfoName, string BuyInfoPrice);
    public static event passCartInfo passCartProductDetailsEvent;*/

    private void Awake()
    {
        ItemsDownloader.CartInformationEvent += cartItemsList;
    }

    
    public void cartItemsList(string BuyInfoPhoto, string BuyInfoBrand, string BuyInfoColor,
         string BuyInfoDescription, string BuyInfoName, string BuyInfoPrice)
    {
         jsonMap = new CartObj( BuyInfoPhoto,  BuyInfoBrand,  BuyInfoColor,
        BuyInfoDescription, BuyInfoName,  BuyInfoPrice);

        string jsonOfCartProduct = JsonUtility.ToJson(jsonMap);
        Debug.Log(jsonOfCartProduct);
        StartCoroutine(cartInfomationPassing(jsonOfCartProduct));
       
    }
    public IEnumerator cartInfomationPassing(string json)
    {
        string url = "https://test-5e868-default-rtdb.firebaseio.com/Cart/"+CartJsonIndex+".json";
        UnityWebRequest postCartJson = UnityWebRequest.Put(url, json);
        yield return postCartJson.SendWebRequest();

        if(postCartJson.result == UnityWebRequest.Result.Success)
        {
            CartJsonIndex++;
            Debug.Log("Success Cart Information");
        }

        UnityWebRequest postCartIndex = UnityWebRequest.Put("https://test-5e868-default-rtdb.firebaseio.com/Cart/index.json"
            , Convert.ToString( CartJsonIndex));
        yield return postCartIndex.SendWebRequest();
        if (postCartIndex.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success Cart Index");
        }
    }

    private void OnDestroy()
    {
        ItemsDownloader.CartInformationEvent -= cartItemsList;
    }

}
