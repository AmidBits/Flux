# Flux

As the name implies, this is an API.

There are also sample solutions for usage referencing, e.g. reference by project. 

If using Visual Studio you can also reference files external to your project, by editing the project file:

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
