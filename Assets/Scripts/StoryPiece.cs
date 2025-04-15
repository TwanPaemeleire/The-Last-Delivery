using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StoryPiece", menuName = "Story Piece")]
public class StoryPiece : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class VoiceLine
    {
        public string speakerName;
        public string subtitle;
        public AudioClip clip;
        public Transform speakerTransform;
    }

    public List<VoiceLine> voiceLines = new List<VoiceLine>();
}
