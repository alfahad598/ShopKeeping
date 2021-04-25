using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
public class UserSignup : MonoBehaviour
{
public TMP_InputField Email;
public TMP_InputField Username;
public TMP_InputField Password;
public TMP_Text Message;
public static string url_OfSignup = "http://localhost/OnlineShopManagement/userSignup.php";

public void StartSignup(){
    if(Email.text == "" && Username.text== "" && Password.text == ""){
        Message.SetText("Please Enter your details");
    }
    else
    StartCoroutine(CreateUserInfo(Email.text, Username.text, Password.text));
}

public void BackToHome(){

SceneManager.LoadScene("Login", LoadSceneMode.Single);
}
public IEnumerator CreateUserInfo(string UserEmail, string UserUsername,
 string UserPassword){
     WWWForm wWWForm = new WWWForm();
    wWWForm.AddField("createEmail", UserEmail);
    wWWForm.AddField("createUserName", UserUsername);
    wWWForm.AddField("createPassword", UserPassword);

    using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url_OfSignup, wWWForm))
    {
        yield return unityWebRequest.SendWebRequest();

        if(unityWebRequest.result != UnityWebRequest.Result.Success){

            Message.SetText("Check Connection");
            
        }
        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            Message.SetText(unityWebRequest.downloadHandler.text);
        }
    }

}

}
