using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class RecipeHolderScript : MonoBehaviour
{
    public List<GameObject> ingredientObjs;
    public List<Image> ingredientImages;
    public List<TMP_Text> ingredientName;
    public List<GameObject> checkObjs;

    //build out the recipe UI
    public void BuilSingledRecipeUi(Recipes r){
        //hide all objets in the ingredient objs
        foreach(GameObject g in ingredientObjs){
            g.SetActive(false);
        }
        //hide all check objs
        foreach(GameObject g in checkObjs){
            g.SetActive(false);
        }
        //set the ingredient image
        foreach(Ingredients i in r.recipeIngredients){
            //turn on the object
            ingredientObjs[r.recipeIngredients.IndexOf(i)].SetActive(true);
            //set the name
            ingredientName[r.recipeIngredients.IndexOf(i)].text = i.ingredientName;
            //set the image
            ingredientImages[r.recipeIngredients.IndexOf(i)].sprite = i.ingredientSprite;
            //set the color
            ingredientImages[r.recipeIngredients.IndexOf(i)].color = Color.white;
            //change the name of the gameobject to the name of ingredient
            ingredientImages[r.recipeIngredients.IndexOf(i)].gameObject.name = i.ingredientName;
            
           
        }
    }
    //updating when an ingredient is added
    public void UpdateRecipeUi(Ingredients i){
        //go through each image in list and if the name is the same as the ingredient name
        foreach(Image img in ingredientImages){
            if(img.gameObject.name == i.ingredientName){
                //set the color to green
                img.color = Color.gray;
                //show the check
                checkObjs[ingredientImages.IndexOf(img)].SetActive(true);
                AnimateCorrect(img.gameObject);
            }
        }
    }
    //when the recipe is correct do a little animation
    public void AnimateCorrect(GameObject g){
        // rotate the image
        g.transform.DORotate(new Vector3(0,360,0), .5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(2);
        // bounce the image
        g.transform.DOLocalMoveY(50, .5f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);

    }
}
