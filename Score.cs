using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Text scoreText;

    public static string score = "0";
    public float timeScore, timeReduction;

    private void Start()
    {
        timeScore = 0f;
        timeReduction -= 1f;
    }
    // Update is called once per frame
    void Update()
    {
        score = timeScore.ToString("0");
        scoreText.text = score;  //get value from time AT this exact moment
        timeScore += -timeReduction * Time.deltaTime;
        // increase the timeScore with +(-timereduction)
        // which equals to timeScore = timeScore + -(-1) hence timeScore = timeScore + 1;
    }
}
