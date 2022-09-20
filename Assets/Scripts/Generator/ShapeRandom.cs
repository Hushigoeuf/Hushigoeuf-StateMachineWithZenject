using System.Linq;

namespace Hushigoeuf
{
    public class ShapeRandom
    {
        private readonly string[] ShapeIDs;
        private readonly IRandomService _randomService;

        public ShapeRandom(ShapeFactory.Settings settings, IRandomService randomService)
        {
            ShapeIDs = new string[settings.Prefabs.Length];
            _randomService = randomService;
            for (var i = 0; i < ShapeIDs.Length; i++)
                ShapeIDs[i] = settings.Prefabs[i].PlayerShape.ShapeID;
        }

        public string GetRandomShapeID() => _randomService.Choose(ShapeIDs.Length != 0 ? ShapeIDs : null);

        public string GetRandomShapeIDWithExclusion(string exclusion)
        {
            string[] shapeIDs = ShapeIDs.Where(ID => ID != exclusion).ToArray();
            if (shapeIDs.Length == 0) return null;
            return _randomService.Choose(shapeIDs);
        }
    }
}