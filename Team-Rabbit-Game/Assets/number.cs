using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;


public class Number : MonoBehaviour
{
    [SerializeField] private Animator m_RabbitAnim;
    
    [SerializeField] TMPro.TextMeshProUGUI questionText;
    [SerializeField] TMPro.TextMeshProUGUI[] answerTexts;
    [SerializeField] GameObject carrot;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    // public GameObject congratsScene;
    // public GameObject tryAgainScene;
    // public GameObject rabbit;
    // public GameObject monster;
    
    Color defaultButtonColor= Color.white; // The default color for buttons
    Color correctButtonColor =Color.green; // The color for correct answers
    Color incorrectButtonColor = Color.red; 
    private int correctAnswer;
    [SerializeField] int correctAnswerFromApi;
    private int[] answerOptions = new int[4];
    private int correctAnswersCount = 0;
    private int totalQuestions = 5; // You can change this to the desired number of questions
    private int questioncount=0;
    
        public GameObject monster1;
        public GameObject monster2;
        public GameObject monster3;
        public GameObject monster4;
        public GameObject monster5;
    
    void Start()
    {
        StartCoroutine(GenerateQuestionWithDelay(0.0f)); // Delay for 1 second
    }
    
   IEnumerator GenerateQuestionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GenerateQuestion();
    //    yield return new WaitForSeconds(0.7f); // Delay for 1 second before making buttons visible
        GenerateAnswerOptions();
    }

     IEnumerator GeneratecongoWithDelay(float delay)
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.Win);
        yield return new WaitForSeconds(delay);
        
    //    yield return new WaitForSeconds(0.7f); // Delay for 1 second before making buttons visible
        SceneManager.LoadScene("congratsScene");

    }
    
    IEnumerator GeneratetryWithDelay(float delay)
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.TryAgain);
        yield return new WaitForSeconds(delay);
        
    //    yield return new WaitForSeconds(0.7f); // Delay for 1 second before making buttons visible
        SceneManager.LoadScene("tryAgainScene");

    }

    void GenerateQuestion()
    {
       // yield return new WaitForSeconds(1);
        // Generate two random numbers
        int number1 = Random.Range(1, 10);
        int number2 = Random.Range(1, 10);

        // Calculate the correct answer
        correctAnswer = number1 * number2;

        // Display the question in the question panel
        questionText.text = number1 + " x " + number2 + " = ";
        StartCoroutine(PostData(number1, number2));
    }

    void GenerateAnswerOptions()
    {
        // Randomly select a panel for the correct answer
       // yield return new WaitForSeconds(1);
        int correctAnswerPanelIndex = Random.Range(0, answerTexts.Length);
        questioncount++;
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
            Button answerButton = answerTexts[i].transform.parent.GetComponent<Button>();
            answerButton.image.color = defaultButtonColor;
        }
    }

    IEnumerator PostData(int a, int b)
    {
        
        string json = $"{{\"a\": {a}, \"b\": \"{b}\"}}";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("https://rabbitgame.azurewebsites.net/api/CheckAnswer?code=rZn-BazuTVyHQbEBmFkfZ4LhAeT4ehEzg9qHjn7dOXkvAzFuFb0y-Q==", "POST");
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error sending POST request: Network error");
        }
        else if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error sending POST requesttt: HTTP error");
            Debug.Log("POST response: " + request.result);

        }
        else
        {
            var response = request.downloadHandler.text;
            MyClass myObject = JsonUtility.FromJson<MyClass>(request.downloadHandler.text);
            Debug.Log("Response: " + response);
            correctAnswerFromApi = myObject.result;
            
        }
    }
    [System.Serializable]
    public class MyClass
    {
        public int result { get; set; }
    }
    public void CheckAnswer(TMPro.TextMeshProUGUI selectedAnswerText)
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.Carrot);
        m_RabbitAnim.SetTrigger("Carrot");
        int selectedAnswer = int.Parse(selectedAnswerText.text);
        Button selectedButton = selectedAnswerText.GetComponentInParent<Button>();
        Image buttonImage = selectedButton.GetComponent<Image>();
        if (selectedAnswer == correctAnswer || selectedAnswer == correctAnswerFromApi)
        {
            carrot.SetActive(true);
            Debug.Log("Correct Answer!");
            correctAnswersCount++;
            scoreText.text= (correctAnswersCount*10).ToString()+"  ";
            //questionText.text=questionText.text+" "+correctAnswer.ToString();
            buttonImage.color = correctButtonColor;
            if (correctAnswersCount == totalQuestions)
            {
                Debug.Log("All questions answered correctly!");
                // Load the congrats scene
                //SceneManager.LoadScene("congratsScene");
                StartCoroutine(GeneratecongoWithDelay(1.5f));
            }
            else
            {
                // Generate a new question
                StartCoroutine(GenerateQuestionWithDelay(0.9f));
            }
            if (correctAnswersCount == 1)
            {
                // monster1.SetActive(false);
                Invoke(nameof(Delay2), 2.0f);         
            }
            else if (correctAnswersCount == 2)
            {
                // monster2.SetActive(false);
                Invoke(nameof(Delay3), 2.0f);            
            }
            else if (correctAnswersCount == 3)
            {
                //monster3.SetActive(false);
                Invoke(nameof(Delay4), 2.0f);          
            }
            else if (correctAnswersCount == 4)
            {
                // monster4.SetActive(false);
                Invoke(nameof(Delay5), 2.0f);
            }
        }
        else
        {
            selectedButton.image.color = incorrectButtonColor;
            questionText.text=questionText.text+correctAnswer.ToString();
            if(totalQuestions == questioncount)
            {
               //SceneManager.LoadScene("tryAgainScene");
               StartCoroutine(GeneratetryWithDelay(0.9f));
            }
            else
            {
            
            Debug.Log("Wrong Answer!");
            
            StartCoroutine(GenerateQuestionWithDelay(0.9f));
            }
            // Wrong answer, show try again scene
           // SceneManager.LoadScene("tryAgainScene");
        }
    }

    private void Delay2()
    {
        //   monster1.SetActive(false);
        monster2.SetActive(true);
    }
    private void Delay3()
    {
        //   monster2.SetActive(false);
        monster3.SetActive(true);
    }
    private void Delay4()
    {
        //   monster3.SetActive(false);
        monster4.SetActive(true);
    }
    private void Delay5()
    {
        // monster4.SetActive(false);
        monster5.SetActive(true);
    }
    
    
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}