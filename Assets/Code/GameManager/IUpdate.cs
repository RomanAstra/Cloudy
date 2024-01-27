namespace Utils
{
    public interface IUpdate : IController
    {
        void OnUpdate(float deltaTime);
    }
}