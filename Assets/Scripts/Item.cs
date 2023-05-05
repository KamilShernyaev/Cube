using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // имя предмета
    public string description; // описание предмета
    public Sprite icon; // иконка предмета, которая будет отображаться в UI
    public float dropRate; // вероятность выпадения предмета из сундука
    public int level; // уровень предмета
    public int value; // стоимость предмета
}