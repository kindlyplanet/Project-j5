using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] private Transform doorModel;
    [SerializeField] private GameObject colObject;
    [SerializeField] private float openSpeed;

    private bool isOpening;
    private bool isClosing;
    private Vector3 openPosition;
    private Vector3 closedPosition;

    private void Start()
    {
        openPosition = new Vector3(doorModel.position.x, doorModel.position.y, 1f);
        closedPosition = doorModel.position;

        // Suscribirse al evento de muerte del jefe
        Boss.OnBossDeath += OpenDoor;
    }

    public void OpenDoor()
    {
        if (isClosing)
            return; // Evitar abrir si se está cerrando.

        if (!isOpening)
        {
            isOpening = true;
            AudioController.instance.PlaySFX("dooropen");
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        while (doorModel.position != openPosition)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, openPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        colObject.SetActive(false);
        isOpening = false;
    }

    // Asegúrate de desuscribirte del evento al destruir el objeto
    private void OnDestroy()
    {
        Boss.OnBossDeath -= OpenDoor;
    }
}
