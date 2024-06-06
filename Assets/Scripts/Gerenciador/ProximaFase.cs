using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximaFase : MonoBehaviour
{
    [SerializeField] int proximaFase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //SceneManager.LoadScene(proximaFase);
            Invoke("teleportDelay", 2f);
        }
    }
    void teleportDelay() 
    {
        SceneManager.LoadScene(proximaFase);
    }
}
