using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CartObj
{


    public string photo_link;
    public string c_brand;
    public string c_color;
    public string c_description;
    public string c_name;
    public string c_price;

    public CartObj(string photo_link_c, string brand_c,string color_c,
        string description_c, string name_c, string price_c)
    {
        photo_link = photo_link_c;
        c_brand = brand_c;
        c_color = color_c;
        c_description = description_c;
        c_name = name_c;
        c_price = price_c;
    }
}
