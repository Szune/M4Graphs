namespace M4Graphs.Core.Interfaces
{
    public interface IRenderer<in TModelType>
    {
        void RenderElements(TModelType model);
    }
}
