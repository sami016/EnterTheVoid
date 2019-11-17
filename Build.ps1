
dotnet publish -r win10-x64 -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/win64
dotnet publish -r win10-x86 -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/win32
dotnet publish -r win10-arm -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/win_arm
dotnet publish -r linux-x64 -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/linux64
dotnet publish -r linux-arm -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/linux_arm
dotnet publish -r osx-x64 -c release .\\GreatSpaceRace\\GreatSpaceRace.csproj --output ./GreatSpaceRace/bin/publish/mac64