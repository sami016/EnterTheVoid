
dotnet publish -r win10-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win64
dotnet publish -r win10-x86 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win32
dotnet publish -r win10-arm -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/win_arm
dotnet publish -r linux-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/linux64
dotnet publish -r linux-arm -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/linux_arm
dotnet publish -r osx-x64 -c release .\\IntoTheVoid\\IntoTheVoid.csproj --output ./IntoTheVoid/bin/publish/mac64