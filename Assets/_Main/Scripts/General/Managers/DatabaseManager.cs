using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region Singleton
    private static DatabaseManager _instance;
    public static DatabaseManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("object manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            GameObject.Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    
    public List<Ingredients> possibleIngredients = new List<Ingredients>();
    public List<Recipes> currentRecipes = new List<Recipes>();

    //get a random ingredient
    public Ingredients GetRandomIngredient(){
        //get a random ingredient
        Ingredients randomIngredient = possibleIngredients[Random.Range(0, possibleIngredients.Count)];
        Debug.Log("random ingredient: " + randomIngredient.ingredientName);
        //return it
        return randomIngredient;
    }
}
