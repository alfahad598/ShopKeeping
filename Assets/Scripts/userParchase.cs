using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
public class userParchase : MonoBehaviour
{

    public RawImage photoGraphView;
    public TMP_Text nameProduct;
    public TMP_Text descriptionProduct;
    public TMP_Text brandProduct;
    public TMP_Text colorProduct;
    public TMP_Text priceProduct;

    public  delegate void nameInformationPassing(string name, string phone, string address);
    public static event nameInformationPassing buyerInfoPassing;

    //Buyer details
    public TMP_InputField buyerName;
    public TMP_InputField buyerPhoneNumber;
    public TMP_InputField buyerAddress;
    public GameObject itemsWindows;
    public GameObject parchaseWindows;
  
    string image_url;
    
  
    private void Awake()
    {
        ItemsDownloader.productInformationEvent += setInformationOfProduct;
    }
    
   public void setInformationOfProduct(string setUrl, string setBrand, 
       string setColor, string setDescription, string setName, string setPrice)
    {
        //string name = itemInfo.ProductName.text;
        nameProduct.SetText(setName);
        descriptionProduct.SetText(setDescription);
        brandProduct.SetText (setBrand);
        colorProduct.SetText (setColor);
        priceProduct.SetText (setPrice);
        
        parchaseWindows.SetActive(true);
        itemsWindows.SetActive(false);
        image_url = setUrl;
        StartCoroutine(photographOfParchase());

        
    }

    public IEnumerator photographOfParchase()
    {
        UnityWebRequest photoDownload = UnityWebRequestTexture.GetTexture(image_url);
        yield return photoDownload.SendWebRequest();

        if (photoDownload.result == UnityWebRequest.Result.Success)
        {
            Texture2D photoDownloaded = ((DownloadHandlerTexture)photoDownload.downloadHandler).texture;
            photoGraphView.texture = photoDownloaded;
        }
    }
    public void backToItemsWindow()
    {
        parchaseWindows.SetActive(false);
        itemsWindows.SetActive(true);

       
    }
    public void ConfirmParchase()
    {
        buyerInfoPassing.Invoke(buyerName.text, buyerPhoneNumber.text, buyerAddress.text);
    }

    private void OnDestroy()
    {
        ItemsDownloader.productInformationEvent -= setInformationOfProduct;
    }
}
