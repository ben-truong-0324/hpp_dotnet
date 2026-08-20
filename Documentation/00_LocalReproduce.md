#!/bin/bash

# Exit immediately if any command fails
set -e

echo "🚀 Bootstrapping Hannah's Pampered Pets App..."

# 1. Create the solution
echo "📁 Creating solution..."
dotnet new sln -n HannahsPamperedPetsApp

# 2. Create the core layers
echo "🏗️ Creating project layers..."
dotnet new classlib -n HannahsPamperedPetsApp.Domain
dotnet new classlib -n HannahsPamperedPetsApp.Application
dotnet new classlib -n HannahsPamperedPetsApp.Infrastructure
dotnet new web -n HannahsPamperedPetsApp.WebUI

# 3. Add projects to the solution
echo "🔗 Adding projects to solution..."
dotnet sln add HannahsPamperedPetsApp.Domain
dotnet sln add HannahsPamperedPetsApp.Application
dotnet sln add HannahsPamperedPetsApp.Infrastructure
dotnet sln add HannahsPamperedPetsApp.WebUI

# 4. Wire up the Clean Architecture dependencies (pointing inward)
echo "🎯 Setting up project references..."
dotnet add HannahsPamperedPetsApp.Application reference HannahsPamperedPetsApp.Domain
dotnet add HannahsPamperedPetsApp.Infrastructure reference HannahsPamperedPetsApp.Application
dotnet add HannahsPamperedPetsApp.WebUI reference HannahsPamperedPetsApp.Application
dotnet add HannahsPamperedPetsApp.WebUI reference HannahsPamperedPetsApp.Infrastructure

# 5. Add Google Cloud Firestore SDK
echo "☁️ Adding Firestore SDK..."
dotnet add HannahsPamperedPetsApp.Infrastructure package Google.Cloud.Firestore

# 6. Create and link the Test project
echo "🧪 Setting up xUnit test project..."
dotnet new xunit -n HannahsPamperedPetsApp.Tests
dotnet sln add HannahsPamperedPetsApp.Tests
dotnet add HannahsPamperedPetsApp.Tests reference HannahsPamperedPetsApp.Domain
dotnet add HannahsPamperedPetsApp.Tests reference HannahsPamperedPetsApp.Application

# 7. Run Sanity Checks
echo "🛠️ Building the entire solution..."
dotnet build

echo "✅ Running tests..."
dotnet test

echo "🎉 Setup complete! To start the web server, run:"
echo "dotnet run --project HannahsPamperedPetsApp.WebUI"