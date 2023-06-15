using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Hamburger> hamburguers;
    public List<Ingredients> ingredients;

    public Hamburger hamburguerCurrent;
    public Hamburger hamburguerOrder;

    public List<Image> ingredientSlots;
    public List<Image> sauceSlots;

    public Image hamburguerSlot;

    private Relogio relogio;
    public int timer;

    public GameObject screenGameOver;
    public TextMeshProUGUI scoreText;
    public int score;
    public int bonus;

    public GameObject revenueMain;
    public GameObject revenuePrefab;
    public List<GameObject> revenueComplete;

    public TextMeshProUGUI nameHamburguer;

    void Start()
    {
        //relogio = FindObjectOfType<Relogio>();
        //screenGameOver.SetActive(false);

        selectHamburguer();
        listReveune();
    }
    public void listReveune() {
        foreach (Ingredients item in hamburguerOrder.ingredientsHamburger)
        {
            GameObject revenue = Instantiate(revenuePrefab, revenueMain.transform.position, revenueMain.transform.rotation);
            revenue.GetComponent<Revenue>().SetInfo(item.spriteIngredients, "+1");
            revenue.transform.SetParent(revenueMain.transform);

            revenueComplete.Add(revenue);
        }
    }
    void Update()
    { 
        //if (relogio.minutoAtual == timer){
        //    gameOver();
        //}
    }
    public void clearRevenueList(){
        foreach (GameObject item in revenueComplete)
        {
            Destroy(item);
        }

        revenueComplete.Clear();
    }
    public void finalizeOrder(){

        bool isCorret = true;

        foreach (Ingredients item in hamburguerCurrent.ingredientsHamburger)
        {
            if (hamburguerOrder.ingredientsHamburger.Contains(item))
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
        hamburguerCurrent.ingredientsHamburger.Clear();

        clearRevenueList();
        selectHamburguer();
        listReveune();
    }
    public void selectHamburguer(){
        hamburguerOrder = hamburguers[Random.Range(0, hamburguers.Count)];
        nameHamburguer.text = hamburguerOrder.nomeHamburger;
    }
    public void gameOver() {
        screenGameOver.SetActive(true);
    }
    public void craftingHamburguer(Image s){
        foreach (Ingredients item in ingredients)
        {
            if (item.spriteIngredients == s.sprite) {
                hamburguerCurrent.ingredientsHamburger.Add(item);
            }
        }
    }
    public void usedBarbecue(){
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[0]);
    }
    public void usedMustard()
    {
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[1]);
    }
    public void usedPepper()
    {
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[2]);
    }
    public void usedWasabi()
    {
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[3]);
    }
    public void usedMayonnaise()
    {
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[4]);
    }
    public void usedKetchup()
    {
        hamburguerCurrent.ingredientsHamburger.Add(ingredients[5]);
    }
}
