namespace Flux
{
	public static partial class ExtensionMethods
	{
		public static void DeleteContent(this System.IO.DirectoryInfo directoryInfo)
		{
			directoryInfo.DeleteDirectories(@"*", System.IO.SearchOption.TopDirectoryOnly, true);
			directoryInfo.DeleteFiles(@"*", System.IO.SearchOption.TopDirectoryOnly);
		}

		public static void DeleteDirectories(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption, bool recursiveDelete)
		{
			if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

			foreach (var directory in directoryInfo.EnumerateDirectories(searchPattern, searchOption))
				directory.Delete(recursiveDelete);
		}
		public static void DeleteFiles(this System.IO.DirectoryInfo directoryInfo, string searchPattern, System.IO.SearchOption searchOption)
		{
			if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

			foreach (var file in directoryInfo.EnumerateFiles(searchPattern, searchOption))
				file.Delete();
		}

		public static System.Collections.Generic.IEnumerable<System.IO.FileInfo> DirectorySearch(this System.IO.DirectoryInfo directoryInfo, System.Func<System.IO.FileInfo, bool> predicateFile, System.Func<System.IO.DirectoryInfo, bool> predicateDirectory)
		{
			if (directoryInfo is null) throw new System.ArgumentNullException(nameof(directoryInfo));

			if (directoryInfo.Exists)
			{
				System.IO.FileInfo[] fileInfos = System.Array.Empty<System.IO.FileInfo>();

				try { fileInfos = directoryInfo.GetFiles(); }
        catch { }

        foreach (var fi in fileInfos)
				{
					if (predicateFile?.Invoke(fi) ?? true)
					{
						yield return fi;
					}
				}

				System.IO.DirectoryInfo[] directoryInfos = System.Array.Empty<System.IO.DirectoryInfo>();

				try { directoryInfos = directoryInfo.GetDirectories(); }
        catch { }

        foreach (var di in directoryInfos)
				{
					if (predicateDirectory?.Invoke(di) ?? true)
					{
						foreach (var fi in DirectorySearch(di, predicateFile, predicateDirectory!))
						{
							yield return fi;
						}
					}
				}
			}
		}
	}
}
