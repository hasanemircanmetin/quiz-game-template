using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class QuestionAndAnswers
{
    
    
    
    public string questionText;
    public string[] options;
    public int trueOption;
    public bool isDisplayed;
    public Category category;
}
[System.Serializable]
public enum Category
{
    Thyroid,
    Lungs,
    Liver
}