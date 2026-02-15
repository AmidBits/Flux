Clear-Host

$base = [System.IO.DirectoryInfo]"E:\Media\Audio"

function Add-ToPlaylist([string]$playlist, [string[]]$directories, [string]$filter = "*.mp3")
{
    $playlistFileInfo = [System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$playlist.m3u8")

    [System.IO.Directory]::GetParent($playlistFileInfo.FullName).Create()

    $playlistName = $playlistFileInfo.BaseName

    $m3u8 = $playlistFileInfo.AppendText();

    $fileInfos = ($directories | ForEach-Object { $directory = [System.IO.DirectoryInfo][System.IO.Path]::Combine($base, $_); $directory.EnumerateFiles($filter, [System.IO.SearchOption]::AllDirectories) } | Sort-Object { $_.FullName })

    foreach($fileInfo in $fileInfos) {
        $filePath = [System.IO.Path]::GetRelativePath([System.IO.Path]::Combine($base, "Playlists"), $fileInfo.FullName)
        $filePath = $filePath.Replace("\", "/")
        #$filePath = $filePath.Replace(' ', "%20")
        $m3u8.WriteLine($filePath)
    }

    $m3u8.Close()

    Write-Host "Add-ToPlaylist: $playlistName $directories $filter"
}

function Build-Playlist([string]$playlist, [string[]]$directories, [string]$filter = "*.mp3")
{
    $playlistFileInfo = [System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$playlist.m3u8")

    [System.IO.Directory]::GetParent($playlistFileInfo.FullName).Create()

    $playlistName = $playlistFileInfo.BaseName

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

    Write-Host "Build-Playlist: $playlistName"
}

function New-Playlist([string]$playlist)
{
    $playlistFileInfo = [System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$playlist.m3u8")

    [System.IO.Directory]::GetParent($playlistFileInfo.FullName).Create()

    $playlistName = $playlistFileInfo.BaseName

    $m3u8 = $playlistFileInfo.CreateText();

    $m3u8.WriteLine("#EXTM3U");
    #$m3u8.WriteLine("#$($playlistFileInfo.Name)");
    $m3u8.WriteLine("#PLAYLIST:$playlistName");

    $m3u8.Close()

    Write-Host "New-PlayList: $playlistName"
}

Build-Playlist "Billy Idol" ("Tracks\Collections\Billy Idol")
Build-Playlist "Depeche Mode" ("Tracks\Collections\Depeche Mode")
Build-Playlist "DEVO" ("Tracks\Collections\DEVO")
Build-Playlist "Enigma" ("Tracks\Collections\Enigma")
Build-Playlist "Enya" ("Tracks\Collections\Enya")
Build-Playlist "Jean-Michel Jarre" ("Tracks\Collections\Jean-Michel Jarre")
Build-Playlist "KMFDM" ("Tracks\Collections\KMFDM")
Build-Playlist "Kraftwerk" ("Tracks\Collections\Kraftwerk")
Build-Playlist "Logic System" ("Tracks\Collections\Logic System")
Build-Playlist "Lustans Lakejer" ("Tracks\Collections\Lustans Lakejer")
Build-Playlist "The Shamen" ("Tracks\Collections\The Shamen")
Build-Playlist "Wipeout" ("Tracks\MediaTracks\CoLD SToRAGE", "Tracks\MediaTracks\Wip3out")
Build-Playlist "Yazoo" ("Tracks\Collections\Yazoo", "Tracks\Collections\Yaz")
Build-Playlist "Yello" ("Tracks\Collections\Yello")
Build-Playlist "Zombies" ("Tracks\Collections\Rob Zombie", "Tracks\Collections\White Zombie")

Build-Playlist "All Music" ("Tracks\Collections", "Tracks\MediaTracks", "Tracks\Miscellaneous")

Build-Playlist "Collections" ("Tracks\Collections")

Build-Playlist "MediaTracks" ("Tracks\MediaTracks")

Build-Playlist "Miscellaneous" ("Tracks\Miscellaneous")

Build-Playlist "Personal Jesus" ("Tracks\Collections", "Tracks\MediaTracks", "Tracks\Miscellaneous") "*Personal Jesus*.mp3"

$currentPlaylistName = "Svensk Musik"
New-Playlist $currentPlaylistName
 Add-ToPlaylist $currentPlaylistName ("Tracks\Collections\Adolphson & Falk", "Tracks\Collections\Alf Robertson", "Tracks\Collections\Freestyle", "Tracks\Collections\Gyllene Tider", "Tracks\Collections\Lustans Lakejer", "Tracks\Collections\Magnum Bonum", "Tracks\Collections\Noice", "Tracks\Collections\Ratata")
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Björn Rosenström - *.mp3"
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Bo Kaspers Orkester - *.mp3"
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Nasa - *.mp3"
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Niels Jensen - *.mp3"
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Onkel Konkel - *.mp3"
 Add-ToPlaylist $currentPlaylistName ("Tracks\Miscellaneous") "Page - *.mp3"

Build-Playlist "Lindeman" ("Comedy") "*Lindeman*.mp3"

Build-Playlist "Talk & Comedy" ("Comedy")

Build-Playlist "X-mas" ("Tracks\X-mas")

Build-Playlist "Agamemnon (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Agamemnon")
Build-Playlist "Always Looking Up (Michael J. Fox)" ("Books\Michael J. Fox\Always Looking Up")
Build-Playlist "Art Of War (Sun Tzu)" ("Books\Sun Tzu\Art of War")
Build-Playlist "George Washington (J. Ellis)" ("Books\Joseph J. Ellis\His Excellency - George Washington")
Build-Playlist "Letting Go Of God (Julia Sweeney)" ("Books\Julia Sweeney\Letting Go Of God")
Build-Playlist "Medea (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Medea (Version 2)")
Build-Playlist "Meditations (Marcus Aurelius)" ("Books\Marcus Aurelius Antoninus\Meditations")
Build-Playlist "Moral Letters to Lucilius (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Lucilius Epistulae")
Build-Playlist "Oedipus (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Oedipus")
Build-Playlist "The Divine Comedy (Dante Alighieri)" ("Books\Dante Alighieri\The Divine Comedy")
Build-Playlist "Thyestes (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Thyestes")
Build-Playlist "Troades (Seneca the Younger)" ("Books\Lucius Annaeus Seneca the Younger\Troades")

# Super-custom!

$currentPlaylistName = "All Robs Favorites"

New-Playlist $currentPlaylistName

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A Flock Of Seagulls") "A Flock Of Seagulls - I Ran (So Far Away).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A Flock Of Seagulls\20 Classics Of The '80s") "11 Messages.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Scoundrel Days") "01 Scoundrel Days.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Scoundrel Days") "06 Cry Wolf.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Hunting High & Low") "04 The Blue Sky.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Hunting High & Low") "06 The Sun Always Shines On T.V.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A-Teens") "A-Teens - Schools Out.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A-Teens") "A-Teens - The Letter.wma"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "06 Adam In Chains.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "07 Neuromancer.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "16 Venus.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol, Don't Need A Gun (Single Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol. Flesh For Fantasy.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol, Rebel Yell.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol, Sweet Sixteen.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Vital Idol") "01 White Wedding, Pts. 1 & 2 (Shot Gun Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Vital Idol") "03 Flesh For Fantasy (Below The Belt Mix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/David Bowie/Changesbowie") "15 Let's Dance.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/David Bowie") "David Bowie - Cat People (Putting Out Fire).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Black Celebration") "01 Black Celebration.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Get The Balance Right! (Single)") "03 Get The Balance Right! (Combination Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Playing The Angel") "02 John The Revelator.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Some Great Reward") "02 Lie To Me.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Some Great Reward") "09 Blasphemous Rumours.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Sounds Of The Universe") "02 Hole To Feed.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Sounds Of The Universe") "03 Wrong.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode") "Personal Jesus (Brat Master Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/The Singles 81-85") "14 Shake The Disease.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Ultra") "04 It's No Good.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Violator") "01 World In My Eyes.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Depeche Mode/Violator") "03 Personal Jesus.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Oh, No! It's Devo-Freedom Of Choice") "02 Peek-A-Boo.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Oh, No! It's Devo-Freedom Of Choice") "05 That's Good.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Oh, No! It's Devo-Freedom Of Choice") "07 Big Mess.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Oh, No! It's Devo-Freedom Of Choice") "14 Whip It.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Something For Everybody") "06 Human Rocket.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Something For Everybody") "10 Later Is Now.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/DEVO/Something For Everybody") "11 No Place Like Home.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "07 Hungry Like The Wolf.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "08 Girls On Film.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "13 Notorious.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/E-Type") "E-Type - This is the Way.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Chemical Brothers") "Chemical Brothers - Come With Us.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Miscellaneous")
