using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> allQuestionsList;
    public GameObject questionWindow, winLosePanel;
    private QuestionAndAnswers _currentQuestion;
    private Slider _slider;
    private List<QuestionAndAnswers> questionsList;
    private int questionNumber, trueCount, falseCount = 0;

    private void Start()
    {
        _slider = questionWindow.transform.Find("Slider").GetComponent<Slider>();
        questionsList = new List<QuestionAndAnswers>(){};
    }

    public void GetCategoryQuestions(string category)
    {
        foreach (QuestionAndAnswers item in allQuestionsList)
        {
            if (category == item.category.ToString())
            {
                questionsList.Add(item);
            }
        }
    }

    public void DisplayNewQuestion()
    {
        QuestionAndAnswers qnA = GetQuestion();
        if (qnA == null)
        {
            Debug.Log("spru kalmadı");
            return;
        }
        questionWindow.transform.Find("QuestionTextPanel").Find("QuestionText").GetComponent<Text>().text = qnA.questionText;
        questionWindow.transform.Find("Option1Panel").Find("Option1Text").GetComponent<Text>().text = qnA.options[0];
        questionWindow.transform.Find("Option2Panel").Find("Option2Text").GetComponent<Text>().text = qnA.options[1];
        questionWindow.transform.Find("Option3Panel").Find("Option3Text").GetComponent<Text>().text = qnA.options[2];
        questionWindow.transform.Find("Option4Panel").Find("Option4Text").GetComponent<Text>().text = qnA.options[3];
    }

    private QuestionAndAnswers GetQuestion()
    {
        if (IsAllQuestionsDisplayed())
            return null;
        QuestionAndAnswers qnA;
        do
        {
            qnA = questionsList[Random.Range(0, questionsList.Count)];  
        } while (qnA.isDisplayed);
        qnA.isDisplayed = true;
        _currentQuestion = qnA;
        return qnA;
    }

    private bool IsAllQuestionsDisplayed()
    {
        foreach (QuestionAndAnswers question in questionsList)
        {
            if (!question.isDisplayed)
                return false;
        }
        return true;
    }

    
    public void CheckAnswer(int buttonOrder)
    {
        if (buttonOrder == _currentQuestion.trueOption)
        {
            trueCount++;
        }
        else
        {
            falseCount++;
            _slider.value += 0.1f;
        }
        DisplayNewQuestion();
    }

    public void CheckIf10Question()
    {
        questionNumber++;
        if (questionNumber == 10)
        {
            questionNumber = 0;
            GameObject.Find("QuestionWindow").gameObject.SetActive(false);
            FinishGame();
        }

    }

    private void FinishGame()
    {
        if (Random.Range(0,trueCount+1) > Random.Range(0,falseCount+1))
        {
            winLosePanel.SetActive(true);
            
            winLosePanel.GetComponentInChildren<Text>().text = "Organ tamamlandı";
            winLosePanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            winLosePanel.SetActive(true);
            winLosePanel.GetComponent<Image>().color = Color.red;
            winLosePanel.GetComponentInChildren<Text>().text = "Organ mutasyona uğradı"; 
        }
        
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
    
}

