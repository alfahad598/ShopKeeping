using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ItemsDownloader : MonoBehaviour
{
    public RawImage photograph;
    public TMP_Text ProductBrand;
    public TMP_Text ProductColor;
    public TMP_Text ProducDescription;
    public TMP_Text ProductName;
    public TMP_Text ProductPrice;
    public string image_UrlParse;
    public GameObject userInterface;
    public GameObject userBuyingInfomation;

    public delegate void BuyInformation(string BuyInfoPhoto, string BuyInfoBrand, string BuyInfoColor,
        string BuyInfoDescription, string BuyInfoName, string BuyInfoPrice);
    public static event BuyInformation productInformationEvent;
    
    private void Start()
    {
        UserInterfaceUpdate.jsonFound += getFoundJson;
        productInformationEvent += Subcribe;
    }
    public void getFoundJson(string brand, string color, string description, string name,
        string price, string url)
    {
        ProductBrand.SetText(brand);
        ProductColor.SetText(color);
        ProducDescription.SetText(description);
        ProductName.SetText(name);
        ProductPrice.SetText(price);
        image_UrlParse = url;
        StartCoroutine(functionLocalImage(url));
        
    
    }

    public IEnumerator functionLocalImage(string localUrl)
    {

        UnityWebRequest getImage = UnityWebRequestTexture.GetTexture(localUrl);
        yield return getImage.SendWebRequest();

        if (getImage.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error Connection");
        }
        else
        {
            Texture2D foundImage = ((DownloadHandlerTexture)getImage.downloadHandler).texture;
            photograph.texture = foundImage;

            UserInterfaceUpdate.jsonFound -= getFoundJson;
        }
    }
  
    public void getBuyingInformation()
    {
        productInformationEvent.Invoke(image_UrlParse, ProductBrand.text, ProductColor.text,
            ProducDescription.text,ProductName.text, ProductPrice.text);

    }

    void Subcribe(string sUrl, string sBrand,
       string sColor, string sDescription, string sName, string sPrice)
    {
        //Debug.Log(sUrl + sBrand + sColor + sDescription + sName + sPrice);
    }
    

}
