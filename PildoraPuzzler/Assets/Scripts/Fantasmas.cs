using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fantasmas : MonoBehaviour
{
    public Transform[] puntosDePatrulla; // Puntos de patrulla en el laberinto
    public Transform objetivoPacMan; // Referencia al objeto de Pac-Man
    public float distanciaPerseguir = 5.0f; // Distancia a la que el fantasma comienza a perseguir a Pac-Man
    public int puntoActual = 0;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Configura el primer punto de patrulla
        navMeshAgent.SetDestination(puntosDePatrulla[puntoActual].position);
        // Inicia la rutina de patrulla
        StartCoroutine(RutinaPatrulla());
    }

    void Update()
    {
        // Comprueba la distancia entre el fantasma y Pac-Man
        float distancia = Vector3.Distance(transform.position, objetivoPacMan.position);
        Debug.Log(distancia);

        if (distancia < distanciaPerseguir)
        {
            // Si Pac-Man está lo suficientemente cerca, persigue
            navMeshAgent.SetDestination(objetivoPacMan.position);
        }
        else
        {
            // Si no, continúa la rutina de patrulla
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                SiguientePuntoDePatrulla();

            }
        }
    }

    void SiguientePuntoDePatrulla()
    {
        // Cambia al siguiente punto de patrulla
        puntoActual = (puntoActual + 1) % puntosDePatrulla.Length;
        navMeshAgent.SetDestination(puntosDePatrulla[puntoActual].position);
        //Debug.Log("mi siguiente punto de patrulla es" + puntoActual);

    }

    IEnumerator RutinaPatrulla()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f); // Espera 5 segundos antes de cambiar de punto de patrulla
            SiguientePuntoDePatrulla();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaPerseguir);
    }
}
