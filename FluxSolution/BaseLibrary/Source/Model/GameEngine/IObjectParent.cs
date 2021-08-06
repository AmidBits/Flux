namespace Flux.Model.GameEngine
{
	public interface IObjectParent
	{
		System.Collections.Generic.IList<IObject> ChildObjects { get; }
	}
}
