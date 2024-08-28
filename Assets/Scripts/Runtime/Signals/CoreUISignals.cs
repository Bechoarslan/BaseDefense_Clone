using Runtime.Enums.UI;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction onDestroyAllPanels = delegate { };
        public UnityAction<UIPanelTypes> onOpenPanel = delegate { };
        public UnityAction<UIPanelTypes> onClosePanel = delegate { };

    }
}