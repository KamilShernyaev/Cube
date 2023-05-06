using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplayableCharacter : MonoBehaviour
{
    public CharacterController[] characters;
    public bool isPlayable = true;
    private Rigidbody rb;
    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update() 
    {
        FollowPlayer();
    }

    private IEnumerator FollowPlayer()
    {
        int targetIndex = Random.Range(0, characters.Length);
        CharacterController targetPlayer = characters[targetIndex];
        
        while (!isPlayable) // пока персонаж не-играбельный
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, Time.deltaTime * 10);

        // Поворачиваем персонажа в сторону целевого игрока
            transform.LookAt(targetPlayer.transform);

            // Если персонаж достиг целевого игрока
            if (transform.position == targetPlayer.transform.position)
            {
                // Выбираем нового случайного игрока для следования
                targetIndex = Random.Range(0, characters.Length);
                targetPlayer = characters[targetIndex];
            }

            yield return null;
        }
    }
}
