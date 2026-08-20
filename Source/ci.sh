#!/bin/bash

# Exit immediately if any command fails
set -e

echo "Starting up Hannah's Pampered Pets App..."

# 1. Run Sanity Checks
echo "🛠️ Building the entire solution..."
dotnet build

echo "✅ Running tests..."
dotnet test

echo "🎉 Setup complete! To start the web server, run:"
echo "dotnet run --project HannahsPamperedPetsApp.WebUI"