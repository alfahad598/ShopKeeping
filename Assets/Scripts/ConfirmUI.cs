using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class ConfirmUI : MonoBehaviour
{
    public TMP_Text nameProductConfirm;
    public TMP_Text descriptionProductConfirm;
    public TMP_Text brandProductConfirm;
    public TMP_Text colorProductConfirm;
    public TMP_Text priceProductConfirm;
    public TMP_Text buyerNameConfirm;
    public TMP_Text buyerPhoneConfirm;
    public TMP_Text buyerAddressConfirm;

    public GameObject parchaseWindow;
    public GameObject confirmWindow;

    userParchase userParchaseRef;
    private void Awake()
    {
        ItemsDownloader.productInformationEvent += setConfirmUI;
        userParchase.buyerInfoPassing += setBuyerInfo;
    }

    void setConfirmUI(string urlForConfirm, string brandForConfirm, string colorForConfirm,
        string descriptionForConfirm, string nameForConfirm, string priceForConfirm)
    {

        nameProductConfirm.SetText(nameForConfirm);
        descriptionProductConfirm.SetText(descriptionForConfirm);
        brandProductConfirm.SetText(brandForConfirm);
        colorProductConfirm.SetText(colorForConfirm);
        priceProductConfirm.SetText(priceForConfirm);


    }
    void setBuyerInfo(string nameBuyer, string phoneBuyer, string addressBuyer)
    {
        buyerNameConfirm.SetText(nameBuyer);
        buyerPhoneConfirm.SetText(phoneBuyer);
        buyerAddressConfirm.SetText(addressBuyer);
    }
    public void setActiveConfirmUi()
    {

        confirmWindow.SetActive(true);
        parchaseWindow.SetActive(false);


    }


    public void MessageSendCoRoutine()
    {
        postConfirmParchase postConfirmParchaseRef = new postConfirmParchase(nameProductConfirm.text, descriptionProductConfirm.text, colorProductConfirm.text,
            priceProductConfirm.text, brandProductConfirm.text, "1", buyerNameConfirm.text,
            buyerPhoneConfirm.text, buyerAddressConfirm.text);


        string ConvertedJson = JsonUtility.ToJson(postConfirmParchaseRef);
        StartCoroutine(postConfirmMessage(ConvertedJson));
    }

    public IEnumerator postConfirmMessage(string JsonString)
    {


        string url = "https://test-5e868-default-rtdb.firebaseio.com/Message.json";

        UnityWebRequest sendMessage = UnityWebRequest.Put(url, JsonString);
        yield return sendMessage.SendWebRequest();
        if (sendMessage.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("success in message");
        }
        else
            Debug.Log(sendMessage.result);
    }
   public void backFromConfirmParchase()
    {
        SceneManager.LoadScene("UserInterface", LoadSceneMode.Single);
    }
}
