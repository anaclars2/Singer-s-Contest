using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CharacterSystem;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public class Fala
    {
        public string autor;
        [TextArea(2, 5)]
        public string texto;
    }

    public GameObject painelDialogo;
    public TextMeshProUGUI textoAutor;
    public TextMeshProUGUI textoDialogo;
    public Fala[] falas;

    private int indiceFala = 0;
    private bool emDialogo = false;
    private bool jogadorPorPerto = false;
    private bool jaConversou = false;



    void Start()
    {
       

        if (painelDialogo != null) painelDialogo.SetActive(false);
    }

    void Update()
    {
        if (jogadorPorPerto && Input.GetKeyDown(KeyCode.F) && !emDialogo && !jaConversou)
        {
            IniciarDialogo();
        }

        if (emDialogo && Input.GetKeyDown(KeyCode.Space))
        {
            ProximaFala();
        }
    }

    void IniciarDialogo()
    {
        painelDialogo.SetActive(true);
        emDialogo = true;
        indiceFala = 0;
        MostrarFala();

       
    }

    void ProximaFala()
    {
        indiceFala++;
        if (indiceFala < falas.Length)
        {
            MostrarFala();
        }
        else
        {
            painelDialogo.SetActive(false);
            emDialogo = false;
            jaConversou = true;

        }
    }

    void MostrarFala()
    {
        textoAutor.text = falas[indiceFala].autor;
        textoDialogo.text = falas[indiceFala].texto;
    }

   
}