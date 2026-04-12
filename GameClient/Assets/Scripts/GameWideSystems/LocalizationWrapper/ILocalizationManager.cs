using Cysharp.Threading.Tasks;

namespace GameWideSystems.LocalizationWrapper
{
    public interface ILocalizationManager
    {
        public UniTask Initialize();
        
        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine);
        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine);

        public bool TryGetLocalized(LocalizedLineKey key, object replacements, out string localizedLine);
        public bool TryGetLocalized(string key, TranslationCategory category, object replacements, out string localizedLine);
    }
}