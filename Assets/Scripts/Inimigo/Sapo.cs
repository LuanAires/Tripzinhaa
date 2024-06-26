using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapo : MonoBehaviour
{
    public Transform pontoOrigem;
    public GameObject Heroi;
    public Animator Animador;

    public GameObject dropPrefab;
    public GameObject projetilPrefab;
    public float velocidadeProjetil = 5f;
    private bool heroiDentroRaio = false;
    private float maxcooldown = 2;
    private float contadortiro;
    public int dano;
    public int hp = 1;
    private int hpMax;
    private HpBarraInimigo hpbarr;



    private void Start()
    {
        hpMax = hp;
        Heroi = GameObject.FindGameObjectWithTag("Player");
        Animador = GetComponentInChildren<Animator>();
        hpbarr = GetComponentInChildren<HpBarraInimigo>();

        if (hpbarr != null)
        {
            hpbarr.maxlife = hpMax;
            hpbarr.currentylife = hp;
        }
    }
    private void Update()
    {
        if (hp <= 0)
        {
            Morrer();
        }
        else
        {
            if (heroiDentroRaio && contadortiro >= maxcooldown)
            {
                StartCoroutine(LançarProjetil());
                contadortiro = 0;
            }

            if (contadortiro <= maxcooldown)
            {
                contadortiro += Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            heroiDentroRaio = true;
        }
        if (other.gameObject.tag == "Atk")
        {
            AplicarDano(10);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            heroiDentroRaio = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Atk") 
        {
            AplicarDano(10);
            Destroy(collision.gameObject);
        }
    }

    public void Morrer()
    {
        Animador.SetBool("Morrendo", true);
        DroparAlma();
        Destroy(gameObject);
    }
    void DroparAlma()
    {
        Instantiate(dropPrefab, transform.position, Quaternion.identity);
    }
    public void AplicarDano(int dano)
    {
        hp -= dano;
        if (hp < 0) hp = 0;

        Animador.SetTrigger("Dano");

        if (hpbarr != null)
        {
            hpbarr.currentylife = hp;
        }
    }
    private IEnumerator LançarProjetil()
    {
        Animador.SetBool("Cuspe", true);
        yield return new WaitForSeconds(Animador.GetCurrentAnimatorStateInfo(0).length);      
    }
    public void MeuTiro()
    {
        if (projetilPrefab != null)
        {
            GameObject projetil = Instantiate(projetilPrefab, pontoOrigem.position, Quaternion.identity);

            Vector2 direcao = (Heroi.transform.position - transform.position).normalized;

            Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direcao * velocidadeProjetil;
            }
            Destroy(projetil, 2f);
        }
    }
}
