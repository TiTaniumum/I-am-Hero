using System.Globalization;
using System.Resources;

namespace I_am_Hero_WPF.Services
{
    public class LocalizationService
    {
        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public LocalizationService()
        {
            _resourceManager = new ResourceManager("I_am_Hero_WPF.Resources.Strings", typeof(LocalizationService).Assembly);
            _currentCulture = CultureInfo.CurrentCulture;
        }

        public string GetString(string key)
        {
            return _resourceManager.GetString(key, _currentCulture);
        }

        public void SetCulture(string cultureCode)
        {
            _currentCulture = new CultureInfo(cultureCode);
        }
    }
}
