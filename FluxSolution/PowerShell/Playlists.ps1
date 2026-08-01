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

Build-Playlist "All Music (Incl. Auggies)" ("Tracks\Auggie", "Tracks\Miscellaneous", "Tracks\Collections", "Tracks\Classical", "Tracks\MediaTracks")

Build-Playlist "Auggies Music" ("Tracks\Auggie")

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

$currentPlaylistName = "All Favorites"

New-Playlist $currentPlaylistName

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A Flock Of Seagulls") "A Flock Of Seagulls - I Ran (So Far Away).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A Flock Of Seagulls\20 Classics Of The '80s") "11 Messages.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Scoundrel Days") "01 Scoundrel Days.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Scoundrel Days") "06 Cry Wolf.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Hunting High & Low") "04 The Blue Sky.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/a-ha/Hunting High & Low") "06 The Sun Always Shines On T.V.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A-Teens") "A-Teens - Schools Out.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/A-Teens") "A-Teens - The Letter.wma"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Bananarama")

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "06 Adam In Chains.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "07 Neuromancer.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "16 Venus.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "17 Then The Night Comes.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Cyberpunk") "19 Mother Dawn.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol, Don't Need A Gun (Single Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol. Flesh For Fantasy.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Greatest Hits") "Billy Idol, Rebel Yell.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Whiplash Smile") "04 Sweet Sixteen.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Billy Idol/Vital Idol") "01 White Wedding, Pts. 1 & 2 (Shot Gun Mix).mp3"

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

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "01 Is There Something I Should Know.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "02 The Reflex.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "03 A View To A Kill.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "06 Rio (US Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "07 Hungry Like The Wolf.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "08 Girls On Film.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "10 Union Of The Snake.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "12 Wild Boys.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "13 Notorious.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Duran Duran/Greatest") "15 All She Wants Is (45 Mix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/E-Type") "E-Type - This is the Way.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/E-Type") "E-Type - When Religion Comes to Town.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/The Innocents") "03 Phantom Bride.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/The Innocents") "09 Imagination.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/The Circus") "06 Victim Of Love.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/The Circus") "07 Leave Me To Bleed.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/The Circus") "09 The Circus.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Erasure/Cowboy") "07 Treasure.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "01 Sweet Dreams (Are Made Of This) (12' Version).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "02 When Tomorrow Comes.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "03 Here Comes The Rain Again (12' Version).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "04 Who's That Girl (Short Version).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Eurythmics/Greatest Hits") "08 Missionary Man (7' Version).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Fad Gadget") "Fad Gadget - Collapsing New People.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Fad Gadget") "Fad Gadget - Love Parasite.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Faithless/Reverence") "06 Insomnia.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Howard Jones/The Best Of Howard Jones") "02 New Song.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Howard Jones/The Best Of Howard Jones") "06 Like To Get To Know You Well.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Howard Jones/The Best Of Howard Jones") "09 Hide And Seek.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Howard Jones/The Best Of Howard Jones") "12 The Prisoner.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/KMFDM/Enemy") "01 Enemy.mp3"
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

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Logic System/LOGIC") "02 Unit.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Logic System/LOGIC") "06 Talk Back.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Logic System/LOGIC") "08 Person to Person.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Logic System/To Gen Kyo") "01 Rydeen.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Uppdrag i Genève") "02 En Lång Natts Färd Mot Dag.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Uppdrag i Genève") "03 Sista Tangon i Paris.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Uppdrag i Genève") "04 Segerns Sötma.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Spotlight") "02 En Främlings Ögon.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Spotlight") "03 Man Lever Bara Två Gånger.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Spotlight") "03 Sista Tangon i Paris.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Lustans Lakejer/Spotlight") "06 Läppar Tiger Ögon Talar.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Madonna")

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Marilyn Manson/Lest We Forget") "12 Rock Is Dead.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Men Without Hats") "Men Without Hats - You Can Dance If You Want To.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Michael Jackson")

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/New Order/Blue Monday 1983 (Single)") "Blue Monday 1983 (12' Mix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Nomad/Songman") "01 With You.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Nomad/Songman") "03 Kava.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Nomad/Songman") "08 Garpi.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Peter Gabriel") "Peter Gabriel - Big Time.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Peter Gabriel") "Peter Gabriel - Sledgehammer.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Propaganda") "Propaganda - P-Machinery.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Queen")

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rammstein/Reise, Reise") "02 Mein Teil.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rammstein/Sehnsucht") "05 Du Hast.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rammstein") "Rammstein - Deutschland.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rammstein") "Rammstein - Feuer Frei.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rob Zombie/Hellbilly Deluxe") "02 Superbeast.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rob Zombie/Hellbilly Deluxe") "09 Meet The Creeper.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Rob Zombie") "Rob Zombie - Dragula (Hot Rod Herman Remix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Robert Palmer/Addictions, Vol. 1") "01 Bad Case Of Loving You (Doctor, Doctor).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Robert Palmer/Addictions, Vol. 1") "12 Simply Irresistible.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Roxette") "Roxette - The Look.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Simple Minds/Glittering Prize 81-92") "01 Waterfront.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Simple Minds/Glittering Prize 81-92") "05 Love Song.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Simple Minds") "Simple Minds - New Gold Dream.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Simple Minds") "Simple Minds - Up On The Catwalk.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Smash Mouth") "All Star.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Talk Talk/Natural History- The Very Best Of Talk Talk") "01 Today.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Talk Talk/Natural History- The Very Best Of Talk Talk") "02 Talk Talk.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Talk Talk/Natural History- The Very Best Of Talk Talk") "03 My Foolish Friend.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Talk Talk/Natural History- The Very Best Of Talk Talk") "04 Such A Shame.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Talk Talk/Natural History- The Very Best Of Talk Talk") "06 It's My Life.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Tears For Fears/Tears Roll Down (Greatest Hits 82-92)") "01 Sowing The Seeds Of Love.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Tears For Fears/Tears Roll Down (Greatest Hits 82-92)") "02 Everybody Wants To Rule The World.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Tears For Fears/Tears Roll Down (Greatest Hits 82-92)") "04 Shout.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Tears For Fears/Tears Roll Down (Greatest Hits 82-92)") "11 Change.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Crystal Method") "The Crystal Method - High Roller.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Prodigy/The Fat Of The Land") "06 Mindfields.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Prodigy/The Fat Of The Land") "08 Firestarter.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/Different Drum") "01 Boss Drum (Beatmasters Radio Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/Different Drum") "02 L.S.I. (Beat Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/Different Drum") "03 Ebeneezer Goode (Beat Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/Different Drum") "04 Comin' On Strong (Beatmasters 7').mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/Different Drum") "05 Phorever People (Beatmasters Heavenly Edit).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/The Shamen/En-Tact") "01 Move Any Mountain.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Ultravox/The Collection") "01 Dancing With Tears In My Eyes.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Ultravox/The Collection") "11 We Came To Dance.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Ultravox/The Collection") "13 Love's Great Adventure.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/Flag") "05 The Race.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/Point") "01 Waba Duba.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/Pocket Universe") "04 On Track.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/The Eye") "01 Planet Dada.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/The Eye") "02 Nervous.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/The Eye") "06 Tiger Dust.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/Tremendous Pain (Suite 904)") "03 Suite 904 (Bible Mix).mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/Yello/Zebra") "03 Night Train.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/ZZ Top") "ZZ Top - Delirious.mp3"
Add-ToPlaylist $currentPlaylistName ("Tracks/Collections/ZZ Top") "ZZ Top - Planet Of Women.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/MediaTracks/Gone In 60 Seconds") "Too Sick to Pray.mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/MediaTracks/Matrix, The") "Rob D - Clubbed to Death (Kurayamino Mix).mp3"

Add-ToPlaylist $currentPlaylistName ("Tracks/Miscellaneous")
