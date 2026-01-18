using System.Security;

namespace SettingsModel.Models.XML.Converters;

/// <summary>
/// Holds a collection of <seealso cref="IAlternativeDataTypeHandler"/> alternative datatype
/// handlers to handle datatypes that are not supported through equivalent conversion
/// in alternative datatypes.
/// </summary>
internal class AlternativeDataTypeHandler
{
    private readonly Dictionary<Type, IAlternativeDataTypeHandler> _converters = [];

    public AlternativeDataTypeHandler()
    {
        _converters.Add(typeof(SecureString), new SecureStringHandler());
    }

    /// <summary>
    /// Finds an alternative datatype handler to handle datatypes that are not
    /// supported through equivalent conversion in alternative datatypes.
    /// </summary>
    /// <param name="typeOfDataType2Handle"></param>
    /// <returns></returns>
    public IAlternativeDataTypeHandler? FindHandler(Type typeOfDataType2Handle)
    {
        IAlternativeDataTypeHandler? ret = null;

        try
        {
            _converters.TryGetValue(typeOfDataType2Handle, out ret);
        }
        catch { }

        return ret;
    }
}
