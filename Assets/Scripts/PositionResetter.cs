using Assets.TestFolder;
using UnityEngine;
using Zenject;

public class PositionResetter : MonoBehaviour
{
    private CameraTest _player;
    [Inject]
    private void Construct(CameraTest player)
    {
        _player = player;
    }

    [ContextMenu(nameof(ParentPlayer))]
    public void ParentPlayer()
    {
        transform.position = _player.transform.position;
        _player.transform.parent = this.transform;
    }
    
}
