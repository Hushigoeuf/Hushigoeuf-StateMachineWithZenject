using Zenject;

namespace Hushigoeuf
{
    public class ShapeGenerator : IInitializable
    {
        [Inject] private readonly ShapeMemory _memory;
        [Inject] private readonly ShapeRandom _shapeRandom;
        [Inject] private readonly SignalBus _signalBus;

        public void Initialize()
        {
            GenerateNewMemory();
        }

        public void GenerateNewMemory()
        {
            string randomShapeID = _shapeRandom.GetRandomShapeIDWithExclusion(_memory.ShapeID);

            _memory.Index++;
            _memory.ShapeID = randomShapeID;

            _signalBus.Fire(new GenerateNewMemorySignal() {ShapeID = randomShapeID});
        }
    }

    public class GenerateNewMemorySignal
    {
        public string ShapeID;
    }
}