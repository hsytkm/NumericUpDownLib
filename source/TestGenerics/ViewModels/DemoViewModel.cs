
using TestGenerics.ViewModels.Base;
using UpDownDemoLib.Demos.ViewModels;

namespace TestGenerics.ViewModels;
/// <summary>
/// Application Viewmodel class to be bound to MainWindow...
/// </summary>
public class AppViewModel : ViewModelBase
{
    public AppViewModel()
    {
        Demo = new DemoViewModel();
    }

    public DemoViewModel Demo { get; }
}
