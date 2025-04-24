using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

    public class PlaceablePlatformManager : MonoBehaviour
    {
        public Transform player; // Asigna el jugador en el Inspector
        public Canvas canvasplatform; // Asigna el Canvas en el Inspector
        public List<Transform> platforms; // Lista de transformaciones de plataformas
        private Transform closestPlatform; // Almacena la plataforma más cercana
        public UpgradeTurrets turretUpgradesCanvas;
        public ScreenManager ScreenManager;
        

    
        public Renderer objetoRenderer;



    void Start()
    {
        ScreenManager.Hide(canvasplatform);
        InvokeRepeating(nameof(FindClosestPlatform), 0f, 0.1f); // Se ejecuta cada 0.1s
    }

    void Update()
    {
        

        // Activa el Canvas solo si hay una plataforma cercana y no está ocupada
        if (closestPlatform != null)
        {
            PlaceablePlatform placeable = closestPlatform.GetComponent<PlaceablePlatform>();
            

            if (placeable != null && !placeable.isOccupied)
            {

                ScreenManager.Show(canvasplatform);


            }
            else if (placeable.isOccupied)
            {

                ScreenManager.Hide(canvasplatform);
                
            }
           


            //si se puede mejorar
            if (placeable != null && placeable.canBeUpgraded)

            {
                DetectTurret(placeable.placedTurret.type);
            }
            else
            {
                turretUpgradesCanvas.DesactivateCanvases();
            }
        }
        else
        {
            ScreenManager.Hide(canvasplatform);
            turretUpgradesCanvas.DesactivateCanvases();
            
        }


    }

    public void DetectTurret(TurretType turretType)
        {
            switch (turretType)
            {
                case TurretType.NormalTurret:
                    turretUpgradesCanvas.DesactivateCanvases();
                    turretUpgradesCanvas.CanvasNormalTurret();
                    break;
                case TurretType.SlowTurret:
                turretUpgradesCanvas.DesactivateCanvases();
                turretUpgradesCanvas.CanvasIcelTurret();
                    break;
                case TurretType.Cannon:
                turretUpgradesCanvas.DesactivateCanvases();
                turretUpgradesCanvas.CanvasMachinegunlTurret();
                    break;
            }
        }

        private void FindClosestPlatform()
        {
            float closestDistance = Mathf.Infinity;
            closestPlatform = null; // Reinicia la referencia

            foreach (var platform in platforms)
            {
            PlaceablePlatform placeableCirculo = platform.GetComponent<PlaceablePlatform>();
            float distance = Vector3.Distance(player.position, platform.position);
                if (distance < closestDistance && distance < 3f) // Solo considera dentro del rango
                {
                
                placeableCirculo.circulo.gameObject.SetActive(true);
                closestDistance = distance;
                closestPlatform = platform;
            }
            else
            {
                placeableCirculo.circulo.gameObject.SetActive(false);
            }
            }
        }

        public Transform GetClosestPlatform()
        {
            return closestPlatform; // Retorna la plataforma más cercana
        }
}

