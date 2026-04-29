using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection;
using Game.GameMode.StorySession.Utilities.WorldInteractables;
using GameWideSystems.InputManager;

namespace Game.GameMode.StorySession.StoryLoop.Services.InputControl
{
    public class InputLayerControlMediator : IInputLayerControlMediator
    {
        private IInputHost _inputHost;
        private StorySessionEncounterSelectionInputLayer _encounterLayer;
        private WorldInteractableInputLayer _worldInteractableInputLayer;
        private ItemManipulationInputLayer _itemManipulationInputLayer;
        private ItemDetailsInputLayers _itemDetailsInputLayers;
        
        

        public InputLayerControlMediator(
            IInputHost inputHost, 
            StorySessionEncounterSelectionInputLayer encounterLayer, 
            WorldInteractableInputLayer worldInteractableInputLayer, 
            ItemManipulationInputLayer itemManipulationInputLayer, 
            ItemDetailsInputLayers itemDetailsInputLayers)
        {
            _inputHost = inputHost;
            _encounterLayer = encounterLayer;
            _worldInteractableInputLayer = worldInteractableInputLayer;
            _itemManipulationInputLayer = itemManipulationInputLayer;
            _itemDetailsInputLayers = itemDetailsInputLayers;
        }


        public UniTask Initialize(CancellationToken cancellationToken)
        {
            // Encounter
            _inputHost.AddInputLayer(_encounterLayer);
            _encounterLayer.SetActive(false);
            
            // Item Movement
            _inputHost.AddInputLayer(_itemManipulationInputLayer);
            
            //item details
            _inputHost.AddInputLayer(_itemDetailsInputLayers);
            _itemDetailsInputLayers.SetActive(true);
            
            // World interaction
            _worldInteractableInputLayer.SetActive(true);
            
            return UniTask.CompletedTask;
        }

        public void ToggleItemMovement(bool isActive)
        {
            _itemManipulationInputLayer.SetActive(isActive);
        }

        public void ToggleDetails(bool isActive)
        {
            _itemDetailsInputLayers.SetActive(isActive);
        }

        public void ToggleEncounter(bool isActive)
        {
            _encounterLayer.SetActive(isActive);
        }

        public void ToggleWorldInteractables(bool isActive)
        {
            _worldInteractableInputLayer.SetActive(isActive);
        }


        public UniTask CleanUp(CancellationToken cancellationToken)
        {
            _inputHost.RemoveInputLayer(_encounterLayer);
            _inputHost.RemoveInputLayer(_itemManipulationInputLayer);
            _inputHost.RemoveInputLayer(_itemDetailsInputLayers);
            
            _encounterLayer.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}