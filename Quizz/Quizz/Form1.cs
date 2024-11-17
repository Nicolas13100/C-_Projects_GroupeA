using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
        private string actualTheme = "";

        private Question asking = new Question("Choisissez le thème des 3 prochaines questions.",
            new List<string> { "Géographie", "Vivant", "Sciences", "Personnalités", "Arts" }, 0, "Nul");

        private List<Question> allQuestions = new List<Question>
        {
            new Question("Quelle est la capitale de la France ?",
                new List<string> { "Paris", "Londres", "Berlin", "Madrid" }, 0, "Géographie"),
            new Question("Quel est le plus grand océan ?",
                new List<string> { "Atlantique", "Indien", "Pacifique", "Arctique" }, 2, "Géographie"),
            new Question("Combien de continents y a-t-il ?", new List<string> { "5", "6", "7", "8" }, 2, "Géographie"),
            new Question("Quel est l'animal national de l'Australie ?",
                new List<string> { "Kangourou", "Koala", "Dingo", "Émeu" }, 0, "Géographie"),
            new Question("Quel est le pays d'origine du fromage parmesan ?",
                new List<string> { "France", "Italie", "Espagne", "Suisse" }, 1, "Géographie"),
            new Question("Quel est le plus long fleuve du monde ?",
                new List<string> { "Nile", "Amazon", "Yangzi", "Mississippi" }, 1, "Géographie"),
            new Question("Quel est le pays le plus peuplé du monde ?",
                new List<string> { "États-Unis", "Inde", "Chine", "Indonésie" }, 2, "Géographie"),
            new Question("Quel est le nom de la plus grande chaîne de montagnes du monde ?",
                new List<string> { "Himalaya", "Andes", "Alpes", "Rocallie" }, 0, "Géographie"),
            new Question("Quelle est la langue officielle du Brésil ?",
                new List<string> { "Espagnol", "Portugais", "Anglais", "Français" }, 1, "Géographie"),
            new Question("Quel est le plus grand mammifère terrestre ?",
                new List<string> { "Éléphant", "Giraffe", "Rhinocéros", "Hippopotame" }, 0, "Vivant"),
            new Question("Quel fruit est connu pour sa richesse en vitamine C ?",
                new List<string> { "Banane", "Pomme", "Orange", "Raisin" }, 2, "Vivant"),
            new Question("Quel est le nom de l'organe responsable de la respiration ?",
                new List<string> { "Cœur", "Poumons", "Foie", "Reins" }, 1, "Vivant"),
            new Question("Quel est l'organe principal du système circulatoire ?",
                new List<string> { "Cerveau", "Cœur", "Foie", "Reins" }, 1, "Vivant"),
            new Question("Quel est l'élément chimique dont le symbole est 'O' ?",
                new List<string> { "Or", "Oxygène", "Hydrogène", "Carbone" }, 1, "Sciences"),
            new Question("Quel est le symbole chimique de l'or ?", new List<string> { "Au", "Ag", "Pb", "Fe" }, 0,
                "Sciences"),
            new Question("Quel est le principal gaz à effet de serre ?",
                new List<string> { "Dioxyde de carbone", "Méthane", "Oxygène", "Azote" }, 0, "Sciences"),
            new Question("Quel pays a été le premier à envoyer un homme dans l'espace ?",
                new List<string> { "États-Unis", "Russie", "Chine", "France" }, 1, "Sciences"),
            new Question("Quel est le nom du premier satellite artificiel lancé dans l'espace ?",
                new List<string> { "Apollo 11", "Voyager 1", "Spoutnik 1", "Hubble" }, 2, "Sciences"),
            new Question("Quel physicien a développé la théorie de la relativité ?",
                new List<string> { "Isaac Newton", "Albert Einstein", "Galilée", "Niels Bohr" }, 1, "Personnalités"),
            new Question("Qui a découvert la pénicilline ?",
                new List<string> { "Louis Pasteur", "Alexander Fleming", "Marie Curie", "Joseph Lister" }, 1,
                "Personnalités"),
            new Question("Quel est le prix Nobel de la paix 2021 ?",
                new List<string> { "Abiy Ahmed", "Maria Ressa", "Malala Yousafzai", "Al Gore" }, 1, "Personnalités"),
            new Question("Qui a écrit 'Les Misérables' ?",
                new List<string> { "Émile Zola", "Victor Hugo", "Gustave Flaubert", "Marcel Proust" }, 1, "Arts"),
            new Question("Qui a peint 'La Nuit étoilée' ?",
                new List<string> { "Pablo Picasso", "Vincent van Gogh", "Claude Monet", "Henri Matisse" }, 1, "Arts"),
            new Question("Quel écrivain a écrit 'À la recherche du temps perdu' ?",
                new List<string> { "Marcel Proust", "Victor Hugo", "Gustave Flaubert", "Émile Zola" }, 0, "Arts"),
        };

        private List<Question> currentQuestions = new List<Question>();

        public Form1()
        {
            InitializeComponent();
            AskForTheme();
        }
        private void ListBoxChoix_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Check if an item is selected
            if (sender is ListBox listBox && listBox.SelectedIndex != -1)
            {
                btnValider.Enabled = true; // Enable the validation button after a selection
            }
        }
        private void GetQuestions()
        {
            if (string.IsNullOrEmpty(actualTheme))
            {
                MessageBox.Show("Veuillez choisir une theme avant de commencer !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var themeQuestions = allQuestions.Where(q => q.Theme == actualTheme).ToList();

            if (themeQuestions.Count < 3)
            {
                MessageBox.Show($"Pas assez de questions pour le thème {actualTheme}. Veuillez choisir un autre thème.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AskForTheme();
                return;
            }

            currentQuestions = themeQuestions.OrderBy(_ => random.Next()).Take(3).ToList();
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
                    AskForTheme(); // Ask for theme again after 3 questions
                    GetQuestions(); // Get 3 new random questions
                }
                else
                {
                    // Load the next question
                    currentQuestion = currentQuestions[questionIndex];
                    lblQuestion.Text = currentQuestion.Texte;
                    listBoxChoix.Items.Clear();
                    listBoxChoix.Items.AddRange(currentQuestion.Choix.ToArray());
                    currentDisplayedIndices.Clear();
                    for (int i = 0; i < currentQuestion.Choix.Count; i++)
                    {
                        currentDisplayedIndices.Add(i);
                    }

                    UpdateGainsDisplay();
                    questionIndex++;
                }

                btnValider.Enabled = false; // Disable validation button until an answer is selected
                listBoxChoix.SelectedIndexChanged += ListBoxChoix_SelectedIndexChanged;
            }
            else
            {
                EndGame(true); // End the game with a win
            }
        }

        private void UpdateGainsDisplay()
        {
            // Highlight the current gain level
            listBoxGains.SelectedIndex = listBoxGains.Items.Count - currentGainLevel - 1;
        }

        private void AskForTheme()
        {
			btnAppelAmi.Enabled = false; // Disable the lifeline buttons for theme selection
            btn5050.Enabled = false;
            btnDemanderPublic.Enabled = false;

            currentQuestion = asking;
            lblQuestion.Text = currentQuestion.Texte;           
            listBoxChoix.Items.Clear(); // Clear any existing items in the list box
    		listBoxChoix.Items.AddRange(currentQuestion.Choix.ToArray()); // Add the themes to the list box
			currentDisplayedIndices.Clear();
                    for (int i = 0; i < currentQuestion.Choix.Count; i++)
                    {
                        currentDisplayedIndices.Add(i);
                    }
            UpdateGainsDisplay();
            btnValider.Enabled = false; // Disable the validation button until a valid theme is selected
    
            // Handle the event when the user selects a theme
            listBoxChoix.SelectedIndexChanged += (sender, e) =>
            {
                // Enable the "Valider" button only if a valid item is selected
                if (listBoxChoix.SelectedIndex != -1 && !string.IsNullOrEmpty(listBoxChoix.SelectedItem?.ToString()))
                {
                    btnValider.Enabled = true;
                }
            };
        }


        private void btnValider_Click(object sender, EventArgs e)
        {
            int indexChoix = listBoxChoix.SelectedIndex; // Get the selected index in the theme selection list

            if (indexChoix == -1) return; // Ensure a valid choice has been made

            // Ensure currentDisplayedIndices has valid elements
            if (currentDisplayedIndices.Count == 0 || indexChoix >= currentDisplayedIndices.Count)
            {
                MessageBox.Show("Erreur: L'index de choix sélectionné est invalide.","Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int actualIndex = currentDisplayedIndices[indexChoix]; // Get the actual index based on the user selection

            if (lblQuestion.Text == asking.Texte) // Check if the question is asking for the theme
            {
                actualTheme = asking.Choix[actualIndex]; // Update the actualTheme variable with the selected theme
                asking.Choix.RemoveAt(actualIndex); // Removes the chosen theme from the list of possible themes
                btnAppelAmi.Enabled = true; // Enable the lifeline buttons after theme selection
                btn5050.Enabled = true;
                btnDemanderPublic.Enabled = true;

                GetQuestions(); // Fetch the questions related to the selected theme
                LoadNextQuestion(); // Load the first question based on the selected theme
                return;
            }

            // Otherwise, proceed with the answer validation and next question logic
            if (actualIndex == currentQuestion.ReponseCorrecte)
            {
                score++; // Increase the score for a correct answer
                currentGainLevel = Math.Min(currentGainLevel + 1, listBoxGains.Items.Count - 1);
                answeredQuestions++;
                MessageBox.Show("Bonne réponse !", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNextQuestion(); // Load the next question
            }
            else
            {
                // If the answer is incorrect, end the game
                EndGame(false);
            }
        }

        private void EndGame(bool victory)
        {
            string message;

            if (victory)
            {
                message = "Félicitations ! Vous avez gagné le grand prix avec un score de " + score + " points.";
            }
            else
            {
                // Calculate guaranteed prize base on the current gain level 
                int guaranteedPrizeLevel = currentGainLevel >= 10 ? 10 : (currentGainLevel >= 5 ? 5 : 0);
                message = "Dommage ! Vous avez perdu. Votre gain garanti est le niveau : " +
                        listBoxGains.Items[listBoxGains.Items.Count - guaranteedPrizeLevel - 1] + ".";

            }

            MessageBox.Show(message, "Fin du jeu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset the game state
            score = 0;
            answeredQuestions = 0;
            questionIndex = 0;
            currentGainLevel = 0;
            asking = new Question("Choisissez le thème des 3 prochaines questions.",
                new List<string> { "Géographie", "Vivant", "Sciences", "Personnalités", "Arts" }, 0, "Nul");

            AskForTheme();
        }

        private void btn5050_Click(object sender, EventArgs e)
        {
            if (used5050 || currentDisplayedIndices.Count <= 2) return;

            used5050 = true;
            btn5050.Enabled = false;

            int correctIndex = currentQuestion.ReponseCorrecte;
            List<int> incorrectIndices = currentDisplayedIndices.Where(i => i != correctIndex).ToList();

            // Remove all but one incorrect answer
            while (incorrectIndices.Count > 1)
                incorrectIndices.RemoveAt(random.Next(incorrectIndices.Count));

            // Combine the correct answer with the remaining incorrect answer
            currentDisplayedIndices = new List<int> { correctIndex };
            currentDisplayedIndices.AddRange(incorrectIndices);
            currentDisplayedIndices = currentDisplayedIndices.OrderBy(_ => random.Next()).ToList();

            // Update the list box display
            listBoxChoix.Items.Clear();
            foreach (int index in currentDisplayedIndices)
            {
                listBoxChoix.Items.Add(currentQuestion.Choix[index]);
            }
        }

        private void btnAppelAmi_Click(object sender, EventArgs e)
        {
            if (usedFriendHelp) return;

            usedFriendHelp = true;
            btnAppelAmi.Enabled = false;

            int friendGuess = currentQuestion.ReponseCorrecte;
            if (random.Next(0, 100) < 80) // 80% chance to give the correct answer
            {
                MessageBox.Show("Je suis sûr que la bonne réponse est : " + currentQuestion.Choix[friendGuess],
                    "Appel à un ami", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int wrongAnswer;
                do
                {
                    wrongAnswer = random.Next(currentDisplayedIndices.Count);
                } while (currentDisplayedIndices[wrongAnswer] == currentQuestion.ReponseCorrecte);

                MessageBox.Show("Je pense que la réponse est : " + currentQuestion.Choix[wrongAnswer], "Appel à un ami",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDemanderPublic_Click(object sender, EventArgs e)
        {
            if (usedAudienceHelp) return;
            usedAudienceHelp = true;
            btnDemanderPublic.Enabled = false;

            Dictionary<int, int> audienceVotes = new Dictionary<int, int>();
            int correctVotePercentage = random.Next(50, 80); // 50-80% confidence in correct answer
            int remainingPercentage = 100 - correctVotePercentage;

            audienceVotes[currentQuestion.ReponseCorrecte] = correctVotePercentage;

            foreach (int index in currentDisplayedIndices.Where(i => i != currentQuestion.ReponseCorrecte))
            {
                int vote = random.Next(0, remainingPercentage);
                audienceVotes[index] = vote;
                remainingPercentage -= vote;
            }

            // Distribute remaining votes to ensure total adds up to 100
            foreach (var key in audienceVotes.Keys.ToList())
            {
                if (remainingPercentage <= 0) break;
                audienceVotes[key]++;
                remainingPercentage--;
            }

            // Ensure the total percentage is 100
            if (remainingPercentage > 0)
            {
                audienceVotes[audienceVotes.Keys.Last()] += remainingPercentage;
            }

            MessageBox.Show("Le public pense que la réponse est :\n" +
                            string.Join("\n", audienceVotes.Select(pair =>
                                $"{currentQuestion.Choix[pair.Key]} - {pair.Value}%")),
                "Demander au public",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
