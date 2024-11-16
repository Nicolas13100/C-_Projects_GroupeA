using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quizz
{
    public partial class Form1 : Form
    {
        private Question currentQuestion;
        private Random random = new Random();
        private int score = 0;
        private int answeredQuestions = 0;
        private int questionIndex = 0; // Index to track the progression of questions
        private bool used5050 = false, usedFriendHelp = false, usedAudienceHelp = false; // Lifeline tracking
        private int currentGainLevel = 0; // Current gain level
        private List<int> currentDisplayedIndices = new List<int>(); // Keep track of currently displayed indices
        private string actualTheme;
        private Question asking = new Question("Choisissez le thème des 3 prochaines questions.", new List<string> {"Géographie", "Vivant", "Sciences", "Personnalités", "Arts"}, 0, "Nul");
        private List<Question> allQuestions = new List<Question>
            {
                new Question("Quelle est la capitale de la France ?", new List<string> { "Paris", "Londres", "Berlin", "Madrid" }, 0, "Géographie"),
                new Question("Quel est le plus grand océan ?", new List<string> { "Atlantique", "Indien", "Pacifique", "Arctique" }, 2, "Géographie"),
                new Question("Combien de continents y a-t-il ?", new List<string> { "5", "6", "7", "8" }, 2, "Géographie"),
                new Question("Quel est l'animal national de l'Australie ?", new List<string> { "Kangourou", "Koala", "Dingo", "Émeu" }, 0, "Géographie"),
                new Question("Quel est le pays d'origine du fromage parmesan ?", new List<string> { "France", "Italie", "Espagne", "Suisse" }, 1, "Géographie"),
                new Question("Quel est le plus long fleuve du monde ?", new List<string> { "Nile", "Amazon", "Yangzi", "Mississippi" }, 1, "Géographie"),
                new Question("Quel est le pays le plus peuplé du monde ?", new List<string> { "États-Unis", "Inde", "Chine", "Indonésie" }, 2, "Géographie"),
                new Question("Quel est le nom de la plus grande chaîne de montagnes du monde ?", new List<string> { "Himalaya", "Andes", "Alpes", "Rocallie" }, 0, "Géographie"),
                new Question("Quelle est la langue officielle du Brésil ?", new List<string> { "Espagnol", "Portugais", "Anglais", "Français" }, 1, "Géographie"),
                new Question("Quel est le plus grand mammifère terrestre ?", new List<string> { "Éléphant", "Giraffe", "Rhinocéros", "Hippopotame" }, 0, "Vivant"),
                new Question("Quel fruit est connu pour sa richesse en vitamine C ?", new List<string> { "Banane", "Pomme", "Orange", "Raisin" }, 2, "Vivant"),
                new Question("Quel est le nom de l'organe responsable de la respiration ?", new List<string> { "Cœur", "Poumons", "Foie", "Reins" }, 1, "Vivant"),
                new Question("Quel est l'organe principal du système circulatoire ?", new List<string> { "Cerveau", "Cœur", "Foie", "Reins" }, 1, "Vivant"),
                new Question("Quel est l'élément chimique dont le symbole est 'O' ?", new List<string> { "Or", "Oxygène", "Hydrogène", "Carbone" }, 1, "Sciences"),
                new Question("Quel est le symbole chimique de l'or ?", new List<string> { "Au", "Ag", "Pb", "Fe" }, 0, "Sciences"),
                new Question("Quel est le principal gaz à effet de serre ?", new List<string> { "Dioxyde de carbone", "Méthane", "Oxygène", "Azote" }, 0, "Sciences"),
                new Question("Quel pays a été le premier à envoyer un homme dans l'espace ?", new List<string> { "États-Unis", "Russie", "Chine", "France" }, 1, "Sciences"),
                new Question("Quel est le nom du premier satellite artificiel lancé dans l'espace ?", new List<string> { "Apollo 11", "Voyager 1", "Spoutnik 1", "Hubble" }, 2, "Sciences"),
                new Question("Quel physicien a développé la théorie de la relativité ?", new List<string> { "Isaac Newton", "Albert Einstein", "Galilée", "Niels Bohr" }, 1, "Personnalités"),
                new Question("Qui a découvert la pénicilline ?", new List<string> { "Louis Pasteur", "Alexander Fleming", "Marie Curie", "Joseph Lister" }, 1, "Personnalités"),
                new Question("Quel est le prix Nobel de la paix 2021 ?", new List<string> { "Abiy Ahmed", "Maria Ressa", "Malala Yousafzai", "Al Gore" }, 1, "Personnalités"),
                new Question("Qui a écrit 'Les Misérables' ?", new List<string> { "Émile Zola", "Victor Hugo", "Gustave Flaubert", "Marcel Proust" }, 1, "Arts"),
                new Question("Qui a peint 'La Nuit étoilée' ?", new List<string> { "Pablo Picasso", "Vincent van Gogh", "Claude Monet", "Henri Matisse" }, 1, "Arts"),
                new Question("Quel écrivain a écrit 'À la recherche du temps perdu' ?", new List<string> { "Marcel Proust", "Victor Hugo", "Gustave Flaubert", "Émile Zola" }, 0, "Arts"),
            };
        private List<Question> currentQuestions = new List<Question>();

        public Form1()
        {
            InitializeComponent();
            AskForTheme();
            GetQuestions();
            LoadNextQuestion();
        }

        private void GetQuestions()
        {
            currentQuestions.Clear();
            while (currentQuestions.Count < 3)
            {
                Question nextQuestion = allQuestions[random.Next()];
                while (nextQuestion.Theme != actualTheme)
                {
                    nextQuestion = allQuestions[random.Next(allQuestions.Count)];
                }
                currentQuestions.Add(nextQuestion);
            }
        }

        private void ShuffleQuestions(List<Question> questions)
        {
            int n = questions.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                var temp = questions[n];
                questions[n] = questions[k];
                questions[k] = temp;
            }
        }

        private void LoadNextQuestion()
        {
            if (answeredQuestions < 15)
            {
                if (questionIndex == 3)
                {
                    questionIndex = 0;
                    AskForTheme(); // Asks for the theme of the 3 next questions
                    GetQuestions(); // Generates the 3 next questions
                } else {
                    // Load the next question
                    currentQuestion = currentQuestions[questionIndex];
                    lblQuestion.Text = currentQuestion.Texte;
                    listBoxChoix.Items.Clear();
                    listBoxChoix.Items.AddRange(currentQuestion.Choix.ToArray());

                    // Populate currentDisplayedIndices with all choice indices
                    currentDisplayedIndices.Clear(); // Clear previous indices
                    for (int i = 0; i < currentQuestion.Choix.Count; i++)
                    {
                        currentDisplayedIndices.Add(i);
                    }
                    UpdateGainsDisplay(); // Updates gain display

                    questionIndex++;
                }

                btnValider.Enabled = false; // Disable button until an answer is selected
                listBoxChoix.SelectedIndexChanged += ListBoxChoix_SelectedIndexChanged;
            }
            else
            {
                EndGame(true); // End game with victory
            }
        }

        private void UpdateGainsDisplay()
        {
            // Highlight the current gain level
            listBoxGains.SelectedIndex = listBoxGains.Items.Count - currentGainLevel - 1;
        }

        private void ListBoxChoix_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnValider.Enabled = true; // Enable button once an answer is selected
        }

        private void AskForTheme()
        {
            //Disables the bonus buttons
            btnAppelAmi.Enabled = false; 
            btn5050.Enabled = false;
            btnDemanderPublic.Enabled = false;

            lblQuestion.Text = asking.Texte;
            listBoxChoix.Items.Clear();
            listBoxChoix.Items.AddRange(asking.Choix.ToArray());

            currentDisplayedIndices.Clear();
            for (int i = 0; i < asking.Choix.Count; i++)
            {
                currentDisplayedIndices.Add(i);
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            int indexChoix = listBoxChoix.SelectedIndex; // This is the index in the reduced list

            if (indexChoix == -1) return; // Ensure an item is selected

            // Ensure currentDisplayedIndices is not empty and has valid elements
            if (currentDisplayedIndices.Count == 0 || indexChoix >= currentDisplayedIndices.Count)
            {
                MessageBox.Show("Erreur: L'index de choix sélectionné est invalide.");
                return;
            }

            // Determine which index corresponds to the selected answer in the original list
            int selectedOriginalIndex = currentDisplayedIndices[indexChoix]; // Index in the displayed choices
            
            if (questionIndex < 3)
            {
                // Get the original correct answer index based on the current displayed indices
                int originalCorrectIndex = currentQuestion.ReponseCorrecte;

                if (selectedOriginalIndex == originalCorrectIndex)
                {
                    score++;
                    currentGainLevel++;
                    MessageBox.Show($"Correct! Vous avez gagné: {listBoxGains.Items[listBoxGains.Items.Count - currentGainLevel]}");
                    LoadNextQuestion(); // Move to the next question
                }
                else
                {
                    EndGame(false); // End game with an incorrect answer
                }
            } 
            else
            {
                actualTheme = asking.Choix[selectedOriginalIndex];
            }
        }

        private void btn5050_Click(object sender, EventArgs e)
        {
            if (!used5050 && currentQuestion != null)
            {
                used5050 = true;
                btn5050.Enabled = false;

                int correctIndex = currentQuestion.ReponseCorrecte;
                List<int> wrongIndices = new List<int>();

                for (int i = 0; i < currentQuestion.Choix.Count; i++)
                {
                    if (i != correctIndex) wrongIndices.Add(i);
                }

                int wrongIndexToRemove = wrongIndices[random.Next(wrongIndices.Count)];
                ShowLimitedChoices(correctIndex, wrongIndexToRemove);
            }
        }

        private void ShowLimitedChoices(int correctIndex, int wrongIndex)
        {
            listBoxChoix.Items.Clear();
            listBoxChoix.Items.Add(currentQuestion.Choix[correctIndex]);
            listBoxChoix.Items.Add(currentQuestion.Choix[wrongIndex]);

            // Save the displayed indices
            currentDisplayedIndices.Clear(); // Clear previous indices
            currentDisplayedIndices.Add(correctIndex);
            currentDisplayedIndices.Add(wrongIndex);
        }

private void btnAppelAmi_Click(object sender, EventArgs e)
{
    if (!usedFriendHelp && currentQuestion != null)
    {
        usedFriendHelp = true;
        btnAppelAmi.Enabled = false;

        int correctIndex = currentQuestion.ReponseCorrecte;
        int suggestedAnswerIndex = random.Next(0, currentQuestion.Choix.Count);

        // Ensure the suggestion is either correct or a random wrong answer
        if (random.Next(2) == 0) // 50% chance to suggest the correct answer
        {
            MessageBox.Show($"Votre ami suggère: {currentQuestion.Choix[correctIndex]}");
        }
        else
        {
            while (suggestedAnswerIndex == correctIndex) // Ensure it's a wrong answer
            {
                suggestedAnswerIndex = random.Next(0, currentQuestion.Choix.Count);
            }
            MessageBox.Show($"Votre ami suggère: {currentQuestion.Choix[suggestedAnswerIndex]}");
        }
    }
}

private void btnDemanderPublic_Click(object sender, EventArgs e)
{
    if (!usedAudienceHelp && currentQuestion != null)
    {
        usedAudienceHelp = true;
        btnDemanderPublic.Enabled = false;

        int correctIndex = currentQuestion.ReponseCorrecte;
        List<int> audienceResults = new List<int>(new int[currentQuestion.Choix.Count]);

        // Simulate audience votes (e.g., 60% for the correct answer, 10% for others)
        audienceResults[correctIndex] = 60; // Correct answer gets 60%
        for (int i = 0; i < audienceResults.Count; i++)
        {
            if (i != correctIndex)
            {
                audienceResults[i] = random.Next(5, 20); // Random percentage for wrong answers
            }
        }

        string resultMessage = "Résultats du vote du public:\n";
        for (int i = 0; i < audienceResults.Count; i++)
        {
            resultMessage += $"{currentQuestion.Choix[i]}: {audienceResults[i]}%\n";
        }
        MessageBox.Show(resultMessage);
    }
}

	private void listBoxChoix_SelectedIndexChanged(object sender, EventArgs e)
		{
    		btnValider.Enabled = listBoxChoix.SelectedIndex != -1;
		}

        private void EndGame(bool isVictory)
        {
            // Display end game message and reset the game if needed
            string message = isVictory ? "Félicitations, vous avez gagné!" : "Désolé, vous avez perdu.";
            MessageBox.Show(message);
            ResetGame(); // Reset the game for the next round
        }

        private void ResetGame()
        {
            score = 0;
            questionIndex = 0;
            used5050 = false;
            usedFriendHelp = false;
            usedAudienceHelp = false;
            currentGainLevel = 0;

            AskForTheme();
            GetQuestions(); 

            LoadNextQuestion(); // Load the first question again
        }
    }

    public class Question
    {
        public string Texte { get; }
        public List<string> Choix { get; }
        public int ReponseCorrecte { get; }
        public string Theme { get; }

        public Question(string texte, List<string> choix, int reponseCorrecte, string theme)
        {
            Texte = texte;
            Choix = choix;
            ReponseCorrecte = reponseCorrecte;
            Theme = theme;
        }
    }
}
