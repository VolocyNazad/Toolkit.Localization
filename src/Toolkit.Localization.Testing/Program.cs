using Microsoft.Extensions.DependencyInjection;
using Toolkit.Localization.Testing;

Hosting.Services.GetRequiredService<Service>();
Hosting.Services.GetRequiredService<Service<int>>();
Console.WriteLine("Hello, World!");