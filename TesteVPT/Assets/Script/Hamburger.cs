using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hamburguer", menuName = "New Hamburguer")]
public class Hamburguer : ScriptableObject
{
    public string nomeHamburguer;
    public Sprite spriteHamburguer;
    public List<Ingredients> ingredientsHamburguer;
}
