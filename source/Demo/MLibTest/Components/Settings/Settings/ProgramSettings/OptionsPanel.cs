
using Settings.Interfaces;
using SettingsModel.Interfaces;
using SettingsModel.Models;

namespace Settings.ProgramSettings;
internal class OptionsPanel : IOptionsPanel
{
    private IEngine? mQuery = null;

    public OptionsPanel()
    {
        mQuery = Factory.CreateEngine();
    }

    /// <summary>
    /// Gets the options <seealso cref="IEngine"/> that used to manage program options.
    /// </summary>
    public IEngine? Options
    {
        get => mQuery;

        private set => mQuery = value;
    }
}
