Clear-Host

$base = [System.IO.DirectoryInfo]"E:\Media\Audio"

function Build-Playlist([string]$playlistName, [string[]]$directories, [string]$filter = "*.mp3")
{
    $playlistFileInfo = [System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$playlistName.m3u8")

    $m3u8 = $playlistFileInfo.CreateText();

    $m3u8.WriteLine("#EXTM3U");
    #$m3u8.WriteLine("#$($playlistFileInfo.Name)");
    $m3u8.WriteLine("#PLAYLIST:$playlistName");

    $fileInfos = ($directories | ForEach-Object { $directory = [System.IO.DirectoryInfo][System.IO.Path]::Combine($base, $_); $directory.EnumerateFiles($filter, [System.IO.SearchOption]::AllDirectories) } | Sort-Object { $_.FullName })

    foreach($fileInfo in $fileInfos) {
        $filePath = [System.IO.Path]::GetRelativePath([System.IO.Path]::Combine($base, "Playlists"), $fileInfo.FullName)
        $filePath = $filePath.Replace("\", "/")
        #$filePath = $filePath.Replace(' ', "%20")
        $m3u8.WriteLine($filePath)
    }

    $m3u8.Close()

    Write-Host $playlistName
}

Build-Playlist "All Music" ("Tracks\Collections", "Tracks\Miscellaneous")

Build-Playlist "Billy Idol" ("Tracks\Collections\Billy Idol")
Build-Playlist "Depeche Mode" ("Tracks\Collections\Depeche Mode")
Build-Playlist "DEVO" ("Tracks\Collections\DEVO")
Build-Playlist "Enigma" ("Tracks\Collections\Enigma")
Build-Playlist "Enya" ("Tracks\Collections\Enya")
Build-Playlist "Jean-Michel Jarre" ("Tracks\Collections\Jean-Michel Jarre")
Build-Playlist "Kraftwerk" ("Tracks\Collections\Kraftwerk")
Build-Playlist "Logic System" ("Tracks\Collections\Logic System")
Build-Playlist "Lustans Lakejer" ("Tracks\Collections\Lustans Lakejer")
Build-Playlist "The Shamen" ("Tracks\Collections\The Shamen")
Build-Playlist "Wipeout" ("Tracks\Collections\CoLD SToRAGE", "Tracks\Collections\Wip3out")
Build-Playlist "Yazoo" ("Tracks\Collections\Yazoo", "Tracks\Collections\Yaz")
Build-Playlist "Yello" ("Tracks\Collections\Yello")

Build-Playlist "Miscellaneous" ("Tracks\Miscellaneous")

Build-Playlist "Personal Jesus" ("Tracks\Collections", "Tracks\Miscellaneous") "*Personal Jesus*.mp3"

Build-Playlist "Art Of War (Sun Tzu)" ("Books\Sun Tzu\Art of War")
Build-Playlist "Letting Go Of God (Julia Sweeney)" ("Books\Julia Sweeney\Letting Go Of God")
Build-Playlist "Meditations (Marcus Aurelius)" ("Books\Marcus Aurelius Antoninus\Meditations")
Build-Playlist "Moral Letters to Lucilius (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Lucilius Epistulae")
Build-Playlist "The Divine Comedy (Dante Alighieri)" ("Books\Dante Alighieri\The Divine Comedy")

Build-Playlist "Svensk Musik" ("Tracks\Collections\Adolphson & Falk", "Tracks\Collections\Gyllene Tider", "Tracks\Collections\Lustans Lakejer", "Tracks\Collections\Noice", "Tracks\Collections\Ratata")

Build-Playlist "SoundTracks" ("Tracks\SoundTracks")

Build-Playlist "Lindeman" ("Comedy") "*Lindeman*.mp3"

Build-Playlist "Talk & Comedy" ("Comedy")

Build-Playlist "X-mas Music" ("Tracks\X-mas")
