namespace PeityCharts

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.Peity

[<JavaScript>]
module Client =
    open WebSharper.UI.Html
    open WebSharper.UI.Client

    let chart1 =
        UpdatingChart([], config = PeityConfig(Width = 200, Height = 150))

    let chart2 =
        UpdatingChart([], 10,
            PeityConfig(
                Width = 200,
                Height = 150,
                Fill  = [| "rgba(255,0,0,0.5)" |],
                Stroke = [| "red" |]
            ))

    let rec updateWithRandomNumbers (c: UpdatingChart) =
        async {
            let! _ = Async.Sleep 1000
            c.AddValue <| Math.Random()
            updateWithRandomNumbers c
        }
        |> Async.Start

    [<SPAEntryPoint>]
    let main =
        let charts =
            div [] [
                span [] [chart1.Doc]
                span [] [chart2.Doc]
                div [] [
                    Doc.Button "Add random" [] (fun () ->
                        chart1.AddValue <| Math.Random()
                    )
                ]
                div [] [
                    text "Press the button to add a random value to the chart on the left."   
                ]
            ]
        updateWithRandomNumbers chart2 // Update chart2 with random numbers
        Doc.RunById "main" charts

