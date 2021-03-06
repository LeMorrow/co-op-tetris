﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    internal Tetromino ParentTetromino { get; private set; }

    [SerializeField] private GameObject _outlineObject;

    private GameBoard _gameBoard;

    private SpriteRenderer _outlineSpriteRenderer;
    private SpriteRenderer _spriteRenderer;

    internal void Initialize(GameBoard gameBoard)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _outlineSpriteRenderer = _outlineObject.GetComponent<SpriteRenderer>();
        _gameBoard = gameBoard;
    }

    internal bool CanMoveInDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return !_gameBoard.TileIsOccupied(new Vector2Int((int) transform.position.x, (int) transform.position.y - 1));

            case Direction.Left:
                return !_gameBoard.TileIsOccupied(new Vector2Int((int) transform.position.x - 1, (int) transform.position.y));

            case Direction.Right:
                return !_gameBoard.TileIsOccupied(new Vector2Int((int) transform.position.x + 1, (int) transform.position.y));

            default: throw new System.Exception($"Direction '{direction}' is not implemented.");
        }
    }

    internal void Activate(Transform parentTetromino, Vector2 localPosition, char colorLetter)
    {
        transform.SetParent(parentTetromino);
        ParentTetromino = parentTetromino.GetComponent<Tetromino>();

        transform.localPosition = localPosition;
        _spriteRenderer.color = GetColorFromLetter(colorLetter);
        gameObject.SetActive(true);
    }

    internal void Deactivate()
    {
        if (_gameBoard == null) { _gameBoard = FindObjectOfType<GameBoard>(); }

        transform.SetParent(_gameBoard.transform);
        gameObject.SetActive(false);
    }

    internal void HighlightBox(bool highlight, Color color = default)
    {
        if (color != default)
        {
            _outlineSpriteRenderer.color = color;
        }

        _outlineObject.SetActive(highlight);
        _spriteRenderer.sortingLayerName = highlight ? "Selected Box" : "Default";
    }

    private Color GetColorFromLetter(char letter)
    {
        switch (letter)
        {
            case 'C': return Color.cyan;
            case 'B': return Color.blue;
            case 'O': return new Color32(232, 144, 0, 255); // Orange
            case 'Y': return Color.yellow;
            case 'G': return Color.green;
            case 'R': return Color.red;
            case 'P': return new Color32(140, 50, 255, 255); // Purple

            default: throw new System.Exception($"There is no color that corresponds with the letter '{letter}'");
        }
    }
}
