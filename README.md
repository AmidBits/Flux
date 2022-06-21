# Flux

As the name implies, this is an API and a solution used for source file reference inside of other projects. 

If using Visual Studio you can reference files external to your project, by editing the project file. E.g.:

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
