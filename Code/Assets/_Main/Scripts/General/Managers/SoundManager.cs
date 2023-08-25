using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager instance
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
    [Header("Sound DB")]
    public AudioClip ingredientPlaced;
    public AudioClip recipeComplete;
    [Header("Audio player")]
    public AudioSource audioSource;

    public void PlayIngredientPlacedSound(){
        audioSource.PlayOneShot(ingredientPlaced);
    }
    public void PlayCompleteScanSound(){
        audioSource.PlayOneShot(recipeComplete);
    }
}
