using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Ingredients
{
    public Sprite ingredientSprite;
    public string ingredientName;
}

[System.Serializable] 
public class Recipes
{
    public List<Ingredients> recipeIngredients;
}

[System.Serializable]
public class ComSetUpString{
    public List<ComData> comSetup;
}
[System.Serializable]
public class ComData{
    public string comData;
}