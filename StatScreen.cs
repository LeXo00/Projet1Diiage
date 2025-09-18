using System.Collections;
using System.Collections.Specialized;
using System.Text.Json;

using Terminal.Gui;

namespace Client.Screens;
public class StatScreen(Window target)
{
    public Window Target { get; } = target;

    public async Task Show()
    {
        await BeforeShow();
        await ShowTitle();
    }

    private Task BeforeShow()
    {
        Target.RemoveAll();
        Target.Title = $"{MainWindow.Title} - [Statistics]";
        return Task.CompletedTask;
    }

    private async Task ShowTitle()
    {
        var titleLabel = new Label() { X = Pos.Center(), Y = 0 };
        var playersLabel = new Label() { X = Pos.Center(), Y = 2 };
        var gamesLabel = new Label() { X = Pos.Center(), Y = 4 };

        Target.Add(titleLabel);
        Target.Add(playersLabel);
        Target.Add(gamesLabel);

        titleLabel.Text = "Statistics";

        var httpClient = new HttpClient();
        var apiUrl = "http://localhost:5432/statistics"; // adapte l’URL

        while (true)
        {
            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var totalPlayers = root.GetProperty("TotalPlayers").GetInt32();
                var totalGames = root.GetProperty("TotalGames").GetInt32();

                playersLabel.Text = $"Total Players: {totalPlayers}";
                gamesLabel.Text = $"Total Games: {totalGames}";
            }
            catch
            {
                playersLabel.Text = "Erreur réseau ou serveur";
                gamesLabel.Text = "Impossible de charger les stats";
            }

            await Task.Delay(5000);
        }


    }
}
