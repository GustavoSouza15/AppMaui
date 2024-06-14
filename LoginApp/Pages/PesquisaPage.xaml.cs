using System.Collections.ObjectModel;
using System.Linq;

namespace LoginApp.Pages
{
    public partial class PesquisaPage : ContentPage
    {
        public ObservableCollection<Music> SearchResults { get; set; }

        public PesquisaPage()
        {
            InitializeComponent();
            SearchResults = new ObservableCollection<Music>
            {
                new Music { Nome = "What I've Done", Artista = "Linkin Park", AudioSource = "What I've Done.mp3" },
                new Music { Nome = "Sweet Child O' Mine", Artista = "Guns N' Roses", AudioSource = "Sweet Child O' Mine.mp3" },
                new Music { Nome = "Through the Fire and Flames", Artista = "DragonForce", AudioSource = "Through the Fire and Flames.mp3" },
                new Music { Nome = "Bohemian Rhapsody", Artista = "DragonForce", AudioSource = "Bohemian Rhapsody.mp3" },
                new Music { Nome = "Demons", Artista = "Imagine Dragons", AudioSource = "Demons.mp3" },
                new Music { Nome = "Don't Stop Believin'", Artista = "Journey", AudioSource = "Don't Stop Believin'.mp3" },
                new Music { Nome = "Pelados em Santos", Artista = "Mamonas Assssinas", AudioSource = "Pelados em Santos.mp3" }
            };

            BindingContext = this;
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            PerformSearch(e.NewTextValue);
        }

        private void PerformSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                searchResultsListView.ItemsSource = SearchResults;
            }
            else
            {
                var filteredResults = SearchResults.Where(m => m.Nome.ToLower().Contains(searchText.ToLower()) || m.Artista.ToLower().Contains(searchText.ToLower())).ToList();
                searchResultsListView.ItemsSource = new ObservableCollection<Music>(filteredResults);
            }
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedMusic = e.CurrentSelection[0] as Music;

            // Aqui você deve implementar a lógica para reproduzir a música selecionada
            // Por enquanto, vamos apenas exibir uma mensagem
            await Navigation.PushAsync(new MusicPlayerPage(selectedMusic));

            ((CollectionView)sender).SelectedItem = null;
        }
    }

    public class Music
    {
        public string Nome { get; set; }
        public string Artista { get; set; }
        public string AudioSource { get; set; }
    }
}
