using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour
{

    #region Singleton
    private static DataManager _instance;
    public static DataManager instance
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

    //Get the path of the Game data folder
    string path = Application.streamingAssetsPath;

    //placed ingredients
    public List<Ingredients> placedIngredients = new List<Ingredients>();
    
    void Start()
    {
       GetIngredients(); 
       
    }
    //get ingredients from the folder
    void GetIngredients(){
        //Get all file names in images folder that are only pngs
        string[] ingredientFiles = Directory.GetFiles(path + "/Ingredients", "*.png");
        //loop through and display the file name without the path
        foreach (string i in ingredientFiles)
        {
            //create a blank ingredient
            Ingredients ingredient = new Ingredients();
            //set the sprite of the ingredient
            ingredient.ingredientSprite = IMG2Sprite.LoadNewSprite(i);
            //set the name of the ingredient
            ingredient.ingredientName = Path.GetFileNameWithoutExtension(i);
            //add the ingredient to the list
            DatabaseManager.instance.possibleIngredients.Add(ingredient);
        }
        //build the first recipe
        GameManager.instance.BuildRecipe();
    }

    //check if the ingredient is in the list
    public bool CheckIfIsIngredient(string ingredientName){
        //go through all ingredients    
        foreach(Ingredients i in DatabaseManager.instance.possibleIngredients){
            //check if the ingredient is in the list
            if(i.ingredientName == ingredientName){
                //exists
                return true;
            }
        }
        //does not exist
        return false;
    }
    //place ingrdient into placed ingridients
    public void placedIngredient(string ingredientName){
         //get the ingredient
        Ingredients ingredient = DatabaseManager.instance.possibleIngredients.Find(x => x.ingredientName == ingredientName);
        //check to see if placed ingredients contains this ingredient
        if(!placedIngredients.Contains(ingredient)){
            //play audio
            SoundManager.instance.PlayIngredientPlacedSound();
            //add the ingredient to the list
            placedIngredients.Add(ingredient);
            //if has ingredient, update the recipe UI
            UiManager.instance.UpdateRecipe(DatabaseManager.instance.currentRecipes[0], ingredient);
            //check the recipe
            GameManager.instance.CheckRecipe();
        } else {
            Debug.Log("already has ingredient");
        }

       
    }
}
