module Global

open Microsoft.FSharp.Reflection

Fable.Helpers.Moment.moment.locale "de" |> ignore

type Page =
  | Aktivitaeten
  | UeberDenHof
  | Lageplan

let allPages =
  let cases = FSharpType.GetUnionCases typeof<Page>
  [ for c in cases -> FSharpValue.MakeUnion(c, [| |]) :?> Page ]

let toHash = function
  | Aktivitaeten -> "#aktivitaeten"
  | UeberDenHof -> "#ueber-den-hof"
  | Lageplan -> "#lageplan"

let toUrl = toHash >> fun s -> s.Replace("#", "/")

let toString = function
  | Aktivitaeten -> "Aktivitäten"
  | UeberDenHof -> "Über den Hof"
  | Lageplan -> "Lageplan"
