using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public int health = 3;
    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;


    private void Update()
    {
        foreach (Image img in hearts)
            {
                img.sprite = emptyHeart;
            }

            for (int i = 0; i < health; i++)
            {
                hearts[i].sprite = fullHeart;
            }   
    }
}
