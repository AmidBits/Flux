namespace Flux.Model.Gaming.Engine
{
	public interface IObjectUpdatable
	{
		void InitializeObject();
		void UpdateObject(float deltaTime);
	}
}
