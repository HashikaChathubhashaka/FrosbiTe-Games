const problemData = [
    {
      //problem 1
        question: "What is the primary source of energy for most power grids around the world?",
        options: ["Solar power", "Wind power", "Fossil fuels", "Hydropower"],
        correctAnswer: "Fossil fuels",
        General_Feedback: "While the mix of energy sources varies by region, fossil fuels remain the dominant source for electricity generation globally, though renewable sources are on the rise.",
        Specific_Feedback: [
        "Solar power is growing but is not the primary source globally.",
        "Wind power is significant in some areas but not the main source worldwide.",
        "Correct! Fossil fuels, including coal, natural gas, and oil, are currently the primary energy source for most power grids.",
        "Hydropower is a key renewable source but not the primary source globally."]
    },
    {
        //problem 2
        question: "How does electricity typically travel from power plants to consumers?",
        options: ["Through water pipes", "Via transmission and distribution networks", "Directly from generators to homes", "Through the internet"],
        correctAnswer: "Via transmission and distribution networks",
        General_Feedback: "Electricity is generated at power plants and then transmitted over long distances via high-voltage transmission lines." + 
                            "It's then distributed to consumers through lower-voltage distribution networks.",
        Specific_Feedback: [
        "Water pipes are used for plumbing, not electrical transmission.",
        "Correct! Transmission and distribution networks are essential for delivering electricity from power plants to consumers.",
        "Electricity must be transmitted and distributed over networks; it doesn't go directly from generators to homes.",
        "The internet is a network for data, not electricity."]

    },

    {
        //problem 3
        question: "Why is energy efficiency important in homes and businesses?",
        options: ["It increases energy consumption", "It leads to higher energy costs", "It reduces energy bills and environmental impact", "It has no impact on the environment"],
        correctAnswer: "It reduces energy bills and environmental impact",
        General_Feedback: "Energy efficiency is crucial for reducing energy consumption, lowering energy bills, and minimizing the environmental footprint by decreasing greenhouse gas emissions.",
        Specific_Feedback: [
        "Energy efficiency aims to reduce, not increase, consumption.",
        "The goal of energy efficiency is to lower costs, not raise them.",
        "Correct! Energy efficiency helps in saving on energy bills and reducing the environmental impact.",
        "Energy efficiency has a significant positive impact on the environment by reducing emissions."]
        
    },

    {
        //problem 4
        question: "What is the primary goal of demand management in energy usage?",
        options: ["To increase the overall energy consumption", "To balance energy supply and demand", "To eliminate the use of renewable energy sources", "To double the energy costs for consumers"],
        correctAnswer: "To balance energy supply and demand",
        General_Feedback: "Demand management aims to ensure energy is used more efficiently, balancing the supply with the fluctuating demand to maintain grid stability and reduce costs.",
        Specific_Feedback: [
        "This is the opposite of demand management's goal, which aims to optimize, not increase, energy use.",
        "Correct! Balancing energy supply and demand helps improve grid reliability and efficiency.",
        "Demand management often encourages the integration of renewable energy sources, not their elimination.",
        "The goal is to potentially lower or optimize costs, not increase them."]
    },

    {
        //problem 5
        question: "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
        options: ["Increasing energy prices during off-peak hours", "Providing incentives for high energy consumption", "Offering lower rates or incentives for using less energy during peak times", "Discouraging the use of energy-efficient appliances"],
        correctAnswer: "Offering lower rates or incentives for using less energy during peak times",
        General_Feedback: "Lowering rates or providing incentives for reduced energy use during peak hours helps smooth out energy demand, benefiting both the grid and consumer.",
        Specific_Feedback: [
        "This approach would not encourage lower usage during peak times.",
        "Incentives are typically offered for reducing consumption, not increasing it.",
        "Correct! Incentives for lower usage during peak hours help manage demand effectively.",
        "Energy-efficient appliances are actually encouraged as part of demand management strategies."]

    },

    {
        //problem 6
        question: "Benefits to the consumer from demand management include:",
        options: ["Higher energy bills", "Less control over their energy use", "Savings on their electricity bill", "Reduced internet connectivity"],
        correctAnswer: "Savings on their electricity bill",
        General_Feedback: "Participating in demand management programs can lead to significant savings on electricity bills for consumers by incentivizing energy use during off-peak hours.",
        Specific_Feedback: [
        "Demand management aims to reduce, not increase, consumer energy bills.",
        "It actually offers more control over energy use and costs.",
        "Correct! One of the key benefits for consumers is the potential for savings on their electricity bills.",
        "Demand management focuses on energy consumption, not affecting internet connectivity."]
        
    },

    {
        //problem 7
        question: "How does implementing demand management strategies benefit the environment?",
        options: ["By significantly increasing carbon emissions", "By reducing reliance on fossil fuels and lowering carbon emissions", "By eliminating the need for public transportation", "By discouraging the use of renewable energy"],
        correctAnswer: "By reducing reliance on fossil fuels and lowering carbon emissions",
        General_Feedback: "Implementing demand management strategies plays a crucial role in environmental conservation by reducing the reliance on non-renewable energy sources and minimizing carbon emissions.",
        Specific_Feedback: [
        "Demand management aims to decrease, not increase, carbon emissions.",
        "Correct! Reducing reliance on fossil fuels and lowering carbon emissions are key environmental benefits of demand management.",
        "Demand management strategies do not involve transportation policies directly.",
        "These strategies typically encourage, rather than discourage, the use of renewable energy sources."]
        
    },

    {
        //problem 8
        question: "What can be a direct benefit of participating in a demand management program for consumers?",
        options: ["Higher energy bills", "Less control over their energy use", "Savings on their electricity bill", "Reduced internet connectivity"],
        correctAnswer: "Savings on their electricity bill",
        General_Feedback: "Participation in demand management programs often results in direct benefits for consumers, such as savings on electricity bills, by encouraging energy use during less expensive, off-peak hours.",
        Specific_Feedback: [
        "The goal of demand management is to offer savings, not to increase bills.",
        "Participants typically gain greater control and flexibility over their energy use.",
        "Correct! Saving on electricity bills is a significant benefit for consumers who participate in demand management programs.",
        "Demand management does not impact internet connectivity."]
        
    },

    {
        //problem 9
        question: "Why is load shifting important in demand management?",
        options: ["It increases the energy load during peak times", "It shifts energy usage to times when demand is higher", "It helps balance the power grid by using energy during lower-demand periods", "It makes energy more expensive during off-peak hours"],
        correctAnswer: "It helps balance the power grid by using energy during lower-demand periods",
        General_Feedback: "Load shifting is a critical component of demand management, aimed at moving energy use from peak to off-peak hours. This helps balance the power grid, reduces the need for additional power plants, and can lead to cost savings for consumers and utility providers alike.",
        Specific_Feedback: [
        "The purpose of load shifting is to decrease, not increase, the load during peak times to help balance energy demand.",
        "Shifting energy usage to higher demand times would counteract the goals of demand management, which seeks to alleviate these peaks.",
        "Correct! By shifting energy use to lower-demand periods, we can achieve a more balanced and efficient use of the power grid.",
        "Load shifting is designed to take advantage of lower costs during off-peak hours, not to make energy more expensive."]
        
    },

    {
        //problem 10
        question: "Which of the following electric loads can be effectively managed as part of a demand management program?",
        options: ["Fixed lighting systems in public areas", "Emergency medical equipment", "Residential air conditioning units", "Data centers that require constant cooling"],
        correctAnswer: "Residential air conditioning units",
        General_Feedback: "Demand management programs focus on adjusting the usage of flexible and non-critical electric loads to optimize energy consumption. Residential air conditioning units, for example, can be adjusted without compromising safety or critical operations, making them ideal for inclusion in these programs.",
        Specific_Feedback: [
        "While lighting can be managed, fixed systems in public areas often have safety implications that limit their flexibility.",
        "Emergency medical equipment is critical and cannot be subject to demand management due to the risk to human life.",
        "Correct! Residential air conditioning units are a significant and flexible load that can be adjusted to enhance grid efficiency without compromising comfort significantly.",
        "Data centers have strict cooling requirements for operational integrity and may not offer the flexibility required for effective demand management."]
        
    },

];

export default problemData;
