namespace Flux.Model.Gaming
{
	public interface IGameObject
	{
		public System.Guid Identity { get; }

		public string Name { get; }
	}

	public interface IObjectRenderable
	{
		void RenderObject();
	}
	
	public interface IObjectHierarchical
	{
		System.Collections.Generic.IList<IGameObject> ChildObjects { get; }
	}

	public interface IObjectTransformable
	{
		System.Numerics.Vector3 ObjectLocation { get; set; }
		System.Numerics.Quaternion ObjectRotation { get; set; }
		System.Numerics.Vector3 ObjectScale { get; set; }
	}

	public interface IObjectUpdatable
	{
		void InitializeObject();
		void UpdateObject(float deltaTime);
	}
}
