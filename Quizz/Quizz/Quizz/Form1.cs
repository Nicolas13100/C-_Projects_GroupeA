using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Quizz
{
    public partial class Form1 : Form
    {
        private List<Question> questions;
        private Question currentQuestion;
        private Random random = new Random();
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeQuestions();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
            {
                // Questions faciles
                new Question("Quelle est la capitale de la France ?", 
                             new List<string> { "Paris", "Londres", "Berlin", "Madrid" }, 0),
                new Question("Quel est le plus grand océan ?", 
                             new List<string> { "Atlantique", "Indien", "Pacifique", "Arctique" }, 2),
                new Question("Combien de continents y a-t-il ?", 
                             new List<string> { "5", "6", "7", "8" }, 2),
                new Question("Quel est le plus grand mammifère terrestre ?", 
                             new List<string> { "Éléphant", "Giraffe", "Rhinocéros", "Hippopotame" }, 0),
                new Question("Quel est l'animal national de l'Australie ?", 
                             new List<string> { "Kangourou", "Koala", "Dingo", "Émeu" }, 0),
                new Question("Quel fruit est connu pour sa richesse en vitamine C ?", 
                             new List<string> { "Banane", "Pomme", "Orange", "Raisin" }, 2),

                // Questions intermédiaires
                new Question("Quel est le pays d'origine du fromage parmesan ?", 
                             new List<string> { "France", "Italie", "Espagne", "Suisse" }, 1),
                new Question("Quel est le plus long fleuve du monde ?", 
                             new List<string> { "Nile", "Amazon", "Yangzi", "Mississippi" }, 0),
                new Question("Quel est l'élément chimique dont le symbole est 'O' ?", 
                             new List<string> { "Or", "Oxygène", "Hydrogène", "Carbone" }, 1),
                new Question("Qui a écrit 'Les Misérables' ?", 
                             new List<string> { "Émile Zola", "Victor Hugo", "Gustave Flaubert", "Marcel Proust" }, 1),
                new Question("Quel est le nom de l'organe responsable de la respiration ?", 
                             new List<string> { "Cœur", "Poumons", "Foie", "Reins" }, 1),
                new Question("Quel est le symbole chimique de l'or ?", 
                             new List<string> { "Au", "Ag", "Pb", "Fe" }, 0),

                // Questions difficiles
                new Question("Qui a peint 'La Nuit étoilée' ?", 
                             new List<string> { "Pablo Picasso", "Vincent van Gogh", "Claude Monet", "Henri Matisse" }, 1),
                new Question("Quel écrivain a écrit 'À la recherche du temps perdu' ?", 
                             new List<string> { "Marcel Proust", "Victor Hugo", "Gustave Flaubert", "Émile Zola" }, 0),
                new Question("Quel est le nom du premier satellite artificiel lancé dans l'espace ?", 
                             new List<string> { "Apollo 11", "Voyager 1", "Spoutnik 1", "Hubble" }, 2),
                new Question("Quel physicien a développé la théorie de la relativité ?", 
                             new List<string> { "Isaac Newton", "Albert Einstein", "Galilée", "Niels Bohr" }, 1),
                new Question("Quel est l'organe principal du système circulatoire ?", 
                             new List<string> { "Cerveau", "Cœur", "Foie", "Reins" }, 1),
                new Question("Quel pays a été le premier à envoyer un homme dans l'espace ?", 
                             new List<string> { "États-Unis", "Russie", "Chine", "France" }, 1),

                // Questions très difficiles
                new Question("Quel est le pays le plus peuplé du monde ?", 
                             new List<string> { "États-Unis", "Inde", "Chine", "Indonésie" }, 2),
                new Question("Quelle est la langue officielle du Brésil ?", 
                             new List<string> { "Espagnol", "Portugais", "Anglais", "Français" }, 1),
                new Question("Qui a découvert la pénicilline ?", 
                             new List<string> { "Louis Pasteur", "Alexander Fleming", "Marie Curie", "Joseph Lister" }, 1),
                new Question("Quel est le nom de la plus grande chaîne de montagnes du monde ?", 
                             new List<string> { "Himalaya", "Andes", "Alpes", "Rocallie" }, 0),
                new Question("Quel est le prix Nobel de la paix 2021 ?", 
                             new List<string> { "Abiy Ahmed", "Maria Ressa", "Malala Yousafzai", "Al Gore" }, 1),
                new Question("Quel est le principal gaz à effet de serre ?", 
                             new List<string> { "Dioxyde de carbone", "Méthane", "Oxygène", "Azote" }, 0)
            };

            ShuffleQuestions(questions);
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

        private void btnChargerQuestion_Click(object sender, EventArgs e)
        {
            int questionIndex = random.Next(questions.Count);
            currentQuestion = questions[questionIndex];
            lblQuestion.Text = currentQuestion.Texte;
            listBoxChoix.Items.Clear();
            listBoxChoix.Items.AddRange(currentQuestion.Choix.ToArray());
            btnValider.Enabled = false; // Disable Validate button until an answer is selected
        }

        private void listBoxChoix_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnValider.Enabled = listBoxChoix.SelectedIndex != -1; // Enable Validate button if an answer is selected
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            int indexChoix = listBoxChoix.SelectedIndex;
            string correctAnswer = currentQuestion.Choix[currentQuestion.ReponseCorrecte];

            if (indexChoix == currentQuestion.ReponseCorrecte)
            {
                score++;
                MessageBox.Show($"Correct! Votre score est: {score}");
            }
            else
            {
                MessageBox.Show($"Incorrect! La bonne réponse était: {correctAnswer}. Votre score est: {score}");
            }
        }

        private void btn5050_Click(object sender, EventArgs e)
        {
            if (currentQuestion != null)
            {
                // Get the indices of the correct answer and one wrong answer
                int correctIndex = currentQuestion.ReponseCorrecte;
                List<int> wrongIndices = new List<int>();

                for (int i = 0; i < currentQuestion.Choix.Count; i++)
                {
                    if (i != correctIndex) wrongIndices.Add(i);
                }

                // Randomly select one wrong answer
                int wrongIndexToRemove = wrongIndices[random.Next(wrongIndices.Count)];

                // Clear the list and add the correct answer and one wrong answer
                listBoxChoix.Items.Clear();
                listBoxChoix.Items.Add(currentQuestion.Choix[correctIndex]);
                listBoxChoix.Items.Add(currentQuestion.Choix[wrongIndexToRemove]);
            }
        }

        private void btnAppelAmi_Click(object sender, EventArgs e)
        {
            if (currentQuestion != null)
            {
                // Randomly suggest the correct answer or one wrong answer
                int correctIndex = currentQuestion.ReponseCorrecte;
                int suggestionIndex = random.Next(0, currentQuestion.Choix.Count);

                // Ensure the suggestion isn't the correct answer
                while (suggestionIndex == correctIndex)
                {
                    suggestionIndex = random.Next(0, currentQuestion.Choix.Count);
                }

                MessageBox.Show($"Je pense que la réponse pourrait être: {currentQuestion.Choix[suggestionIndex]}");
            }
        }

        private void btnDemanderPublic_Click(object sender, EventArgs e)
        {
            if (currentQuestion != null)
            {
                int correctIndex = currentQuestion.ReponseCorrecte;
                int audienceTotal = 100;
                int correctVotes = random.Next(40, 61); // 40% to 60% votes for the correct answer

                int wrongVotes = (audienceTotal - correctVotes) / (currentQuestion.Choix.Count - 1);

                StringBuilder results = new StringBuilder();
                for (int i = 0; i < currentQuestion.Choix.Count; i++)
                {
                    if (i == correctIndex)
                    {
                        results.AppendLine($"{currentQuestion.Choix[i]}: {correctVotes}%");
                    }
                    else
                    {
                        results.AppendLine($"{currentQuestion.Choix[i]}: {wrongVotes}%");
                    }
                }

                MessageBox.Show("Résultats du vote du public:\n" + results.ToString());
            }
        }
    }

    public class Question
    {
        public string Texte { get; set; }
        public List<string> Choix { get; set; }
        public int ReponseCorrecte { get; set; }

        public Question(string texte, List<string> choix, int reponseCorrecte)
        {
            Texte = texte;
            Choix = choix;
            ReponseCorrecte = reponseCorrecte;
        }
    }
}
