using System.Linq.Expressions;
using System.Reflection;

namespace MauiApp1.Extensions
{
    public static partial class TypedBindingExtensions
    {
        /// <summary>Binds to the default bindable property.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        public static TBindable Bindv2<TBindable, TBindingContext, TSource>(
            this TBindable bindable,
            Expression<Func<TBindingContext, TSource>> expression,
            BindingMode mode = BindingMode.Default,
            IValueConverter? converter = null,
            object? converterParameter = null,
            string? stringFormat = null,
            object? source = null,
            object? targetNullValue = null,
            object? fallbackValue = null)
            where TBindable : BindableObject
        {
            bindable.Bind(PropertyName(expression),
                          mode,
                          converter,
                          converterParameter,
                          stringFormat,
                          source,
                          targetNullValue,
                          fallbackValue);
            return bindable;
        }

        /// <summary>Binds to the specified bindable property.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        public static TBindable Bindv2<TBindable, TBindingContext, TSource>(
            this TBindable bindable,
            BindableProperty property,
            Expression<Func<TBindingContext, TSource>> expression,
            BindingMode mode = BindingMode.Default,
            IValueConverter? converter = null,
            object? converterParameter = null,
            string? stringFormat = null,
            object? source = null,
            object? targetNullValue = null,
            object? fallbackValue = null)
            where TBindable : BindableObject
        {
            bindable.Bind(property,
                          PropertyName(expression),
                          mode,
                          converter,
                          converterParameter,
                          stringFormat,
                          source,
                          targetNullValue,
                          fallbackValue);
            return bindable;
        }

        /// <summary>Parses the lambda expression and returns the property name.</summary>
        /// <param name="expression">Lambda expression of the source property to bind to.</param>
        static string PropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            MemberExpression? memExp;

            if (expression.Body is UnaryExpression unExp)
            {
                if (unExp.NodeType == ExpressionType.Convert)
                {
                    memExp = (MemberExpression)unExp.Operand;
                    return memExp.Member.Name;
                }
            }

            memExp = (MemberExpression)expression.Body;
            var propMemExp = memExp;

            string path = string.Empty;

            while (memExp is not null && memExp?.Expression?.NodeType == ExpressionType.MemberAccess)
            {
                var propInfo = memExp.Expression.GetType().GetProperty("Member");
                var propValue = propInfo?.GetValue(memExp.Expression, null) as PropertyInfo;
                path = $"{propValue?.Name}.{path}";

                memExp = memExp.Expression as MemberExpression;
            }

            return path + propMemExp.Member.Name;
        }
    }
}
