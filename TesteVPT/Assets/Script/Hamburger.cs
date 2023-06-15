using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hamburger", menuName = "New Hamburger")]
public class Hamburger : ScriptableObject
{
    public string nomeHamburger;
    public Sprite spriteHamburger;
    public List<Ingredients> ingredientsHamburger;
}

