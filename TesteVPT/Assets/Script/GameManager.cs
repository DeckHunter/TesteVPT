using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Hamburguer> hamburguers;
    public List<Ingredients> ingredients;

    public Hamburguer hamburguerCurrent;
    public Hamburguer hamburguerOrder;

    public List<Image> ingredientSlots;
    public List<Image> sauceSlots;

    public Image hamburguerSlot;

    private Relogio relogio;
    public int timer;

    public GameObject screenGameOver;
    public TextMeshProUGUI scoreText;
    public int score;
    public int bonus;

    void Start()
    {
        relogio = FindObjectOfType<Relogio>();
        screenGameOver.SetActive(false);
    }

    void Update()
    {
        if (relogio.minutoAtual == timer){
            gameOver();
        }
    }
    public void finalizeOrder(){

        bool isCorret = true;

        foreach (Ingredients item in hamburguerCurrent.ingredientsHamburguer)
        {
            if (hamburguerOrder.ingredientsHamburguer.Contains(item))
            {
                isCorret = true;
            }
            else {
                isCorret = false;
                break;
            }
        }
        if (isCorret)
        {
            score += (score * bonus);
        }
        else {
            score -= 5;
        }
        scoreText.text = score.ToString();
    }

    public void gameOver() {
        screenGameOver.SetActive(true);
    }
    public void usedBarbecue(){
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[0]);
    }
    public void usedMustard()
    {
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[1]);
    }
    public void usedPepper()
    {
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[2]);
    }
    public void usedWasabi()
    {
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[3]);
    }
    public void usedMayonnaise()
    {
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[4]);
    }
    public void usedKetchup()
    {
        hamburguerCurrent.ingredientsHamburguer.Add(ingredients[5]);
    }
}
