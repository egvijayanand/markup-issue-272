# markup-issue-272
Community Toolkit Markup - Automated Typed Bindings

[Initial Discussion](https://github.com/CommunityToolkit/Maui.Markup/issues/272)

Automated setter method for two-way binding, facilitating the definition of default bindable properties:

[Active Discussion](https://github.com/CommunityToolkit/Maui.Markup/discussions/343)

```cs
// Existing

// Requires the target property even if it is the default one, and a setter definition if it is two-way binding.
new Picker().Bind(Picker.SelectedIndexProperty, static (MyViewModel vm) => vm.Index, static (MyViewModel vm, int value) => vm.Index = value);

// Proposed

// SelectedIndexProperty is the default bindable property for Picker control
// It's two-way binding by default
new Picker().Bind(static (MyViewModel vm) => vm.Index);
```
