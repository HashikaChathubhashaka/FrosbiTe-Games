
const jsonDataArray = [
    {
      "questionTitle": "What is the primary source of energy for most power grids around the world?",
      "option1": "Solar power",
      "option2": "Wind power",
      "option3": "Fossil fuels",
      "option4": "Hydropower",
      "rightAnswer": "Fossil fuels",
      "feedback1": "Solar power is growing but is not the primary source globally.",
      "feedback2": "Wind power is significant in some areas but not the main source worldwide.",
      "feedback3": "Correct! Fossil fuels, including coal, natural gas, and oil, are currently the primary energy source for most power grids.",
      "feedback4": "Hydropower is a key renewable source but not the primary source globally.",
      "generalFeedback": "While the mix of energy sources varies by region, fossil fuels remain the dominant source for electricity generation globally, though renewable sources are on the rise.",
      "category": "string"
    },
    {
      "questionTitle": "What is the capital of France?",
      "option1": "London",
      "option2": "Paris",
      "option3": "Berlin",
      "option4": "Madrid",
      "rightAnswer": "Paris",
      "feedback1": "London is the capital of the United Kingdom.",
      "feedback2": "Correct! Paris is the capital of France.",
      "feedback3": "Berlin is the capital of Germany.",
      "feedback4": "Madrid is the capital of Spain.",
      "generalFeedback": "Paris is indeed the capital of France.",
      "category": "geography"
    }
  ];



  // Function to convert JSON data into the desired format for one problem
  function convertJsonToProblem(jsonData) {
    return {
      question: jsonData.questionTitle,
      options: [jsonData.option1, jsonData.option2, jsonData.option3, jsonData.option4],
      correctAnswer: jsonData.rightAnswer,
      General_Feedback: jsonData.generalFeedback,
      Specific_Feedback: [
        jsonData.feedback1,
        jsonData.feedback2,
        jsonData.feedback3,
        jsonData.feedback4
      ]
    };
  }
  
  // Convert JSON data for each problem into the desired format
  const problemData = jsonDataArray.map(convertJsonToProblem);
  
  // Now you can use the `problemData` array in your ReactJS application
  console.log(problemData);
  

  export default problemData;