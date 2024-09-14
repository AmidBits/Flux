# Converts a local project URI to a 'binary' URI, for debugging purposes.
function ConvertTo-BinUri ( [uri]$VsUri, [hashtable]$VsReference ) 
{ New-Object System.Uri $VsUri.OriginalString.Replace("/\", "/\$($VsReference.SolutionName)\$($VsReference.ProjectName)\bin\$($VsReference.Configuration)\$($VsReference.TargetFramework)\") }
# Expand a relative solution resource reference to a file path.
function Expand-FileToPath ( [string]$VsRelativeFileName, [hashtable]$VsReference ) 
{ ".$((ConvertTo-BinUri (New-Object System.Uri "file://\$VsRelativeFileName") $VsReference).LocalPath.Replace('/', '\'))" }
# Tests whether a pipeline contains any objects. 
function Test-Any() 
{ begin { $any = $false } process { $any = $true } end { $any } }

Clear-Host

"PowerShell $($PSVersionTable.PSVersion) $($PSVersionTable.PSEdition) on $($PSVersionTable.OS) ($($PSVersionTable.Platform))"

$vsProjectReference = @{ SolutionName='FluxSolution'; ProjectName='BaseLibrary'; Configuration='Debug'; TargetFramework='net8.0' }

[string]$assemblyFileName = Expand-FileToPath "$($vsProjectReference.ProjectName).dll" $vsProjectReference

if(-not ([System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.FullName -match "^$($vsProjectReference.ProjectName)" } | Test-Any)) # Check whether the project library is already loaded.
{
    "Add-on <$assemblyFileName>$([System.Environment]::NewLine)"

    # Various ways to include/use the Flux BaseLibray in PowerShell:

    [byte[]]$bytes = [System.IO.File]::ReadAllBytes($assemblyFileName) # Read the binary assembly to a byte array.

    <#
    [string]$base64TextFile = 'C:\Flux\AssemblyString.txt' # The path of the base64 string.

    [string]$base64 = [System.Convert]::ToBase64String($bytes) # Encode the byte array to a Base64 string. 

    [void][System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($base64TextFile)) # Ensure the folder structure is there.
    [System.IO.File]::WriteAllText($base64TextFile, $base64) # Write the Base64 string to a text file.

    if([System.IO.File]::Exists($base64TextFile)) # Check if the Base64 file exists.
    { [string]$base64 = [System.IO.File]::ReadAllText($base64TextFile) } # Read a Base64 string from the text file.

    [byte[]]$bytes = [System.Convert]::FromBase64String($base64) # Decode the Base64 string to a byte array.
    #>

    [void][System.Reflection.Assembly]::Load($bytes) # Load the byte array as an assembly into the current context.
}

# Sample use from Flux BaseLibrary:

"Flux.Locale.EnvironmentVariables: (This dictionary is projected directly from the `"System.Environment.GetEnvironmentVariables()`" method.)"
[Flux.Locale]::EnvironmentVariables | Format-Table

"Flux.Locale.SpecialFolders: (This dictionary represents the names and values of the `"System.Environment.SpecialFolder`" enum.)"
[Flux.Locale]::SpecialFolders | Format-Table

"Flux.Locale.GetProperties(): (This dictionary is compiled from various sources within the system. They can also be accessed as properties of the Flux.Locale, using the same names as the keys in the dictionary.)"
[Flux.Locale]::GetProperties() | Format-Table

# "Prime numbers: $([Flux.NumberSequences.PrimeNumber]::GetAscendingPrimes[int](2) | Select-Object -First 25 | Join-String -Separator ',')$([System.Environment]::NewLine)"

"LevenshteinDistanceMatrix(`"sitting`", `"kitten`")$([System.Environment]::NewLine)"
$m = [Flux.Fx]::LevenshteinDistanceMatrix[char]("sitting", "kitten")
$s = [Flux.Fx]::Rank2ToConsoleString[int]($m)
"$($s)$([System.Environment]::NewLine)"

"LevenshteinDistanceMatrix(`"Sunday`", `"Saturday`")$([System.Environment]::NewLine)"
$m = [Flux.Fx]::LevenshteinDistanceMatrix[char]("Sunday", "Saturday")
$s = [Flux.Fx]::Rank2ToConsoleString[int]($m)
"$($s)$([System.Environment]::NewLine)"

"Excerpt from ProjectGutenberg's TenThousandWonderfulThings, searching for `"SCANDINAVIA`" in the title.$([System.Environment]::NewLine)"
$book = New-Object Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings
$book.AcquireDataTable($null) 
#| Select-Object -Skip 100 -First 100
| Where-Object {$_.Title -match 'SCANDINAVIA'} | Format-Table

"$([System.Environment]::NewLine)Looking through interfaces using reflection."
[Flux.Locale].Assembly.GetTypes()
 | Where-Object { $_.IsPublic -and $_.IsInterface }
 | Where-Object { $_.ImplementedInterfaces | Where-Object { $_.Name.EndsWith('`1') } | Test-Any }
 | Select-Object FullName, ImplementedInterfaces
 | Sort-Object FullName
