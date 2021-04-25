using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class postConfirmParchase

{
    public string product_Name;
    public string product_Description;
    public string product_Color;
    public string product_Price;
    public string product_Brand;
    public string product_Quantity;
    
    public string product_BuyerName;
    public string product_BuyerPhone;
    public string product_BuyerAddress;

    public  postConfirmParchase(string P_name, string P_Description, string P_Color, string P_Price, string P_Brand,
        
        string P_Quantity, string P_BuyerName, string P_BuyerPhone, string P_BuyerAddress)
    {

        product_Name = P_name;
        product_Description = P_Description;
        product_Color = P_Color;
        product_Price = P_Price;
        product_Brand = P_Brand;
        product_Quantity = P_Quantity;
        product_BuyerName = P_BuyerName;
        product_BuyerPhone = P_BuyerPhone;
        product_BuyerAddress = P_BuyerAddress;


    }

}




