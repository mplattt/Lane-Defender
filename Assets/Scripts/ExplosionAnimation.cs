using System.Collections;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Boom1());
    }

    private IEnumerator Boom1()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
        yield return new WaitForSeconds(.15f);
        StartCoroutine(Boom2());
    }
    private IEnumerator Boom2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
        yield return new WaitForSeconds(.15f);
        StartCoroutine(Boom3());
    }
    private IEnumerator Boom3()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;
        yield return new WaitForSeconds(.15f);
        Destroy(gameObject);
    }
}
