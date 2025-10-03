using Microsoft.Extensions.DependencyInjection;
using Toolkit.Localization.Testing;

Hosting.Services.GetRequiredService<Service>();
Hosting.Services.GetRequiredService<Service<int>>();
Hosting.Services.GetRequiredService<Service2>();
Console.WriteLine("Hello, World!");