namespace Flux.Model.Gaming.Engine
{
	public interface IObjectParent
	{
		System.Collections.Generic.IList<IObject> ChildObjects { get; }
	}
}
