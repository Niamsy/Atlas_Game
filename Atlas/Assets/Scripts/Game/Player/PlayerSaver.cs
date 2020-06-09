using Game.SavingSystem;
using Game.SavingSystem.Datas;

namespace Player.Scripts
{
    public class PlayerSaver : MapSavingBehaviour
    {
        protected override void SavingMapData(MapData data)
        {
            data.TransformData.SetFromTransform(transform);
        }

        protected override void LoadingMapData(MapData data)
        {
            transform.position = data.TransformData.Position.Value;
            transform.rotation = data.TransformData.Rotation.Value;
        }
    }
}