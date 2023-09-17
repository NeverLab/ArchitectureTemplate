public interface IView<in T> where T : struct
{
    void Init(T dependency);
}