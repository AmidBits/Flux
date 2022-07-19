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

$vsProjectReference = @{ SolutionName='FluxSolution'; ProjectName='BaseLibrary'; Configuration='Debug'; TargetFramework='net6.0' }

[string]$assemblyFileName = Expand-FileToPath "$($vsProjectReference.ProjectName).dll" $vsProjectReference

"Add-on <$assemblyFileName>$([System.Environment]::NewLine)"

if(-not ([System.AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.FullName -match "^$($vsProjectReference.ProjectName)" } | Test-Any)) # Check whether the project library is already loaded.
{
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

# "Locale-Properties:"
 [Flux.Locale]::GetProperties()
# [Flux.Locale]::SpecialFolders
# return;

#$md = New-Object 'Flux.Memory.Metrics.DamerauLevenshteinDistance[char]'
#$fm = $md.GetFullMatrix("settings", "kitten")

# $fe = 
# {  
#   param($e, $i)

#   $e.ToString()
# }

#$af = New-Object Flux.Formatting.ArrayFormatter
#$s = $af.TwoToConsoleString($fm)

# $uri = New-Object System.Uri ([Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings]::LocalFile)
# $uri.OriginalString
# $binuri = ConvertTo-BinUri $uri $vsProjectReference
# $binuri.OriginalString
# $cad = New-Object Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings $binuri
# $atb = $cad.AcquireDataTable() 
# $atb.Rows | Where-Object {$_.Title -match 'SCANDINAVIA'}  | Format-List

# [Flux.Locale].Assembly.GetTypes() | ForEach-Object { $_.ImplementedInterfaces }
# [Flux.Locale].Assembly.GetTypes() | Select-Object FullName

#[Flux.Locale].Assembly.GetTypes() | 
    #Where-Object { $_.IsPublic -and $_.IsInterface } |
    # Where-Object { $_.ImplementedInterfaces | Where-Object { $_.Name.EndsWith('`1') } | Test-Any } |
    # Select-Object FullName, ImplementedInterfaces |
    #Sort-Object FullName
