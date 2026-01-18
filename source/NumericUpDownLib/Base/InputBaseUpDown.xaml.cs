using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NumericUpDownLib.Base;

/// <summary>
/// This class serves as a target for styling the <see cref="AbstractBaseUpDown{T}"/> class
/// since styling directly on <see cref="AbstractBaseUpDown{T}"/> is not supported in XAML.
/// </summary>
public abstract class InputBaseUpDown : Control
{
    /// <summary>
    /// Determines whether the textbox portion of the control is editable
    /// (requires additional check of bounds) or not.
    /// </summary>
    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(InputBaseUpDown), new PropertyMetadata(true));

    /// <summary>
    /// Determines the allowed style of a number entered and displayed in the textbox.
    /// </summary>
    public static readonly DependencyProperty NumberStyleProperty =
        DependencyProperty.Register(nameof(NumberStyle), typeof(NumberStyles), typeof(InputBaseUpDown), new PropertyMetadata(NumberStyles.Any));

    /// <summary>
    /// Backing store of <see cref="EnableValidatingIndicator"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EnableValidatingIndicatorProperty =
        DependencyProperty.Register(nameof(EnableValidatingIndicator), typeof(bool), typeof(InputBaseUpDown), new PropertyMetadata(false));

    /// <summary>
    /// Backing store of <see cref="EditingVisibility"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EditingVisibilityProperty =
        DependencyProperty.Register(nameof(EditingVisibility), typeof(Visibility), typeof(InputBaseUpDown), new PropertyMetadata(Visibility.Hidden));

    /// <summary>
    /// Backing store of <see cref="EditingColorBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EditingColorBrushProperty =
        DependencyProperty.Register(nameof(EditingColorBrush), typeof(System.Windows.Media.SolidColorBrush),
            typeof(InputBaseUpDown), new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green)));


    /// <summary>
    /// identify that the inputing data is valid or not.,
    /// </summary>
    /// <value></value>
    protected System.Windows.Media.SolidColorBrush EditingColorBrush
    {
        get => (System.Windows.Media.SolidColorBrush)GetValue(EditingColorBrushProperty);
        set => SetValue(EditingColorBrushProperty, value);
    }

    /// <summary>
    /// identify that the editing Visibility
    /// </summary>
    /// <value></value>
    protected Visibility EditingVisibility
    {
        get => (Visibility)GetValue(EditingVisibilityProperty);
        set => SetValue(EditingVisibilityProperty, value);
    }

    /// <summary>
    /// identify that the is enable the red/green tip while editing
    /// </summary>
    /// <value></value>
    public bool EnableValidatingIndicator
    {
        get => (bool)GetValue(EnableValidatingIndicatorProperty);
        set => SetValue(EnableValidatingIndicatorProperty, value);
    }

    private static readonly RoutedCommand _increaseCommand = new(nameof(IncreaseCommand), typeof(InputBaseUpDown));
    private static readonly RoutedCommand _decreaseCommand = new(nameof(DecreaseCommand), typeof(InputBaseUpDown));

    /// <summary>
    /// Class constructor
    /// </summary>
    public InputBaseUpDown()
    {
        CommandManager.RegisterClassCommandBinding(
            typeof(InputBaseUpDown), new CommandBinding(_increaseCommand, OnIncreaseCommand, OnCanIncreaseCommand));

        CommandManager.RegisterClassInputBinding(
            typeof(InputBaseUpDown), new InputBinding(_increaseCommand, new KeyGesture(Key.Up)));

        CommandManager.RegisterClassCommandBinding(
            typeof(InputBaseUpDown), new CommandBinding(_decreaseCommand, OnDecreaseCommand, OnCanDecreaseCommand));
    }

    /// <summary>
    /// Expose the increase value command via <seealso cref="RoutedCommand"/> property.
    /// </summary>
    public static RoutedCommand IncreaseCommand => _increaseCommand;

    /// <summary>
    /// Expose the decrease value command via <seealso cref="RoutedCommand"/> property.
    /// </summary>
    public static RoutedCommand DecreaseCommand => _decreaseCommand;

    /// <summary>
    /// Determines whether the textbox portion of the control is editable
    /// (requires additional check of bounds) or not.
    /// </summary>
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    /// <summary>
    /// Gets/sets the allowed style of a number entered and displayed in the textbox.
    /// </summary>
    public NumberStyles NumberStyle
    {
        get => (NumberStyles)GetValue(NumberStyleProperty);
        set => SetValue(NumberStyleProperty, value);
    }

    /// <summary>
    /// Increase the displayed integer value
    /// </summary>
    protected abstract void OnIncrease();

    /// <summary>
    /// Determines whether the increase command is available or not.
    /// </summary>
    protected abstract bool CanIncreaseCommand();

    /// <summary>
    /// Decrease the displayed integer value
    /// </summary>
    protected abstract void OnDecrease();

    /// <summary>
    /// Determines whether the decrease command is available or not.
    /// </summary>
    protected abstract bool CanDecreaseCommand();

    /// <summary>
    /// Determine whether the IncreaseCommand can be executed or not and return the result
    /// in the <see cref="CanExecuteRoutedEventArgs.CanExecute"/> property of the given
    /// <paramref name="e"/>.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnCanIncreaseCommand(object sender, CanExecuteRoutedEventArgs e)
    {
        if (sender is InputBaseUpDown control)
        {
            e.CanExecute = control.CanIncreaseCommand();
            e.Handled = true;
        }
    }

    /// <summary>
    /// Execute the increase value command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs e)
    {
        if (sender is InputBaseUpDown control)
        {
            control.OnIncrease();
            e.Handled = true;
        }
    }

    /// <summary>
    /// Execute the decrease value command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs e)
    {
        if (sender is InputBaseUpDown control)
        {
            control.OnDecrease();
            e.Handled = true;
        }
    }

    /// <summary>
    /// Determine whether the DecreaseCommand can be executed or not and return the result
    /// in the <see cref="CanExecuteRoutedEventArgs.CanExecute"/> property of the given
    /// <paramref name="e"/>.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnCanDecreaseCommand(object sender, CanExecuteRoutedEventArgs e)
    {
        if (sender is InputBaseUpDown control)
        {
            e.CanExecute = control.CanDecreaseCommand();
            e.Handled = true;
        }
    }
}
