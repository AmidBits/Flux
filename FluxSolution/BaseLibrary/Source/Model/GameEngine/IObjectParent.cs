namespace Flux.Model.GameEngine
{
	public interface IObjectParent
	{
		System.Collections.Generic.List<IObject> ChildObjects { get; }
	}
}
