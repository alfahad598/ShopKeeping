using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Cart_infoDownloader : MonoBehaviour
{
    public TMP_Text d_productName;
    public TMP_Text d_productDescription;
    public TMP_Text d_productBrand;
    public TMP_Text d_productColor;
    public TMP_Text d_productPrice;
    public TMP_Text d_productQuantity;
    public Image photoGraph;
    void Start()
    {
        readCartInfo.passCartProductDetailsEvent += updateCartInfo;
    }


    void updateCartInfo(string BuyInfoPhoto, string BuyInfoBrand, string BuyInfoColor,
         string BuyInfoDescription, string BuyInfoName, string BuyInfoPrice)
    {

        d_productName.SetText(BuyInfoName);
        d_productDescription.SetText(BuyInfoDescription);
        d_productBrand.SetText(BuyInfoBrand);
        d_productColor.SetText(BuyInfoColor);
        d_productPrice.SetText(BuyInfoPrice);
        d_productQuantity.SetText("1");

        Debug.Log(BuyInfoPhoto);
        StartCoroutine(cartPhotoUpdate(BuyInfoPhoto));
    }

    public IEnumerator cartPhotoUpdate(string url)
    {

        UnityWebRequest photoDownload = UnityWebRequestTexture.GetTexture(url);
        yield return photoDownload.SendWebRequest();

        if(photoDownload.result == UnityWebRequest.Result.Success)
        {
            Texture2D photoTexture = ((DownloadHandlerTexture)photoDownload.downloadHandler).texture;
            photoGraph.sprite = Sprite.Create(photoTexture, new Rect(0, 0, 300, 300), new Vector2(0, 0), 1);
                  
        }

        readCartInfo.passCartProductDetailsEvent -= updateCartInfo;
    }
    private void OnDestroy()
    {
        readCartInfo.passCartProductDetailsEvent -= updateCartInfo;
    }
}



