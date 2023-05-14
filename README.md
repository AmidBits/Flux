# Flux

This is sort of an life arc API. Meaning it's code accumulation from over 40 years of programming.

Some code is probably no longer applicable or just plain unnecessary (especially with better API surfaces, e.g. .NET). Much code originates from really old coding projects, from other languages, e.g. assembler, C/C++, etc., and was simply transcribed from such. Some code originate from Wikipedia. If the code was not written by me, there should be a blurb or link to the origin. Many topics have links to Wikipedia, since the information already exists. I also noticed that some links to origins no longer exists, and I either removed them or let them be.

This specific project has been in production for over 20 years on local repos.

The original intent was to eventually create a collection of smaller projects that could be referenced individually, but we'll see to what degree this will be desired.

Over time, hierarchies of projects seemed more advantageous, e.g. 'BaseLibrary referenced by Modeling' and then 'Modeling referenced by WindowsLibrary' and finally 'WindowsLibrary referenced by WpfApp'. This way, more paths can be obtained for various purpose without micro-managing projects.

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
