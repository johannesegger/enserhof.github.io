module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map UeberDenHof (s "ueber-den-hof")
    map Aktivitaeten (s "aktivitaeten")
  ]

let urlUpdate (page: Option<Page>) model =
  let (model', cmd') =
    page
    |> FSharp.Core.Option.map (fun page ->
      { model with CurrentPage = page }, Cmd.none
    )
    |> FSharp.Core.Option.defaultWith (fun () ->
      console.error("Error parsing url")
      model, Navigation.modifyUrl (toHash model.CurrentPage)
    )
  
  Fable.Import.Browser.window.document.title <- toString model'.CurrentPage |> sprintf "%s | Enserhof z'Ehrndorf"

  GTag.setPage GTag.trackingId (toUrl model'.CurrentPage)

  model', cmd'

let init result =
  urlUpdate result { CurrentPage = Aktivitaeten }

let update () model =
  model, Cmd.none
