module Home.View

open Fable.Helpers.Moment
open Fable.Helpers.React
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
        span []
          [ str "Nächste Stallzeit: "
            b [] [ str (formatTime (2018, 08, 03) (08, 30)) ] ] ] ]
  