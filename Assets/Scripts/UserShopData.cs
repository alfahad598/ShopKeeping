using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;

public class UserShopData : MonoBehaviour
{
    public RawImage image;
    public TMP_Dropdown categories;
    public TMP_InputField brandOfProduct;
    public TMP_InputField colorOfProduct;
    public TMP_InputField descriptionOfProduct;
    public TMP_InputField nameOfProduct;
    public TMP_InputField priceOfProduct;
    public TMP_InputField image_urlOfProduct;
    itemsIndex itemsIndexTotal;
    public GameObject uploadButton;
    public GameObject confirmButton;
    public GameObject backButton;
    
    
    public string url_Json ;
    
    public string category;
    public int stringToInteger;

    public void indexDownloader()
    {
        if(categories.captionText.text == "Home appliance")
        {
            StartCoroutine(IndexDownloaderLoop("homeappliance"));
        }
        else if (categories.captionText.text == "Men/Women wardrobe")
        {
            StartCoroutine(IndexDownloaderLoop("menwomenwardrobe"));
        }
        else if (categories.captionText.text == "Automobile")
        {
            
            StartCoroutine(IndexDownloaderLoop("automobile"));
        }
        else if (categories.captionText.text == "Mobile phone")
        {
            StartCoroutine(IndexDownloaderLoop("mobilephone"));
        }
        else if (categories.captionText.text == "Tele communication")
        {
            StartCoroutine(IndexDownloaderLoop("telecommunication"));
        }
        else if (categories.captionText.text == "Office equipment")
        {
            StartCoroutine(IndexDownloaderLoop("officeequipment"));
        }
    }

    public IEnumerator IndexDownloaderLoop(string addTourl)
    {
        category = addTourl;
        string indexUrl = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/" + addTourl + ".json";
        UnityWebRequest getJsonOfIndex = UnityWebRequest.Get(indexUrl);
        yield return getJsonOfIndex.SendWebRequest();
        if(getJsonOfIndex.result == UnityWebRequest.Result.Success)
        {
            string jsonValue = getJsonOfIndex.downloadHandler.text;

            itemsIndexTotal = JsonUtility.FromJson<itemsIndex>(jsonValue);
            
        }

        stringToInteger = Convert.ToInt32(itemsIndexTotal.index);

        Debug.Log("Index number - "+ stringToInteger);
        url_Json = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/all/" + addTourl +
            "/" + stringToInteger + ".json";


    }

    public void OpenFileExplorer()
    {
       
            StartCoroutine(openedImagePreview(image_urlOfProduct.text));
        
    }

    public IEnumerator openedImagePreview(string urlPathOfImage)
    {
        UnityWebRequest imageUrl = UnityWebRequestTexture.GetTexture( urlPathOfImage);
        yield return imageUrl.SendWebRequest();

        if (imageUrl.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error Connection");
        }
        else
        {
            Texture foundImage = ((DownloadHandlerTexture)imageUrl.downloadHandler).texture;

            image.texture = foundImage;

        }
    }
  
    public void uploadJsonToFirebase()
    {
        jsonObj jsonFormatFromObject = new jsonObj(brandOfProduct.text, colorOfProduct.text,
            descriptionOfProduct.text, nameOfProduct.text, priceOfProduct.text, image_urlOfProduct.text);

        string jsonFrom = JsonUtility.ToJson(jsonFormatFromObject);

        StartCoroutine(postFuntion(jsonFrom));
       



    }
    public IEnumerator postFuntion(string json)
    {

        UnityWebRequest postToJsonUrl = UnityWebRequest.Put(url_Json, json);
        
        yield return postToJsonUrl.SendWebRequest();

   

        if (postToJsonUrl.result == UnityWebRequest.Result.ConnectionError || postToJsonUrl.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Connection or url problems. Can't post this " + json + " to " + url_Json);
        }
        else
            Debug.Log("Successs");
        stringToInteger++;
        string increment = Convert.ToString(stringToInteger);
        string indexUrlModified = "https://test-5e868-default-rtdb.firebaseio.com/itemsTotal/"
            + category +"/index.json";
        UnityWebRequest postToJsonUrlIndex = UnityWebRequest.Put(indexUrlModified, increment);
        yield return postToJsonUrlIndex.SendWebRequest();

        if (postToJsonUrlIndex.result == UnityWebRequest.Result.ConnectionError || postToJsonUrl.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Connection or url problems. Can't post this " + json + " to " + url_Json);
        }
        else
        {
            Debug.Log("Successs in index");
            confirmButton.SetActive(false);
            backButton.SetActive(true);
        }
    }

    public void ButtonActiveDeActive()
    {
        uploadButton.SetActive(false);
        confirmButton.SetActive(true);  
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ItemsWindowView()
    {
        SceneManager.LoadScene("UserInterface", LoadSceneMode.Single);
    }
}
