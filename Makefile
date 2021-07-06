build:
	dotnet build Ramosi.sln

test:
	dotnet build Ramosi.sln; dotnet test -v=normal --no-build
