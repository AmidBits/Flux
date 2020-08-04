
Clear-Host

# Various ways to include/use the Flux BaseLibray in PowerShell:

[string]$assemblyFileName = '.\FluxSolution\BaseLibrary\bin\Debug\netcoreapp3.1\BaseLibrary.dll' # The file name of the binary assembly.
[string]$base64TextFile = 'C:\Flux\AssemblyString.txt' # The path of the base64 string.

[byte[]]$bytes = [System.IO.File]::ReadAllBytes($assemblyFileName) # Read the binary assembly to a byte array.

[string]$base64 = [System.Convert]::ToBase64String($bytes) # Encode the byte array to a Base64 string. 

[void][System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($base64TextFile)) # Ensure the folder structure is there.
[System.IO.File]::WriteAllText($base64TextFile, $base64) # Write the Base64 string to a text file.

# Optinoally use a literal Base64 string (obtain from the text file created above) to create independence from files.
[string]$base64 = '[REPLACE_WITH_LITERAL_BASE64_STRING_FROM_FILE]'

if([System.IO.File]::Exists($base64TextFile)) # Check if the Base64 file exists.
{ [string]$base64 = [System.IO.File]::ReadAllText($base64TextFile) } # Read a Base64 string from the text file.

[byte[]]$bytes = [System.Convert]::FromBase64String($base64) # Decode the Base64 string to a byte array.

[void][System.Reflection.Assembly]::Load($bytes) # Load the byte array as an assembly into the current context.

# Sample use from Flux BaseLibrary:

[Flux.Locale].GetProperties() | Select-Object Name | ForEach-Object { $_.Name + "=$([Flux.Locale]::"$($_.Name)")" }
