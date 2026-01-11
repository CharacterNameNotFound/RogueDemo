using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Utils.UtilityTypes.AssetReferencing
{
    public static class AssetReferenceDtoUtilExtensions
    {
        public static async UniTask<T> Load<T>(this AssetReferenceDto assetReferenceDto, CancellationToken cancellationToken)
        {
            AsyncOperationHandle<T> operation = Addressables.LoadAssetAsync<T>(assetReferenceDto.RuntimeKey);
            await operation.WithCancellation(cancellationToken);
            return operation.Result;
        }

        public static async UniTask<T> Instantiate<T>(this AssetReferenceDto assetReferenceDto, InstantiationParameters instantiationParameters, CancellationToken cancellationToken)
        {
            GameObject gameObject = await Instantiate(assetReferenceDto, instantiationParameters, cancellationToken);
            return gameObject.GetComponent<T>();
        }
        
        public static async UniTask<GameObject> Instantiate(this AssetReferenceDto assetReferenceDto, InstantiationParameters instantiationParameters, CancellationToken cancellationToken)
        {
            AsyncOperationHandle operation = Addressables.InstantiateAsync(assetReferenceDto.RuntimeKey, instantiationParameters);
            await operation.WithCancellation(cancellationToken);
            return (GameObject) operation.Result;
        }

        public static async UniTask<SceneInstance> LoadScene(this AssetReferenceDto assetReferenceDto, LoadSceneMode loadSceneMode, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, CancellationToken cancellationToken = default)
        {
            AsyncOperationHandle<SceneInstance> sceneLoading = Addressables.LoadSceneAsync(assetReferenceDto.RuntimeKey, loadSceneMode, releaseMode);
            
            await sceneLoading.Task.AsUniTask().AttachExternalCancellation(cancellationToken);
            return sceneLoading.Result;
        }
        
    }
}