using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class StoryPieceManager : MonoBehaviour
{
    [SerializeField] private List<StoryPiece> _storyPieces;
    private int _currentPlayingIndex;
    private int _amountOfLinesInStoryPiece;
    private int _amountOfLinesPlayed;
    private bool _isPlayingStoryPiece = false;
    public UnityEvent OnFinishedPlayingStoryPiece = new();

    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _currentLineText;
    [SerializeField] private TextMeshProUGUI _currentSpeakerNameText;
    [SerializeField] private Image _currentSpeakerImage;
    [SerializeField] private Image _backgroundImage;


    //bool tempHasPlayedIntro = false;
    bool tempHasPlayedIntro1 = false;
    bool tempHasPlayedIntro2 = false;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayStoryPiece("Intro");
        OnFinishedPlayingStoryPiece.AddListener(GoToNext);
    }

    private void GoToNext()
    {
        if(tempHasPlayedIntro1 && !tempHasPlayedIntro2)
        {
            tempHasPlayedIntro2 = true;
            PlayStoryPiece("Intro2");
        }
        else if(!tempHasPlayedIntro2)
        {
            tempHasPlayedIntro1 = true;
            PlayStoryPiece("Intro1");
        }
    }

    private void Update()
    {
        if (!_isPlayingStoryPiece) return;

        if(_audioSource.isPlaying == false) // Current Line Finished Playing
        {
            ++_amountOfLinesPlayed;
            OnLineFinished();
        }
    }

    public void PlayStoryPiece(string storyPieceName)
    {
        Debug.Log("Attempt To Play Story Piece With Name: " + storyPieceName);
        for(int storyPieceIdx =0; storyPieceIdx < _storyPieces.Count; ++storyPieceIdx)
        {
            if (_storyPieces[storyPieceIdx].name == storyPieceName)
            {
                _amountOfLinesInStoryPiece = _storyPieces[storyPieceIdx].voiceLines.Count;
                _amountOfLinesPlayed = 0;
                _isPlayingStoryPiece = true;
                _currentPlayingIndex = storyPieceIdx;
                _currentLineText.enabled = true;
                _currentSpeakerNameText.enabled = true;
                _currentSpeakerImage.enabled = true;
                _backgroundImage.enabled = true;
                PlayNextDialogueLine();
                break;
            }
        }
    }

    private void OnLineFinished()
    {
        if(_amountOfLinesPlayed >= _amountOfLinesInStoryPiece) // Story Piece Has Finished
        {
            _currentLineText.enabled = false;
            _currentSpeakerNameText.enabled = false;
            _currentSpeakerImage.enabled = false;
            _backgroundImage.enabled = false;
            _isPlayingStoryPiece = false;
            OnFinishedPlayingStoryPiece?.Invoke();
        }
        else // Go To Next Line Of Story Piece
        {
            PlayNextDialogueLine();
            // Change UI here as well
        }
    }
    
    private void PlayNextDialogueLine()
    {
        _audioSource.clip = _storyPieces[_currentPlayingIndex].voiceLines[_amountOfLinesPlayed].clip;
        _currentLineText.text = _storyPieces[_currentPlayingIndex].voiceLines[_amountOfLinesPlayed].subtitle;
        _currentSpeakerNameText.text = _storyPieces[_currentPlayingIndex].voiceLines[_amountOfLinesPlayed].speakerName;
        _audioSource.Play();
    }
}
