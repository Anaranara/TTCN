using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    [SerializeField] private GameObject terr;
    private GameManager mn;

    private void Start()
    {
        mn = GameManager.Instance;
    }
    void Update()
    {
        if (mn.isplay)
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(8f);
        Destroy(terr);
    }

}
