using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Listiners")]
    [SerializeField] private AllEntitiesContainer entitiesContainer;

    private int currentScore;

    private void Start()
    {
        UpdateScore();
        FollowPoolsEvents(entitiesContainer.AsteroidsPools);
        FollowUFOEvent(entitiesContainer.UFO);
    }

    private void FollowPoolsEvents(List<AsteroidPool> asteroidsPools)
    {
        foreach (var pool in asteroidsPools)
        {
            pool.OnDestroyEvent += GetBodyValue;
        }
    }

    private void FollowUFOEvent(UFO ufo)
    {
        ufo.OnDestroyEvent += GetBodyValue;
    }

    public void GetBodyValue(SpaceBodyController body)
    {
        if (body.gameObject.TryGetComponent<IScorable>(out IScorable score))
        {
            ChangeScore(score.Value);
        }
    }

    public void ChangeScore(int value)
    {
        currentScore += value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = currentScore.ToString();
    }
}
