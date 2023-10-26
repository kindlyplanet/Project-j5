using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private Transform doorModel;
    [SerializeField] private GameObject colObject;
    [SerializeField] private float openSpeed;

    private bool isOpening;
    private bool isClosing;
    private Vector3 openPosition;
    private Vector3 closedPosition;

    void Start()
    {
        openPosition = new Vector3(doorModel.position.x, doorModel.position.y, 1f);
        closedPosition = doorModel.position;
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
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

    public void CloseDoor()
    {
        if (isOpening)
            return; // Evitar cerrar si se está abriendo.

        if (!isClosing)
        {
            isClosing = true;
            StartCoroutine(CloseDoorCoroutine());
        }
    }

    private IEnumerator CloseDoorCoroutine()
    {
        while (doorModel.position != closedPosition)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, closedPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        colObject.SetActive(true);
        isClosing = false;
    }
}
