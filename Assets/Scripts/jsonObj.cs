

using UnityEngine;

[System.Serializable]
public class jsonObj
{
    public string brand;
    public string color;
    public string description;
    public string image_url;
    public string name;
    public string price;
   

    public jsonObj(string brandS, string colorS, string descriptionS, string nameS, string priceS, string image_urlS)
    {
        brand = brandS;
        color = colorS;
        description = descriptionS;
        name = nameS;
        price = priceS;
        image_url = image_urlS;
    } 
}



