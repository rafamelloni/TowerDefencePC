using UnityEngine;

public class OrbitRobot : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 3f;   // Distancia de la órbita
    public float orbitSpeed = 2f;    // Velocidad de la órbita
    public float floatAmplitude = 0.5f; // Amplitud del movimiento vertical
    public float floatSpeed = 2f;    // Velocidad del movimiento vertical

    private float angle;
    private float initialY;

    public float offset = 3f;

    private bool isStopped = false;  // Indicador de si el movimiento ha sido detenido

    public RobotTargeting targeting;

    void Start()
    {
        // Guardar la posición Y inicial del objeto
        initialY = transform.position.y;
    }

    void Update()
    {
        if (player == null || isStopped) return;

        // Incrementamos el ángulo para la órbita
        angle += orbitSpeed * Time.deltaTime;

        // Calculamos la nueva posición en la órbita
        float x = player.position.x + Mathf.Cos(angle) * orbitRadius;
        float z = player.position.z + Mathf.Sin(angle) * orbitRadius;

        // Movimiento de flotación en el eje Y con un efecto de onda senoidal
        float y = initialY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Aplicamos la posición con el movimiento vertical
        transform.position = new Vector3(x, y + offset, z);

        // Hacer que el robot mire siempre hacia el jugador
        if(!targeting.hasEnemiesInRange) transform.LookAt(player);

    }


}
