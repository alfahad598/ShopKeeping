using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class UserLogin : MonoBehaviour
{
    
    public TMP_InputField userName;
    public TMP_InputField passWord;
    public TMP_Text message;
    public static string url= "http://localhost/OnlineShopManagement/userLogin.php";


    public void StartLogin()
    {
        if(userName.text == "" && passWord.text== "" ){
        message.SetText("Please Enter your details");}

        else
        StartCoroutine(loginWithUsernameAndPassword(userName.text, passWord.text));

    }

    public void SignUp()
    {
        SceneManager.LoadScene("Signup", LoadSceneMode.Single);

    }


    public IEnumerator loginWithUsernameAndPassword(string username, string password)
    {

        WWWForm wWWForm = new WWWForm();

        wWWForm.AddField("userNameLogin", username);
        wWWForm.AddField("userPasswordLogin", password);

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, wWWForm))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                message.SetText("Connection Error");
            }
            else{
                message.SetText(unityWebRequest.downloadHandler.text);
                //  yield WaitForSeconds(1);
                if(unityWebRequest.downloadHandler.text == "Login Successfull"){
                    SceneManager.LoadScene("UserShopData", LoadSceneMode.Single);
                }
                
            }
               
            


        }


    }
}
