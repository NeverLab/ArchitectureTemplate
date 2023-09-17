using Game.GameStart;
using Game.Level;
using UnityEngine;

public class ViewProvider : MonoBehaviour
{
    //Пример передачи юнити контекста в бизнес логику. При разрастании необходимо декомпозировать по смыслу на подклассы
    [SerializeField] private GameObject _uiRoot;
    [SerializeField] private GameStartView _gameStartViewPrefab;
    [SerializeField] private LevelView _levelViewPrefab;
    public GameObject UiRoot => _uiRoot;
    public LevelView LevelViewPrefab => _levelViewPrefab;
    public GameStartView GameStartViewPrefab => _gameStartViewPrefab;
}