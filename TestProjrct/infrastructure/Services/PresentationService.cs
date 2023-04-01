using System;
using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using TestProjrct.Models;

namespace TestProjrct.infrastructure.Services
{
    public interface IPresentationService
    {
        void ViewData(List<Snack> snacks);
    }

    public class PresentationService: IPresentationService
    {
        public PresentationService() { }
        public void ViewData(List<Snack> snacks)
        {
            var doc = new Document(
                new Grid
                    {
                        Stroke = LineThickness.Double,
                        StrokeColor = ConsoleColor.DarkGray
                    }
                    .AddColumns(
                        new Column { Width = GridLength.Auto },
                        new Column { Width = GridLength.Auto },
                        new Column { Width = GridLength.Auto },
                        new Column { Width = GridLength.Auto },
                        new Column { Width = GridLength.Auto }
                    )

                    .AddChildren(snacks.Select(snack => new[]
                    {
                        new Cell
                            {
                                Stroke = LineThickness.Double,
                                Color = snack.Count > 0 ? ConsoleColor.DarkCyan : ConsoleColor.DarkRed,
                                TextAlign = TextAlign.Center
                            }
                            .AddChildren($"##{snack.Id}## ")
                            .AddChildren($"\n {snack.Name}")
                            .AddChildren($"\n (Price: {snack.Price}$)")
                            .AddChildren($"\n (Count: {snack.Count})")
                    }))
                
                );
            ConsoleRenderer.RenderDocument(doc);
        }
    }
}
