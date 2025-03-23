namespace Flux.Model.GameEngine
{
	public interface IObjectUpdatable
	{
		void InitializeObject();
		void UpdateObject(float deltaTime);
	}
}
