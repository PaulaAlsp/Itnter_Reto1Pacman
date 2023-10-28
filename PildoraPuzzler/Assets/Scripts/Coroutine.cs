using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour

{
    public Vector3[] targetScales; 
    public Vector3[] targetPositions; 
    void Start()
    {
        StartCoroutine(Escala());
        StartCoroutine(CambiarPosicion());
        StartCoroutine(AsignarColor());
        
    }

    IEnumerator Escala()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        yield return new WaitUntil(() => players.Length >= 0); // Modificar valor segun la cantidad de datos en el array

        if (targetScales.Length < 3)
        {
            Debug.LogError("Debes asignar al menos 3 escalas.");
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(1f); // Esperar 1 segundo

            for (int i = 0; i < players.Length; i++)
            {
                GameObject player = players[i];

                Vector3 targetScale = targetScales[i];
                float duration = 1.0f;
                float elapsedTime = 0.0f;
                Vector3 startScale = player.transform.localScale;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / duration);
                    player.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                    yield return null;
                }
            }
        }
    }


    IEnumerator CambiarPosicion()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        yield return new WaitUntil(() => players.Length >= 0); // Modificar valor segun la cantidad de datos en el array

        if (targetPositions.Length < 3)
        {
            Debug.LogError("Debes asignar al menos 3 posiciones.");
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(1f); // Esperar 1 segundo

            for (int i = 0; i < players.Length; i++)
            {
                GameObject player = players[i];

                Vector3 targetPosition = targetPositions[i];
                float duration = 1.0f;
                float elapsedTime = 0.0f;
                Vector3 startPosition = player.transform.position;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / duration);
                    player.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                    yield return null;
                }
            }
        }
    }

    private IEnumerator AsignarColor()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        yield return new WaitUntil(() => Time.frameCount % 2800 == 0); //Esperar a llegar a este numero de frames 2800

        players[0].GetComponent<Renderer>().material.color = UnityEngine.Color.red;
        players[1].GetComponent<Renderer>().material.color = UnityEngine.Color.green;
        players[2].GetComponent<Renderer>().material.color = UnityEngine.Color.blue;
    }

}
