using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCell : MonoBehaviour
{
    public IEnumerator DestroyObjectDelayed()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.DecrementRedBloodCell();
        GameManager.instance.scoreText.text = "Score: " + GameManager.instance.Score;
        Destroy(gameObject);
    }
}
