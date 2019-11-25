
dotnet publish -r win10-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win64
dotnet publish -r win10-x86 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win32
dotnet publish -r win10-arm -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win_arm
dotnet publish -r linux-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/linux64
dotnet publish -r linux-arm -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/linux_arm
dotnet publish -r osx-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/mac64

Compress-Archive -Path .\IntoTheVoid\bin\publish\win64\ -DestinationPath .\IntoTheVoid\bin\publish\win64 -Force
Compress-Archive -Path .\IntoTheVoid\bin\publish\win32\ -DestinationPath .\IntoTheVoid\bin\publish\win32 -Force
Compress-Archive -Path .\IntoTheVoid\bin\publish\win_arm\ -DestinationPath .\IntoTheVoid\bin\publish\win_arm -Force
Compress-Archive -Path .\IntoTheVoid\bin\publish\linux64\ -DestinationPath .\IntoTheVoid\bin\publish\linux64 -Force
Compress-Archive -Path .\IntoTheVoid\bin\publish\linux_arm\ -DestinationPath .\IntoTheVoid\bin\publish\linux_arm -Force
Compress-Archive -Path .\IntoTheVoid\bin\publish\mac64\ -DestinationPath .\IntoTheVoid\bin\publish\mac64 -Force