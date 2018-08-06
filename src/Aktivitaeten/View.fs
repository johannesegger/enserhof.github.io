module Home.View

open Fable.Core.JsInterop
open Fable.Helpers.Moment
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

moment.locale "de" |> ignore

let root =
  let formatTime (year, month, day) (hour, minute) =
    let dateTime =
      System.DateTime(year, month, day, hour, minute, 0)
      |> moment.Invoke
    dateTime.format "dd, DD. MMMM YYYY, HH:mm \\U\\h\\r"

  [ Content.content []
      [ Heading.h1 [ Heading.Is3 ]
          [ str "Aktivitäten" ]
        Heading.h2 [ Heading.Is4 ]
          [ str "Stallarbeit erledigen" ]
        Content.content []
          [ Image.image
              [ Image.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Left) ] ]
              [ img [ Src (importAll "../../images/stallarbeit.jpg"); Style [ MaxWidth "640px" ] ] ]
            p []
              [ str "Ihr habt ab nun die Möglichkeit, mit uns in den Stall zu gehen."
                br []
                str "Wir reinigen gemeinsam die Koppel und die Ställe, pfücken Futter für die Hasen, heben gemeinsam frische Eier ab und füttern die Esel, Kühe und Ponys mit Heu."
                br []
                str "Das Stallgehen dauert ca. eine Stunde und findet bei jeder Witterung statt."
                br []
                str "Wir freuen uns, wenn ihr einfach mal vorbei schaut." ]
            str "Nächste Stallzeit: "
            b [] [ str (formatTime (2018, 08, 08) (08, 30)) ] ] ] ]
  