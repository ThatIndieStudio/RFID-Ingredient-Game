using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager instance
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
    

    //build a new recipe if recipe is done
    public void BuildRecipe(){
        //empty placed ingredients
        DataManager.instance.placedIngredients.Clear();
        //new recipe
        Recipes newRecipe = new Recipes();
        //instnatiate the list
        newRecipe.recipeIngredients = new List<Ingredients>();
        //get 2 random ingredients that are different
        List<Ingredients> randomIngredients = new List<Ingredients>();
        //duplicate the list from databasemanager to this list
        foreach(Ingredients i in DatabaseManager.instance.possibleIngredients){
            randomIngredients.Add(i);
        }
        // get 2 random ingredients
        int random = Random.Range(2, 4);
        for(int i = 0; i < random; i++){
            //get a random number between 0 and the number of ingredients
            int ingredient = Random.Range(0, randomIngredients.Count);
            //create that ingredient
            Ingredients newIngredient = randomIngredients[ingredient];
            //add it to the recipe
            newRecipe.recipeIngredients.Add(newIngredient);
            //delete that index from list
            randomIngredients.RemoveAt(ingredient);
        }
        //add this recipe to current ingredients
        DatabaseManager.instance.currentRecipes.Add(newRecipe);
        //build the recipe UI
        UiManager.instance.BuildRecipe(newRecipe);
    }

    //check if the recipe is done
    public void CheckRecipe(){
        //go through all recipes
        foreach(Recipes r in DatabaseManager.instance.currentRecipes){
            //check if the recipe is done
            if(CheckIfRecipeIsDone(r)){
                //recipe is done
                Debug.Log("recipe is done");
                RecipeComplete(r);
            } else{
                //recipe is not done
                Debug.Log("recipe is not done");
            }
        }
    }
    //check if recipe is done
    bool CheckIfRecipeIsDone(Recipes r){
        //go through all ingredients in the recipe
        foreach(Ingredients i in r.recipeIngredients){
            //check if the ingredient is in the placed list
            if(!DataManager.instance.placedIngredients.Contains(i)){
                //if it is not, return false
                return false;
            } 
        }
        //if all ingredients are placed, return true
        return true;
    }

    //Recipe complete
    public void RecipeComplete(Recipes r){
        StartCoroutine(RecipeCompleted(r));
    }
    IEnumerator RecipeCompleted(Recipes r){
        //play the complete sound
        SoundManager.instance.PlayCompleteScanSound();
        //wait 1 second for ingredient to be placed
        yield return new WaitForSeconds(2f);
        //update the ui
        UiManager.instance.RecipeComplete(r);
    }
}
