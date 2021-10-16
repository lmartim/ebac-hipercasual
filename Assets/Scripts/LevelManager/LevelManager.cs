using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;

    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelPiece();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            CreateLevelPiece();
    }

    #region LEVELS
    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
                ResetLevelIndex();
        }

        var currentLevel = Instantiate(levels[_index], container);
        currentLevel.transform.localPosition = Vector3.zero;
    }
    #endregion

    #region PIECES
    private void CreateLevelPiece()
    {
        CleanSpawnedPieces();

        if (_currSetup != null)
        {
            _index++;

            if (_index >= levelPieceBasedSetups.Count)
                ResetLevelIndex();
        }

        _currSetup = levelPieceBasedSetups[_index];

        ItiratePieces(_currSetup.piecesStartNumber, _currSetup.levelPiecesStart);
        ItiratePieces(_currSetup.piecesNumber, _currSetup.levelPieces);
        ItiratePieces(_currSetup.piecesEndNumber, _currSetup.levelPiecesEnd);

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
    }

    private void ItiratePieces(int piecesQtd, List<LevelPieceBase> list)
    {
        for (int i = 0; i < piecesQtd; i++)
            CreateLevelPiece(list);
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        } else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }
    #endregion

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }
}
