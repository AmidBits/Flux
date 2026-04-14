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

function Create-PlaylistDirectory($playlistPathName)
{
    $playlistDirectoryInfo = (New-Object System.IO.FileInfo($playlistPathName)).Directory

    if(!$playlistDirectoryInfo.Exists)
    {
        $playlistDirectoryInfo.Create()
    }

    return $playlistDirectoryInfo;
}

function Create-PlaylistFile($playlistPathName)
{
    $playlistFileInfo = New-Object System.IO.FileInfo($playlistPathName)

    if(!$playlistFileInfo.Exists)
    {
        $sw = $playlistFileInfo.CreateText();
        
        $sw.WriteLine("#EXTM3U");
        #$sw.WriteLine("#$($playlistFileInfo.Name)");
        $sw.WriteLine("#PLAYLIST:$($playlistFileInfo.BaseName)");

        $sw.Close();
    }
}

function Create-Playlist($playlistPathName)
{
    $playlistDirectoryInfo = Create-PlaylistDirectory($playlistPathName)
    $playlistFileInfo = Create-PlaylistFile($playlistPathName)
}

function Get-CommonRoot([System.IO.FileInfo]$a, [System.IO.FileInfo]$b) {
    $p1 = $a.DirectoryName.Split([System.IO.Path]::DirectorySeparatorChar)
    $p2 = $b.DirectoryName.Split([System.IO.Path]::DirectorySeparatorChar)

    $common = for ($i = 0; $i -lt [System.Math]::Min($p1.Count, $p2.Count); $i++) {
        if ($p1[$i] -ne $p2[$i]) { break }
        $p1[$i]
    }

    $common -join [System.IO.Path]::DirectorySeparatorChar
}

function Append-PlaylistEntry($playlistPathName, $entryPathName)
{
    $playlistFileInfo = New-Object System.IO.FileInfo($playlistPathName)
    $entryFileInfo = New-Object System.IO.FileInfo($entryPathName)

    $commonRoot = Get-CommonPath $playlistFileInfo $entryFileInfo

    $entryRelativePathName = '..' + $entryFileInfo.FullName.Remove(0, $commonRoot.Length)
    $entryRelativePathName = $entryRelativePathName.Replace('\', '/')

    $playlistFileInfo = New-Object System.IO.FileInfo($playlistPathName)

    $sw = $playlistFileInfo.AppendText()
    $sw.WriteLine($entryRelativePathName)
    $sw.Close()
}

function Reset-Playlist($playlistPathName)
{
    $playlistDirectoryInfo = Create-PlaylistDirectory($playlistPathName)

    $playlistFileInfo = Create-PlaylistFile($playlistPathName)

    $playlistDirectoryInfo = $playlistFileInfo.Directory

    if(!$playlistDirectoryInfo.Exists)
    {
        $playlistDirectoryInfo.Create()
    }

    if($playlistFileInfo.Exists) {
        $playlistFileInfo.Delete();
    }

    if(!$playlistFileInfo.Exists) {
        $fs = $playlistFileInfo.Create();
        $fs.Close();
    }

    [System.ValueTuple]::Create($streamWriter, $playlistFileInfo, $playlistDirectoryInfo)
}

#Create-Playlist "E:\Media\Audio\Playlister\Test.m3u8"
#Append-PlaylistEntry "E:\Media\Audio\Playlister\Test.m3u8" "E:/Media/Audio/Playlister/Tracks/Collections/Enigma/MCMXC a.D/07 Back To The Rivers Of Belief - A- Way To Eternity B- Hallelujah C- The Rivers Of Belief.mp3"

#$tuple = Get-PlaylistInfo "E:\Media\Audio\Playlister\Test.m3u8"
#$tuple

#return;

function Read-Lines($path) { $sr = [System.IO.StreamReader]$path; while($l = $sr.ReadLine()) { $l }; $sr.Close() }

function Read-PlaylistPaths($path) { Read-Lines($path) | Where-Object { $_ -like "..*" } }

function Read-PathsInPlaylists([string[]]$playlists) { $playlists | ForEach-Object { Read-PlaylistPaths([string][System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$($_).m3u8")) } }

function Merge-Playlists([string]$playlist, [string[]]$playlists)
{
    $playlistFileInfo = [System.IO.FileInfo][System.IO.Path]::Combine($base, "Playlists\$playlist.m3u8")

    [System.IO.Directory]::GetParent($playlistFileInfo.FullName).Create()

    $playlistName = $playlistFileInfo.BaseName

    $m3u8 = $playlistFileInfo.CreateText();

    $m3u8.WriteLine("#EXTM3U");
    #$m3u8.WriteLine("#$($playlistFileInfo.Name)");
    $m3u8.WriteLine("#PLAYLIST:$playlistName");

    Read-PathsInPlaylists($playlists) | Sort-Object -Unique | ForEach-Object { $m3u8.WriteLine($_) }

    $m3u8.Close()

    Write-Host "Merge-PlayLists: $playlistName ($([System.String]::Join(', ', $playlists)))"
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

Build-Playlist "All Music" ("Tracks\Miscellaneous", "Tracks\Collections", "Tracks\Classical", "Tracks\MediaTracks")

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
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/E-Type") "E-Type - When Religion Comes to Town.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "01 Sweet Dreams (Are Made Of This) (12' Version).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Faithless/Reverence") "06 Insomnia.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/Hau Ruck 2025") "04 New American Century 2025.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM") "KMFDM - Megalomaniac.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM") "KMFDM - Ready To Blow.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM") "KMFDM - Stray Bullet.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/Krank (Single)") "05 Day of Light (247 Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/Let Go") "03 Next Move.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/Let Go") "06 Touch.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/MDFMK (Single)") "01 Megalomaniac (Bomb).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/MDFMK (Single)") "05 Anarchy (God and the State Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/WWIII") "10 Revenge.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rob Zombie/Hellbilly Deluxe") "02 Superbeast.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rob Zombie/Hellbilly Deluxe") "09 Meet The Creeper.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/MediaTracks/Matrix, The") "Rammstein - Du Hast.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/MediaTracks/Matrix, The") "Rob D - Clubbed to Death (Kurayamino Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/MediaTracks/Matrix, The") "Rob Zombie - Dragula (Hot Rod Herman Remix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Miscellaneous")
