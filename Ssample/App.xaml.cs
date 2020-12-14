using SimpleWPF.Core;
using Ssample.ViewModel;
using Ssample.ViewModel.Base_view_models;
using System.Windows;

namespace Ssample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Entry point for simple wpf
            var core = new SimpleCore();
            core.Startup(new AppViewModel(), new DefaultViewModel(), true);

            //Multiple ways to load templates
            var templateManager = new DataTemplateManager();
            templateManager.LoadDataTemplatesByConvention();
        }
    }
}
