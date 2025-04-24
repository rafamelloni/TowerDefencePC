using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour, IEPickUp
{
    private Vector3 _initialPos;

    // Variable privada
    private bool _posicionada = false;

    TurretRayCast rayCastTorre;

    // Propiedad pública
    public bool Posicionada
    {
        get { return _posicionada; }
        protected set { _posicionada = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialPos = transform.position;
        rayCastTorre = GetComponent<TurretRayCast>();
    }

    public void OnPickUp(Vector3 position)
    {
        if (!Posicionada)
        {
            // Clampeo de la posición en el eje Y
            float clampedY = Mathf.Clamp(position.y, _initialPos.y, _initialPos.y + 5f);

            // Actualiza la posición del objeto en X y Z para que siga al mouse
            Vector3 clampedPosition = new Vector3(position.x, clampedY, position.z);

            // Mueve el objeto a la posición clamped
            transform.position = clampedPosition;
        }
    }

    public void OnPickDown(Vector3 position)
    {
        Vector3 newPos = transform.position;
        newPos.y = _initialPos.y;
        transform.position = newPos;
        Posicionada = true; // Utiliza la propiedad
    }

    public void MoveObject(Vector3 pos, RaycastHit hit)
    {
        OnPickUp(pos);
    }

    public void ReleaseObject(Vector3 pos, RaycastHit hit)
    {
        if (rayCastTorre.puedePosicionarseAca) OnPickDown(pos);
    }
}
