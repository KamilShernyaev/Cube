using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour,IInteractable
{
    // public GameObject chestUI; // UI для отображения содержимого сундука
    // public Transform playerTransform; // позиция игрока
    public GameObject[] lootPrefabs; // массив предметов, которые могут выпадать из сундука
    public float lootDropDistance = 2f; // расстояние, на котором предметы будут выпадать от сундука

    // private bool isInRange; // флаг, находится ли игрок рядом с сундуком
    public bool isOpen = false; // флаг, открыт ли сундук

    private void OpenChest()
    {
        isOpen = true;
        // chestUI.SetActive(true); // показываем UI для отображения содержимого сундука
        Vector3 chestPosition = transform.position;
        chestPosition.y += 2f; // поднимаем UI, чтобы он был выше сундука
        // chestUI.transform.position = chestPosition;

        // Выпадение предметов
        foreach (GameObject lootPrefab in lootPrefabs)
        {
            if (Random.value < lootPrefab.GetComponent<Item>().dropRate)
            {
                Vector3 lootPosition = transform.position + Random.insideUnitSphere * lootDropDistance;
                Instantiate(lootPrefab, lootPosition, Quaternion.identity);
            }
        }
    }

    // private void CloseChest()
    // {
    //     isOpen = false;
    //     // chestUI.SetActive(false); // скрываем UI
    // }

    public void Interact(Transform interactorTransform)
    {
        if (!isOpen)
            {
                OpenChest();
            }
            // else
            // {
            //     CloseChest();
            // }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}