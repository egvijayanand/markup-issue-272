﻿namespace MauiApp1;

public partial class App : Application
{
    public App() => InitializeComponent();

    protected override Window CreateWindow(IActivationState? activationState)
    => new(new NavigationPage(new MainPage())) { Title = "MauiApp1" };
}
