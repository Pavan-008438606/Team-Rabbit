using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Number : MonoBehaviour
{
    public TMPro.TextMeshProUGUI questionText;
    public TMPro.TextMeshProUGUI[] answerTexts;
    public GameObject congratsScene;
    public GameObject tryAgainScene;

    private int correctAnswer;
    private int[] answerOptions = new int[4];
    private int correctAnswersCount = 0;
    private int totalQuestions = 5; // You can change this to the desired number of questions

    void Start()
    {
        GenerateQuestion();
        GenerateAnswerOptions();
    }

    void GenerateQuestion()
    {
        // Generate two random numbers
        int number1 = Random.Range(1, 10);
        int number2 = Random.Range(1, 10);

        // Calculate the correct answer
        correctAnswer = number1 * number2;

        // Display the question in the question panel
        questionText.text = "   " + number1 + " * " + number2;
    }

    void GenerateAnswerOptions()
    {
        // Randomly select a panel for the correct answer
        int correctAnswerPanelIndex = Random.Range(0, answerTexts.Length);

        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i == correctAnswerPanelIndex)
            {
                // Set the correct answer in the selected panel
                answerTexts[i].text = correctAnswer.ToString();
            }
            else
            {
                // Generate unique incorrect answers for the other panels
                int randomIncorrectAnswer = Random.Range(correctAnswer - 1, correctAnswer + 20);

                // Ensure that incorrect answers are unique and not the same as the correct answer
                while (randomIncorrectAnswer == correctAnswer || System.Array.Exists(answerOptions, element => element == randomIncorrectAnswer))
                {
                    randomIncorrectAnswer = Random.Range(correctAnswer - 1, correctAnswer + 20);
                }

                answerTexts[i].text = randomIncorrectAnswer.ToString();

                // Store the generated incorrect answer in the answerOptions array to prevent duplicates
                answerOptions[i] = randomIncorrectAnswer;
            }
        }
    }



    public void CheckAnswer(TMPro.TextMeshProUGUI selectedAnswerText)
    {
        int selectedAnswer = int.Parse(selectedAnswerText.text);

        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct Answer!");
            correctAnswersCount++;

            if (correctAnswersCount == totalQuestions)
            {
                Debug.Log("All questions answered correctly!");
                // Load the congrats scene
                SceneManager.LoadScene("congratsScene");
            }
            else
            {
                // Generate a new question
                GenerateQuestion();
                GenerateAnswerOptions();
            }
        }
        else
        {
            Debug.Log("Wrong Answer!");
            // Wrong answer, show try again scene
            SceneManager.LoadScene("tryAgainScene");
        }
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}