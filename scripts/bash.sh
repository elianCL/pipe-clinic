#!/bin/bash

echo "Executing Bash script..."

# Execute Gradle clean build and capture output to both console and a log file
./gradle clean build -p ./build > >(tee -a build_output.log) 2>&1

# Check the exit code of the Gradle build
if [ $? -eq 0 ]; then
    echo "Gradle build completed successfully."
    
    # Navigate to dotnet/PipeClinic directory
    cd dotnet/PipeClinic
    
    echo "Executing dotnet clean..."
    dotnet clean
    
    echo "Executing dotnet restore..."
    dotnet restore
    
    echo "Executing dotnet build..."
    dotnet build
    
    # Navigate to dotnet/PipeClinic.Tests directory
    cd ../PipeClinic.Tests
    
    echo "Executing dotnet test..."
    dotnet test
else
    echo "Error during Gradle build. Skipping dotnet commands."
    exit 1
fi
