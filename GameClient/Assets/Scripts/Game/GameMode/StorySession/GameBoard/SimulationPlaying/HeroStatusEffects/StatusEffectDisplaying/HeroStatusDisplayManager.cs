using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying
{
    public class HeroStatusDisplayManager : IHeroStatusDisplayManager
    {
        private StatusEffectIconPool _effectIconPool;

        private HeroStatusEffectDisplayingConfigs _effectDisplayingConfigs;
        private IPooledObjectHostProvider _hostProvider;

        // ToDo create status effect data provider
        private Dictionary<Type, Sprite> _sprites;

        public HeroStatusDisplayManager(HeroStatusEffectDisplayingConfigs effectDisplayingConfigs, IPooledObjectHostProvider hostProvider)
        {
            _effectDisplayingConfigs = effectDisplayingConfigs;
            _hostProvider = hostProvider;
        }
        
        public async UniTask Initialize(HeroStatusEffectDisplay playerDisplay, HeroStatusEffectDisplay encounterDisplay, CancellationToken cancellationToken)
        {
            _sprites = new Dictionary<Type, Sprite>();
            
            Sprite icon = await _effectDisplayingConfigs.BurnIcon.LoadAssetAsync<Sprite>();
            _sprites.Add(typeof(BurnHeroStatusEffect), icon);
            
            icon = await _effectDisplayingConfigs.RegenerationIcon.LoadAssetAsync<Sprite>();
            _sprites.Add(typeof(RegenerationHeroStatusEffect), icon);
            
            icon = await _effectDisplayingConfigs.PoisonIcon.LoadAssetAsync<Sprite>();
            _sprites.Add(typeof(PoisonHeroStatusEffect), icon);

            InitializeStatusDisplays(icon, playerDisplay);
            InitializeStatusDisplays(icon, encounterDisplay);

            _effectIconPool = new(
                new List<HeroStatusEffectIcon>(_effectDisplayingConfigs.PreSpawnedCount), 
                new AddressablePoolEntityProvider<HeroStatusEffectIcon>(_effectDisplayingConfigs.StatusEffectIcon), 
                _hostProvider);

            await _effectIconPool.ExtendBy(_effectDisplayingConfigs.PreSpawnedCount, cancellationToken);
        }

        public async UniTask AddEffectIcon<T>(string text, HeroStatusEffectDisplay heroStatusEffectDisplay, CancellationToken cancellationToken) where T: IHeroStatusEffect
        {
            HeroStatusEffectIcon heroStatusIcon = await _effectIconPool.GetObject(cancellationToken);

            Sprite sprite = _sprites[typeof(T)];

            heroStatusIcon.Initialize(sprite, text, typeof(T));
            
            heroStatusIcon.transform.SetParent(heroStatusEffectDisplay.transform);
            heroStatusEffectDisplay.StatusEffectIcons.Add(heroStatusIcon);
            heroStatusEffectDisplay.UpdateLine();
            
            heroStatusIcon.gameObject.SetActive(true);
        }

        public async UniTask UpdateEffectIcon<T>(string text, float progressPercentile, HeroStatusEffectDisplay heroStatusEffectDisplay, CancellationToken cancellationToken) where T: IHeroStatusEffect
        {
            HeroStatusEffectIcon heroStatusEffectIcon;
            
            do
            {
                heroStatusEffectIcon = heroStatusEffectDisplay.StatusEffectIcons.FirstOrDefault(item => item.StatusEffect == typeof(T));
                await UniTask.NextFrame(cancellationToken);
            }
            while (heroStatusEffectIcon is null) ;
            
            heroStatusEffectIcon.UpdateProgress(text, progressPercentile);
        }

        public async UniTask UpdateEffectIcon<T>(
            float percentile, 
            HeroStatusEffectDisplay heroStatusEffectDisplay,
            CancellationToken cancellationToken) where T : IHeroStatusEffect
        {
            HeroStatusEffectIcon heroStatusEffectIcon;
            
            do
            {
                heroStatusEffectIcon = heroStatusEffectDisplay.StatusEffectIcons.FirstOrDefault(item => item.StatusEffect == typeof(T));
                await UniTask.NextFrame(cancellationToken);
            }
            while (heroStatusEffectIcon is null) ;
            
            heroStatusEffectIcon.UpdateProgress(percentile);
        }

        public async UniTask UpdateEffectIcon<T>(string text, HeroStatusEffectDisplay heroStatusEffectDisplay,
            CancellationToken cancellationToken)
        {
            HeroStatusEffectIcon heroStatusEffectIcon;
            
            do
            {
                heroStatusEffectIcon = heroStatusEffectDisplay.StatusEffectIcons.FirstOrDefault(item => item.StatusEffect == typeof(T));
                await UniTask.NextFrame(cancellationToken);
            }
            while (heroStatusEffectIcon is null) ;
            
            heroStatusEffectIcon.UpdateProgress(text);
        }

        public async UniTask RemoveItemEffectIcon<T>(HeroStatusEffectDisplay heroStatusEffectDisplay, CancellationToken cancellationToken) where T: IHeroStatusEffect
        {
            HeroStatusEffectIcon heroStatusEffectIcon;

            do
            {
                heroStatusEffectIcon = heroStatusEffectDisplay.StatusEffectIcons.FirstOrDefault(item => item.StatusEffect == typeof(T));
                await UniTask.NextFrame(cancellationToken);
            } while (heroStatusEffectIcon is null);

            _effectIconPool.ReturnToPool(heroStatusEffectIcon);
            
            heroStatusEffectDisplay.StatusEffectIcons.Remove(heroStatusEffectIcon);
            
            heroStatusEffectDisplay.UpdateLine();
        }

        public void Clear()
        {
            foreach (Sprite item in _sprites.Values)
            {
                Addressables.Release(item);
            }
            
            _effectIconPool.ReleaseAll();
            _effectIconPool = null;
        }


        private void InitializeStatusDisplays(Sprite icon, HeroStatusEffectDisplay display)
        {
            float iconSizeX = icon.bounds.size.x;
            float minX = display.DisplayRenderer.bounds.min.x;

            Vector3 step = new Vector3(iconSizeX, 0, 0);
            Vector3 start = new Vector3(minX + (iconSizeX / 2), 0, 0);
            
            display.Initialize(start, step);
        }
        
    }
}