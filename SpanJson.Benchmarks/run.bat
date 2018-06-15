SET COMPlus_JitDump=Serialize
dotnet publish -c Release
robocopy /e C:\Repos\coreclr\bin\Product\Windows_NT.x64.Release C:\Repos\SpanJson\SpanJson.Benchmarks\bin\Release\netcoreapp2.1\win10-x64\publish > NUL
copy C:\Repos\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll C:\Repos\SpanJson\SpanJson.Benchmarks\bin\Release\netcoreapp2.1\win10-x64\publish > NUL
C:\Repos\SpanJson\SpanJson.Benchmarks\bin\Release\netcoreapp2.1\win10-x64\publish\SpanJson.Benchmarks.exe > output.txt
