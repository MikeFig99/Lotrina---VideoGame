using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource coinAudioSource;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private DynamicJoystick _joystickGiro;
    [SerializeField] private Rigidbody _rigidbody;  
    [SerializeField] private Animator _animator;

    public float movementSpeed = 8f;
    
    void Start()
    {
    }
    
    private int GetCurrentLayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f, 0 | 1);

        // Filtra las colisiones por capa, eligiendo la primera que coincida
        var filteredCollider = colliders.FirstOrDefault(c => c.gameObject.layer == 0 || c.gameObject.layer == 1);
        

        // Verifica si se encontró una colisión válida
        if (filteredCollider != null)
        {
            return filteredCollider.gameObject.layer;
        }
        else
        {
            return -1; // Indica que no se detectó ninguna colisión válida
        }
    }
    
    private void Update()
    {
        
        // Obtener la dirección del joystick
        Vector2 joystickInput = _joystick.Direction;

        // Calcular la dirección de movimiento
        Vector3 moveDirection = new Vector3(joystickInput.y, 0, joystickInput.x).normalized;
        Vector3 velocidadObjetivo = moveDirection.x * movementSpeed * transform.forward;

        // Aplica la velocidad objetivo mientras preserva la velocidad en el eje y
        _rigidbody.velocity = new Vector3(-velocidadObjetivo.x, _rigidbody.velocity.y, -velocidadObjetivo.z);
        
        transform.Rotate(0, _joystick.Horizontal , 0);
        transform.Rotate(0, _joystickGiro.Horizontal , 0);
        
        int currentLayer = GetCurrentLayer();
        
        // Animaciones
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (currentLayer == 0)
            {
                _animator.SetBool("Swimming", false);
                _animator.SetBool("Running", true);
            }
            else if (currentLayer == 1)
            {
                _animator.SetBool("Swimming", true);
                _animator.SetBool("Running", false);
            }

        }
        else
            // Activa la animación de estar parado.
            _animator.SetBool("Running", false);
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "CapsuleMapa")
        {
            coinAudioSource.Play();
        }
    }
}
