Clear-Host

$path = [System.IO.DirectoryInfo]"F:\Media\Audio\Sounds\Drum Collections\DoruMalaia"

$list = @($path.EnumerateFiles('*.wav', [System.IO.SearchOption]::AllDirectories))

$list | ForEach-Object { $_.CopyTo([System.IO.Path]::Combine("F:\Test", [System.IO.Path]::GetFileName($_)), $false)  }
#$list | ForEach-Object { [System.IO.Path]::Combine([System.IO.Directory]::GetParent($_), [System.IO.Path]::GetFileName($_).Replace("-", ""))  } | Sort-Object Length
#$list | ForEach-Object { "$([System.IO.Path]::GetFileNameWithoutExtension($_.FullName))" } | Sort-Object Length 

$list.Count
