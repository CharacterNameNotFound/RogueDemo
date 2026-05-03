using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.UI
{
    public class StorySessionScreenController : UIScreen<IScreenParams, StorySessionScreenDependencies>
    {
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;

        // boss view, ToDo: maybe remake into pooled pop-up?
        [SerializeField] private GameObject _bossScreenHost;
        [SerializeField] private List<Image> _bossImages;
        [SerializeField] private Button _hideBossScreen;

        public override async UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask result = await base.OnBeforeOpen(screenParams, cancellationToken);

            _hideBossScreen.onClick.AddListener(() => _bossScreenHost.SetActive(false));
            
            return result;
        }

        public async UniTask SetBossImages(IEnumerable<BattleEncounter> bossEncounters, CancellationToken cancellationToken)
        {
            List<UniTask<Sprite>> bossSprites = new List<UniTask<Sprite>>();
            
            foreach (BattleEncounter item in bossEncounters)
            {
                bossSprites.Add(item.VerticalPortrait.Load<Sprite>(cancellationToken));
            }

            Sprite[] sprites = await bossSprites;
            cancellationToken.ThrowIfCancellationRequested();

            for (int i = 0; i < _bossImages.Count; i++)
            {
                _bossImages[i].sprite = sprites[i];
                _bossImages[i].transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            
            _hideBossScreen.gameObject.SetActive(false);
            
        }

        public async UniTask PlayBossIntro(CancellationToken cancellationToken)
        {
            for (int i = 0; i < _bossImages.Count; i++)
            {
                _bossImages[i].transform.DORotate(Vector3.zero, 0.5f).Play().ToUniTask(cancellationToken: cancellationToken).Forget();
                await UniTask.WaitForSeconds(0.2f, cancellationToken: cancellationToken);
            }
            
            await UniTask.WaitForSeconds(0.5f, cancellationToken: cancellationToken);
            
            _hideBossScreen.gameObject.SetActive(true);
        }

        public override void OnAfterClose()
        {
            base.OnAfterClose();

            _hideBossScreen.onClick.RemoveAllListeners();
            
            foreach (Image item in _bossImages)
            {
                Addressables.Release(item.sprite);
            }
            
        }
    }
}