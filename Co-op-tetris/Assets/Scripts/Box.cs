﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    [SerializeField] private GameObject _outlineObject;

    private GameBoard _gameBoard;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gameBoard = FindObjectOfType<GameBoard>();
    }

    internal bool CanMoveDown
        => !_gameBoard.TileIsOccupied(new Vector2Int((int) transform.position.x, (int) transform.position.y - 1));

    internal void Activate(Transform parentTetromino, Vector2 localPosition, char colorLetter)
    {
        transform.SetParent(parentTetromino);
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

    internal void HighlightBox(bool highlight)
    {
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