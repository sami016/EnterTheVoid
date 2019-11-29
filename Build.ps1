
dotnet publish -r win10-x64 -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/win64
dotnet publish -r win10-x86 -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/win32
dotnet publish -r win10-arm -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/win_arm
dotnet publish -r linux-x64 -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/linux64
dotnet publish -r linux-arm -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/linux_arm
dotnet publish -r osx-x64 -c release .\\EnterTheVoid\\EnterTheVoid.csproj --output ./EnterTheVoid/bin/publish/mac64

Compress-Archive -Path .\EnterTheVoid\bin\publish\win64\ -DestinationPath .\EnterTheVoid\bin\publish\win64 -Force
Compress-Archive -Path .\EnterTheVoid\bin\publish\win32\ -DestinationPath .\EnterTheVoid\bin\publish\win32 -Force
Compress-Archive -Path .\EnterTheVoid\bin\publish\win_arm\ -DestinationPath .\EnterTheVoid\bin\publish\win_arm -Force
Compress-Archive -Path .\EnterTheVoid\bin\publish\linux64\ -DestinationPath .\EnterTheVoid\bin\publish\linux64 -Force
Compress-Archive -Path .\EnterTheVoid\bin\publish\linux_arm\ -DestinationPath .\EnterTheVoid\bin\publish\linux_arm -Force
Compress-Archive -Path .\EnterTheVoid\bin\publish\mac64\ -DestinationPath .\EnterTheVoid\bin\publish\mac64 -Force