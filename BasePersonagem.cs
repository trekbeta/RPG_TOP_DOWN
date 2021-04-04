using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class BasePersonagem : MonoBehaviour {
 
    private float vel;
    protected Vector3 direcao;//vector2 old
    private Rigidbody heroiRB;//ajuste
    private Animator anim;
    protected Vector3 direcaoHeroi;//vector2 old
    private bool liberaCor = false;
    private bool danoCritico = false;
    private SpriteRenderer heroiR;
    protected CircleCollider2D ataqueEfeito;
 
    // Use this for initialization
    protected virtual void Start ()
    {
        vel = 5;
        direcao = Vector3.zero;//vector2 old
        heroiRB = GetComponent<Rigidbody>();//ajustado
        anim = GetComponent<Animator>();
 
        heroiR = GetComponent<SpriteRenderer>();
        //ataqueEfeito = transform.GetChild(0).GetComponent<CircleCollider2D>();
        AtaqueEnab();
        
    }
    
    // Update is called once per frame
    protected virtual void Update ()
    {
        if (direcao.x != 0 || direcao.z != 0)//direcao.y old
        {
            Animacao(direcao);
 
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
 
        if (liberaCor == true)
        {
            PingPongColor(8);
        }
 
        if (danoCritico == true)
        {
            PingPongColor(1);
        }
    }
 
    public void PingPongColor(int x)
    {
        heroiR.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(x * Time.time, 0.5f));
    }
 
    protected virtual void FixedUpdate()
    {
        Mover();
    }
 
    protected void Mover()
    {
        heroiRB.MovePosition(heroiRB.transform.position + direcao * vel * Time.deltaTime);//ajustado
    }
 
 
    public void Animacao(Vector3 dir)//ajustado
    {
        anim.SetLayerWeight(1, 1);
 
        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.z);//ajustado
    }
 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("morte"))
        {
            StartCoroutine(KnockBack(1f, 50, direcaoHeroi));
            DanoCor();
        }
    }
 
    public IEnumerator KnockBack(float duracao, float poder, Vector3 direcao)//ajustado
    {
        float tempo = 0;
 
        while (duracao > tempo)
        {
            tempo += Time.deltaTime;
            heroiRB.AddForce(new Vector3(direcao.x * -poder, direcao.z * -poder),ForceMode.Force);//ajustado
 
        }
 
        yield return 0;
    }
 
    void DanoCor()
    {
        liberaCor = true;
        StartCoroutine(LiberaCor());
    }
 
    IEnumerator LiberaCor()
    {
        yield return new WaitForSeconds(0.5f);
        liberaCor = false;
        heroiR.color = new Color(1, 1, 1, 1);
    }
 
    public void AnimacaoAtaque()
    {
        anim.SetTrigger("ataque");
        //AtaqueEnab();
    }
 
    public void AtaqueEnab()
    {
        //ataqueEfeito.enabled = false;
    }
 
 
}