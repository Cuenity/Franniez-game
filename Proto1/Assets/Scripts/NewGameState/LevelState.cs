using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.NewGameState
{
    class LevelState : MonoBehaviour
    {
        // Serialize deze dingen nog
        public RollingPhaseManager rollingPhaseManager;
        public BuildingPhaseManager buildingPhaseManager;

        public PlayerManager playerManager;
        public PlayerBallManager playerBallManager;
        public PlayerCamera playerCamera;

        public PlatformManager platformManager;
        public CollectableManager collectableManager;
        public GridManager gridManager;
        public ButtonManager buttonManager;

        // Privates
        private bool buildingPhaseActive = true;
        private bool rollingPhaseActive = false;

        public enum GamePhase
        {
            rollingPhase,
            buildingPhase
        }

        private void Awake()
        {
            //rollingPhaseManager = Instantiate(rollingPhaseManager, instance.transform);
            //rollingPhaseManager.transform.parent = this.transform;

            //buildingPhaseManager = Instantiate(buildingPhaseManager, instance.transform);
            //buildingPhaseManager.transform.parent = this.transform;

            //playerManager = Instantiate(playerManager, instance.transform);
            //playerManager.transform.parent = this.transform;

            //playerBallManager = Instantiate(playerBallManager, instance.transform);
            //playerBallManager.transform.parent = this.transform;

            //playerCamera = Instantiate(playerCamera, instance.transform);
            //playerCamera.transform.parent = this.transform;

            //platformManager = Instantiate(platformManager, instance.transform);
            //platformManager.transform.parent = this.transform;

            //collectableManager = Instantiate(collectableManager, instance.transform);
            //collectableManager.transform.parent = this.transform;
            
            //gridManager = Instantiate(gridManager, instance.transform);
            //gridManager.transform.parent = this.transform;

            //buttonManager = Instantiate(buttonManager, instance.transform);
            //buttonManager.transform.parent = this.transform;

        }
    }
}
