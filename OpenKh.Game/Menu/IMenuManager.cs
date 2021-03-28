using OpenKh.Engine.Input;

namespace OpenKh.Game.Menu
{
    public interface IMenuManager
    {
        IGameContext GameContext { get; }
        AnimatedSequenceFactory SequenceFactory { get; }
        IInput Input { get; }

        void PushSubMenuDescription(ushort messageId);
        void PushSubMenuDescription(string message);
        void PopSubMenuDescription();
        void SetElementDescription(ushort messageId);
        void CloseAllMenu();
    }
}
