using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RaceDBGui.Views
{
    public partial class SQLRequestView : UserControl
    {
        public SQLRequestView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
