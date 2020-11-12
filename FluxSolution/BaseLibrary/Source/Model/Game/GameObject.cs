namespace Flux.Model.Gaming
{
	public interface IGameObject
	{
		public System.Guid Identity { get; }
	
		public string Name { get; }
	}

	public interface IObjectTimeLinear
	{
		void InitializeObject();
		void UpdateObject(float deltaTime);
	}

	public interface IObjectSubHierarchical
	{
		System.Collections.Generic.List<IGameObject> ChildObjects { get; }
	}

	public interface IObjectTransformable
	{
		System.Numerics.Vector3 ObjectLocation { get; set; }
		System.Numerics.Quaternion ObjectRotation { get; set; }
		System.Numerics.Vector3 ObjectScale { get; set; }
	}
}
