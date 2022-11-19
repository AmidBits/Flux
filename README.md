# Flux

This is sort of an life arc API. Most code did not originate from C# and was simple transcribed from original code. If the code was not written by me, there should be a blurb or link to the origin (e.g. wikipedia.com).

The original intent was to eventually create a collection of smaller projects that could be referenced individually, but we'll see to what degree this will be desired.

Also, over time hierarchies of projects became more advantageous, e.g. 'BaseLibrary referenced by Modeling' and then 'Modeling referenced by WindowsLibrary' and finally 'WindowsLibrary referenced by WpfApp'. This way, more paths can be obtained for various purpose without micro-managing projects.

If using Visual Studio you can also directly reference files external to your project, by editing the project file, e.g.:

	<ItemGroup>
		<Compile Include="..\Flux\FluxSolution\BaseLibrary\Source\**\*.cs">
			<Link>%(RecursiveDir)%(Filename)%(Extension)</Link>
			<Visible>True</Visible>
		</Compile>
		<Content Include="..\Flux\FluxSolution\BaseLibrary\Resources\**\*.*">
			<Link>%(RecursiveDir)%(Filename)%(Extension)</Link>
			<Visible>True</Visible>
		</Content>
	</ItemGroup>
