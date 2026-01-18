using SettingsModel.Interfaces;

namespace SettingsModel.Models;

/// <summary>
/// Determines the type of schema of a schema object <seealso cref="IOptionsSchema"/>
/// </summary>
public enum OptionSchemaType
{
    /// <summary>
    /// The schema object represents a simple value (bool, int etc).
    /// </summary>
    SingleValue = 0,

    /// <summary>
    /// The schema object represents a list of simple values (bool values, int values etc).
    /// </summary>
    List = 1
}
