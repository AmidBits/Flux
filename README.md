# Flux

This is sort of an arc API. 

The original intent was to eventually create a collection of smaller projects that could be referenced individually, but we'll see.

There are also sample solutions for usage referencing, e.g. reference by project. 

Also, over time hierarchies of projects became more advantageous, e.g. BaseLibrary -> WindowsLibrary, since the latter is specialized and the former is cross platform.

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
