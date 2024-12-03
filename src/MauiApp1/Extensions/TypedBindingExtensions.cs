using System.Linq.Expressions;

namespace MauiApp1.Extensions;

public static partial class TypedBindingExtensions
{
    // Since the package-defined DefaultProperties is not accessible outside
    // So, have defined a similar one - To be reused
    private static readonly Dictionary<string, BindableProperty> DefaultProperties = new()
    {
        { "Microsoft.Maui.Controls.Label", Label.TextProperty },
        { "Microsoft.Maui.Controls.Picker", Picker.SelectedIndexProperty }
    };

    /// <summary>Binds to the default bindable property.</summary>
    public static TBindable BindV2<TBindable, TBindingContext, TSource>(
        this TBindable bindable,
        Expression<Func<TBindingContext, TSource>> getter,
        Action<TBindingContext, TSource>? setter = null,
        BindingMode mode = BindingMode.Default,
        string? stringFormat = null,
        TBindingContext? source = default)
        where TBindable : BindableObject
        where TBindingContext : class?
    {
        // Since it's a class derived from BindableObject, FullName can't be null.
        var typeName = typeof(TBindable).FullName!;
        if (!DefaultProperties.TryGetValue(typeName, out var targetProperty))
        {
            throw new InvalidOperationException($"Default bindable property is not defined for the type - {typeName}.");
        }

        // User can still provide their own setter definition
        // If undefined, the toolkit will add the default implementation
        // That just assigns the updated value back to the same property
        if (setter is null)
        {
            // Include the setter only if it is defined as two-way or if the mode is overridden.
            if (targetProperty.DefaultBindingMode == BindingMode.TwoWay || mode == BindingMode.TwoWay)
            {
                // Already defined in the toolkit to retrieve the member name - To be reused
                // Assuming MemberExpression for sample
                var propertyName = ((MemberExpression)getter.Body).Member.Name;
                var param1 = Expression.Parameter(typeof(TBindingContext), "context");
                var param2 = Expression.Parameter(typeof(TSource), "value");
                var memExp = Expression.Property(param1, propertyName);
                var assignExp = Expression.Assign(memExp, param2);
                // TODO: Need to check whether the action can be a static lambda
                var action = Expression.Lambda<Action<TBindingContext, TSource>>(assignExp, [param1, param2]).Compile();

                // Structure of the generated definition
                // setter = (TBindingContext context, TSource value) => context.Property = value;
                setter = action;
            }
        }

        // Invokes one of the overloads from the package, with value for the targetProperty and setter.
        return bindable.Bind<TBindable, TBindingContext, TSource, object?, object?>(
            targetProperty,
            getter,
            setter,
            mode,
            null,
            null,
            null,
            stringFormat,
            source);
    }
}
