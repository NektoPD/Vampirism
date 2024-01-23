using UnityEngine;

[RequireComponent(typeof(EnemyView))]
[RequireComponent(typeof(EnemyPlayerFinder))]
public class EnemyWaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Transform[] _targets;
    private EnemyView _view;
    private int _currentPosition;
    private float _speed = 3;
    private EnemyPlayerFinder _playerFinder;

    private void Awake()
    {
        _playerFinder = GetComponent<EnemyPlayerFinder>();
        _view = GetComponent<EnemyView>();

        _targets = new Transform[_path.childCount];

        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Transform target = _targets[_currentPosition];

        if (!_playerFinder.TargetFound) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.position) < 1f)
            {
                _view.Flip();
                MoveToNextTarget();
            }
        }
    }

    private void MoveToNextTarget()
    {
        _currentPosition++;

        if (_currentPosition >= _targets.Length)
        {
            _currentPosition = 0;
        }
    }
}
