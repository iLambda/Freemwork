using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MonoGame.Framework;

namespace Freemwork.Test.RT
{
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        readonly XNAContext game;

        public GamePage(string LaunchArguments)
        {
            InitializeComponent();

            game = XamlGame<XNAContext>.Create(LaunchArguments, Window.Current.CoreWindow, this);
        }
    }
}
