
Clear-Host

$vsProjectReference = @{ SolutionName='FluxSolution'; ProjectName='BaseLibrary'; Configuration='Debug'; TargetFramework='net5.0' }

# Converts a local project URI to a 'binary' URI, for debugging purposes.
function ConvertTo-BinUri ( [uri]$VsUri, [hashtable]$VsReference ) { New-Object System.Uri $VsUri.OriginalString.Replace("/\", "/\$($VsReference.SolutionName)\$($VsReference.ProjectName)\bin\$($VsReference.Configuration)\$($VsReference.TargetFramework)\") }
# Expand a relative solution resource reference to a file path.
function Expand-FileToPath ( [string]$VsRelativeFileName, [hashtable]$VsReference ) { ".$((ConvertTo-BinUri (New-Object System.Uri "file://\$VsRelativeFileName") $VsReference).LocalPath.Replace('/', '\'))" }
# Tests whether a pipeline contains any objects. 
function Test-Any() { begin { $any = $false } process { $any = $true } end { $any } }

if(-not ([System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.FullName -match "^$($vsProjectReference.ProjectName)" } | Test-Any)) # Check whether the project library is already loaded.
{
    # Various ways to include/use the Flux BaseLibray in PowerShell:

    [string]$assemblyFileName = Expand-FileToPath "$($vsProjectReference.ProjectName).dll" $vsProjectReference # The file name of the binary assembly.
    "Loading assembly from file ($assemblyFileName)."

    [byte[]]$bytes = [System.IO.File]::ReadAllBytes($assemblyFileName) # Read the binary assembly to a byte array.

    # [string]$base64 = [System.Convert]::ToBase64String($bytes) # Encode the byte array to a Base64 string. 

    # [string]$base64TextFile = 'C:\Flux\AssemblyString.txt' # The path of the base64 string.
    # [void][System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($base64TextFile)) # Ensure the folder structure is there.
    # [System.IO.File]::WriteAllText($base64TextFile, $base64) # Write the Base64 string to a text file.

    # # Optionally use a literal Base64 string (obtain from the text file created above) to create independence from files.
    # [string]$base64 = '[REPLACE_WITH_LITERAL_BASE64_STRING_FROM_FILE]'

    # if([System.IO.File]::Exists($base64TextFile)) # Check if the Base64 file exists.
    # { [string]$base64 = [System.IO.File]::ReadAllText($base64TextFile) } # Read a Base64 string from the text file.

    # [byte[]]$bytes = [System.Convert]::FromBase64String($base64) # Decode the Base64 string to a byte array.

    [void][System.Reflection.Assembly]::Load($bytes) # Load the byte array as an assembly into the current context.
}
else { "Assembly already loaded..." }

# Sample use from Flux BaseLibrary:

# $PSVersionTable

# [Flux.Locale].GetProperties() | Select-Object Name | ForEach-Object { "$($_.Name)=`"$([Flux.Locale]::"$($_.Name)")`"" }
# [Flux.Locale]::SpecialFolders | Format-Table

# $md = New-Object 'Flux.SetMetrics.DamerauLevenshteinDistance[char]'
# $fm = $md.GetFullMatrix("settings", "kitten")
# $fe = 
# {  
#   param($e, $i)

#   $e.ToString()
# }
# $af = New-Object Flux.FormatProviders.ArrayFormatter
# $s = $af.TwoToConsoleString($fm)
# $s

# $cad = New-Object Flux.Resources.Scowl.TwoOfTwelveFull
# $uri = ConvertTo-BinUri ([Flux.Resources.Scowl.TwoOfTwelveFull]::LocalUri) $vsProjectReference
# $cad.GetDataTable($uri) | Where-Object {$_."Word" -cmatch '^[a-z]{2,}$' -and $_."NonAmerican" -eq '-&'} | Select-Object -Unique -First 1000 Word | Format-Table

# [Flux.Locale].Assembly.GetTypes() | ForEach-Object { $_.ImplementedInterfaces }
# [Flux.Locale].Assembly.GetTypes() | Select-Object FullName

#[Flux.Locale].Assembly.GetTypes() | 
    #Where-Object { $_.IsPublic -and $_.IsInterface } |
    # Where-Object { $_.ImplementedInterfaces | Where-Object { $_.Name.EndsWith('`1') } | Test-Any } |
    # Select-Object FullName, ImplementedInterfaces |
    #Sort-Object FullName
