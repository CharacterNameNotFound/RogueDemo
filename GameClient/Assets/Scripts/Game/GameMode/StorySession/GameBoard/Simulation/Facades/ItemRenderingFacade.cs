using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Facades
{
    public class ItemRenderingFacade : IItemRenderingFacade
    {
        private static readonly int UndrawnPart = Shader.PropertyToID("_UndrawnPart");

        public void UpdateCharge(ItemContainerComponent item, float charge)
        {
            item.ChargeProgressRenderer.material.SetFloat(UndrawnPart, charge);
        }
    }
}