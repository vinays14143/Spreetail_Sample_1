# App Overview

A basic console app built on .NETCore 5.0.

# Build

`dotnet build`

# Run

`dotnet run`

# Program

- Console app creates singleton instance of ReadWriteService for all read and write operations.
- Contains CommandManager which manages various operations on dictionary.
- Read/Write MultiValues dictionary Service for read and write operation implementation for dictionary.
- Helper called ValidateInput class does the validation on input request.

# Test
Unit test for some of the read and write operations using Nunit unit testing packages.