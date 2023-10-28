using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Colicionar : MonoBehaviour
{
    public Animator pacmanAnimator;
    public MeshRenderer MR;
    public GameObject cere;

    private int puntos = 0;
    public TMP_Text puntaje;

    public Image panelWin;
    public Image panelLose;

    IEnumerator EsperarXSegundos(float tiempo) // Espera el tiempo especificado
    {
        yield return new WaitForSeconds(tiempo);
        actiPanel();
    }

    public void actiPanel()
    {
        Time.timeScale = 0f;
        if (puntaje.text == "GameOver")
        {
            panelWin.gameObject.SetActive(true);
        }
        else
        {
            panelLose.gameObject.SetActive(true);
        }
    }

    public void verCereza()
    {
        cere.SetActive(true);
    }

    public void adiosJugador()
    {
        MR.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fantasma"))
        {
            pacmanAnimator.SetBool("destruido", true); //Animation event (Adios Jugador)
            puntaje.text = "GameOver";
            StartCoroutine(EsperarXSegundos(1f));
        }
        else if (other.gameObject.CompareTag("Cerecita"))
        {
            cere.SetActive(false);
            puntaje.text = "WIN";
            StartCoroutine(EsperarXSegundos(0.5f));
        }
        else if (other.gameObject.CompareTag("izquierda"))
        {
            transform.localPosition = new Vector3(8, 1, -1);
        }
        
        else if (other.gameObject.CompareTag("derecha"))
        {
            transform.localPosition = new Vector3(-8, 1, -1);
        }
        else
        {
            puntos += 1;
            puntaje.text = "Score: " + puntos.ToString();
            Destroy(other.gameObject);
            if (puntos == 15)
            {
                verCereza();
            }
        }
    }
}
