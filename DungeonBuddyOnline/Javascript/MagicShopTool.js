function getValues() 
{
    var value, minimumValue, maximumValue;
    var rarity = document.getElementById("BodyPlaceHolder_rarityDropDownList").value;

    if (rarity === "Common") 
    {
        minimumValue = 50;
        maximumValue = 100;
        
    }
    else if (rarity === "Uncommon") 
    {
        minimumValue = 101;
        maximumValue = 500;
    }
    else if (rarity === "Rare") 
    {
        minimumValue = 501;
        maximumValue = 5000;
    }
    else if (rarity === "Very Rare") 
    {
        minimumValue = 5001;
        maximumValue = 50000;
    }
    else if (rarity === "Legendary") 
    {
        minimumValue = 50000;
        maximumValue = 250000;
    }

    value = Math.floor(Math.random() * (maximumValue - minimumValue)) + minimumValue;

    document.getElementById("BodyPlaceHolder_minimumValueTextBox").value = minimumValue;
    document.getElementById("BodyPlaceHolder_maximumValueTextBox").value = maximumValue;
    document.getElementById("BodyPlaceHolder_valueTextBox").value = value;
}

//window.onload = getValues();