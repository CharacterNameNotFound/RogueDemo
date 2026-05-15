using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;
using UnityEngine;
using Utils.UtilityTypes.Splines.CatmullRom;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingParticle
{
    public static class FlyingParticleParametersBuilder
    {
        public static FlyingParticleParameters BuildParameters(
            ItemContainerComponent origin,
            ItemContainerComponent destination, 
            FlyingParticleConfigs flyingParticleConfigs)
        {
            CatmullRomModel model = new CatmullRomModel();

            Vector3 vfxOrigin = origin.transform.position;
            Bounds originItemBounds = destination.ItemRenderer.bounds;
            

            Vector3 vfxDestination = destination.transform.position;
            Bounds destinationItemBounds = destination.ItemRenderer.bounds;

            
            Vector2[] points = new Vector2[4];
            points[0] = GetOutsidePoint(originItemBounds, flyingParticleConfigs);
            points[1] = vfxOrigin;
            points[2] = vfxDestination;
            points[3] = GetOutsidePoint(destinationItemBounds, flyingParticleConfigs);
            

            FlyingParticleParameters flyingParticleParameters = new FlyingParticleParameters();
            flyingParticleParameters.Duration = flyingParticleConfigs.FlyingParticlesDuration;
            flyingParticleParameters.CatmullRomModel = model;
            model.Points = points;

            return flyingParticleParameters;
        }

        // If this not looks good enough, swap to randomizing inside item sector, not item itself Catmull–Rom should guaranty smooth line anyway
        private static Vector2 GetOutsidePoint(Bounds itemBounds, FlyingParticleConfigs flyingParticleConfigs)
        {
            Vector2 destinationMod = Random.insideUnitCircle * flyingParticleConfigs.CurveRandomPointRadiusModifier;
            
            return new Vector3(itemBounds.extents.x * destinationMod.x, itemBounds.extents.y * destinationMod.y, 0);
        }
        

    }
}