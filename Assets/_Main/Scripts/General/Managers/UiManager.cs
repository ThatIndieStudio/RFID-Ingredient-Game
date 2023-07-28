using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UiManager : MonoBehaviour
{
    #region Singleton
    private static UiManager _instance;
    public static UiManager instance
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

    //get the ui elements
    public Transform recipeHolderTrf;
    public GameObject recipeObj;
    public GameObject completedObj;

    //add new recipe to the ui
    public void BuildRecipe(Recipes r){
        Debug.Log("build recipe");
        // instantiate it
        //set the recipe info
        recipeObj.GetComponent<RecipeHolderScript>().BuilSingledRecipeUi(r);
    }
    //update recipe when ingredient is placed
    public void UpdateRecipe(Recipes r , Ingredients i)
    {
        Debug.Log("update recipe");
        //update the recipe ui
        recipeObj.GetComponent<RecipeHolderScript>().UpdateRecipeUi(i);
    }

    //completed recipe
    public void RecipeComplete(Recipes r){
        Debug.Log("recipe complete");
        //run through the animation
        StartCoroutine(Animatecomplete());
    }
    IEnumerator Animatecomplete(){
        //dg tween the completed canvas group alpha to 1
        completedObj.GetComponent<CanvasGroup>().DOFade(1, 1f);
        //wait for 2 seconds
        yield return new WaitForSeconds(1f);
        //build new recipe
        GameManager.instance.BuildRecipe();
        yield return new WaitForSeconds(1f);
        //dg tween the completed canvas group alpha to 0
        completedObj.GetComponent<CanvasGroup>().DOFade(0, 1f);

    }
}
