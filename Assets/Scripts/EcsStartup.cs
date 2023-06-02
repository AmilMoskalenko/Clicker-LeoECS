using Leopotam.Ecs;
using Settings;
using Systems;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private ConfigNames _configNames;
    [SerializeField] private ConfigValues _configValues;
    [SerializeField] private SavedKeys _savedKeys;
    [SerializeField] private SpawnData _spawnData;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;
 
    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);
    	
        _systems
            .Add(new BalanceSpawnSystem())
            .Add(new BusinessSpawnSystem())
            .Add(new BusinessInitSystem())
            .Add(new BusinessRunSystem())
            .Inject(_configNames)
            .Inject(_configValues)
            .Inject(_savedKeys)
            .Inject(_spawnData)
            .Init();
    }
 
    private void FixedUpdate()
    {
        _systems?.Run();
    }
 
    private void OnDestroy()
    {
        _systems?.Destroy();
        _systems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}
