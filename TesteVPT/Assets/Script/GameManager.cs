using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Hamburger> hamburguers;
    public List<Ingredients> ingredients;

    public List<Ingredients> hamburguerCurrent;
    public Hamburger hamburguerOrder;

    public List<Image> ingredientSlots;
    public List<Image> sauceSlots;
    public List<Ingredients> sauces;

    public Image hamburguerSlot;

    private Relogio relogio;

    public GameObject screenGameOver;
    public GameObject screenMenu;
    public GameObject screenKitchen; 

    [Header("Bonus")]
    public TextMeshProUGUI bonusText;
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI scoreMenu;
    public int bonus = 1;
    [Space]

    public GameObject recipeMain;
    public GameObject recipePrefab;
    public List<GameObject> recipeComplete;
    public TextMeshProUGUI nameHamburguer;

    public int bonusControll;

    public List<int> listNumbers = new List<int>();
    int number;
    public Sprite hamburgerSprite;
    public Hamburger hamburgerEmpty;

    void Start()
    {
        screenKitchen.SetActive(false);
        screenKitchen.SetActive(false);
        screenGameOver.SetActive(false);
        screenMenu.SetActive(true);

    }
    public void listRecipe() {
        foreach (Ingredients item in hamburguerOrder.ingredientsHamburger)
        {
            GameObject recipe = Instantiate(recipePrefab, recipeMain.transform.position, recipeMain.transform.rotation);
            recipe.GetComponent<Recipe>().SetInfo(item.spriteIngredients, "+1");
            recipe.transform.SetParent(recipeMain.transform);

            recipeComplete.Add(recipe);
        }
    }
    void Update()
    {
        if (relogio != null) {
            if (relogio.horaAtual >= 2)
            {
                openGameOver();
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                menu();
            }
        }
    }
    public Hamburger hamburgerRandom(){

        hamburgerEmpty.spriteHamburger = hamburgerSprite;
        hamburgerEmpty.nomeHamburger = "X-Cliente";

        hamburgerEmpty.ingredientsHamburger.Clear();
        hamburgerEmpty.ingredientsHamburger.Add(ingredients[Random.Range(0,2)]);

        for (int i = 1; i < 4; i++)
        {
            hamburgerEmpty.ingredientsHamburger.Add(ingredients[Random.Range(2, ingredients.Count)]);
        }
        hamburgerEmpty.ingredientsHamburger.Add(sauces[Random.Range(0, sauces.Count)]);


        return hamburgerEmpty;
    }
    public void initGame(){

        screenKitchen.SetActive(true);
        relogio = FindObjectOfType<Relogio>();
        screenGameOver.SetActive(false);
        screenMenu.SetActive(false);

        relogio.velocidadeDoRelogio = 1;
        relogio.horaAtual = 0;
        relogio.minutoAtual = 0;

        selectHamburguer();
        listRecipe();
        generateNumber();
        organizeIngredients();
    }
    
    public void openGameOver(){

        relogio.velocidadeDoRelogio = 0;

        screenGameOver.SetActive(true);

        hamburguerCurrent.Clear();
        listNumbers.Clear();

        clearRecipeList();

        scoreMenu.text = "Score " + score;
 
        bonusControll = 0;
        bonus = 1;
    }
    public void menu() {

        clearRecipeList();
        screenKitchen.SetActive(false);
        screenMenu.SetActive(true);    
    }
    public void reloadGame() {

        screenGameOver.SetActive(false);
        relogio = FindObjectOfType<Relogio>();

        relogio.velocidadeDoRelogio = 1;
        relogio.horaAtual = 0;
        relogio.minutoAtual = 0;

        selectHamburguer();
        listRecipe();
        generateNumber();
        organizeIngredients();
    }
    public void clearRecipeList(){
        foreach (GameObject item in recipeComplete)
        {
            Destroy(item);
        }

        recipeComplete.Clear();
    }
    public void finalizeOrder(){
        bool isCorret = true;
        if (hamburguerCurrent.Count < 5 || hamburguerCurrent.Count > 5)
        {
            isCorret = false;
        }
        else
        {
            foreach (Ingredients item in hamburguerCurrent)
            {
                int i = 0;
                if (hamburguerOrder.ingredientsHamburger.Contains(item))
                {
                    isCorret = true;
                }
                else
                {
                    isCorret = false;
                    break;
                }
                i++;
            }
        }
        if (isCorret)
        {
            if (bonusControll == 5) {
                bonusControll = 0;
                bonus += 1; 
            }
            bonusControll += 1;
            score += (5 * bonus);
        }
        else {

            bonusControll = 0;
            bonus = 1;
            score -= 5;
        }

        bonusText.text = "X" + bonus.ToString();
        scoreText.text = score.ToString();

        hamburguerCurrent.Clear();
        listNumbers.Clear();

        clearRecipeList();
        selectHamburguer();
        listRecipe();
        generateNumber();
        organizeIngredients();
    }
    public void selectHamburguer(){
        int i = Random.Range(0,2);
        if (i == 0)
        {
            hamburguerOrder = hamburguers[Random.Range(0, hamburguers.Count)];
            nameHamburguer.text = hamburguerOrder.nomeHamburger;
        }
        else {
            Hamburger n = hamburgerRandom();

            hamburguerOrder = n;
            nameHamburguer.text = n.nomeHamburger;
        }
    }
    
    public void generateNumber()
    {
        for (int i = 0; i < 5; i++)
        {
            do
            {
                number = Random.Range(0, ingredientSlots.Count);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
        }
    }
    public void organizeIngredients() {
        foreach (Image i in ingredientSlots) {
            i.sprite = null;
        }
        foreach (Ingredients item in hamburguerOrder.ingredientsHamburger)
        {
            if (!sauces.Contains(item)) {
                int i = Random.Range(0, listNumbers.Count);
                ingredientSlots[listNumbers[i]].sprite = item.spriteIngredients;
                listNumbers.Remove(listNumbers[i]);
            }
        }
        completeList();
    }
    public void completeList(){
        foreach (Image item in ingredientSlots)
        {
            if (item.sprite == null)
            {
                int i = Random.Range(0, ingredients.Count);
                if (!hamburguerOrder.ingredientsHamburger.Contains(ingredients[i]))
                {
                    item.sprite = ingredients[i].spriteIngredients;
                }
                else {
                    completeList();
                }
                
            }
        }
    }
    public void craftingHamburguer(Image s){
        foreach (Ingredients item in ingredients)
        {
            if (item.spriteIngredients == s.sprite) {
                hamburguerCurrent.Add(item);
            }
        }
        foreach (Ingredients item in sauces)
        {
            if (item.spriteIngredients == s.sprite)
            {
                hamburguerCurrent.Add(item);
            }
        }
    }
}
