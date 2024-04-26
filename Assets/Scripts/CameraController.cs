using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        // Primero, comprueba si hay toques disponibles.
        if (Input.touchCount > 0)
        {
            // Si hay toques disponibles, obtén las coordenadas del primer toque.
            Vector2 touchPosition = Input.GetTouch(0).position;

            // El resto del código para mover la cámara permanece igual.
            Vector3 moveDirection = new Vector3(touchPosition.x, 0, touchPosition.y);
            moveDirection.Normalize();
            _camera.transform.position += moveDirection * Time.deltaTime;
        }
    }
}
